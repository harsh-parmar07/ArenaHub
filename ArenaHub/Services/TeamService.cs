using ArenaHub.Data;
using ArenaHub.DTOs;
using ArenaHub.Interfaces;
using ArenaHub.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArenaHub.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TeamService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TeamViewDTO>> GetTeams()
        {
            var teams = await _context.Teams
                .Include(t => t.Players)
                .Include(t => t.HomeMatches)
                .Include(t => t.AwayMatches)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<TeamViewDTO>>(teams);
        }

        public async Task<TeamViewDTO?> GetTeamById(Guid id)
        {
            var team = await _context.Teams
                .Include(t => t.Players)
                .Include(t => t.HomeMatches)
                .Include(t => t.AwayMatches)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            return team == null ? null : _mapper.Map<TeamViewDTO>(team);
        }

        public async Task<TeamViewDTO> AddTeam(TeamCreateDTO teamCreateDTO)
        {
            var team = _mapper.Map<Team>(teamCreateDTO);
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return _mapper.Map<TeamViewDTO>(team);
        }

        public async Task<TeamViewDTO?> UpdateTeam(Guid id, TeamUpdateDTO teamUpdateDTO)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return null;
            }

            _mapper.Map(teamUpdateDTO, team);
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();

            return await GetTeamById(team.Id);
        }

        public async Task<bool> DeleteTeam(Guid id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return false;
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<PlayerViewDTO>> GetPlayersByTeam(Guid teamId)
        {
            var players = await _context.Players
                .Where(p => p.TeamId == teamId)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<PlayerViewDTO>>(players);
        }

        public async Task<Result> DeleteTeamWithDependencies(Guid teamId)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    var team = await _context.Teams
                        .Include(t => t.Players)
                        .Include(t => t.HomeMatches)
                            .ThenInclude(m => m.MatchResult)
                        .Include(t => t.AwayMatches)
                            .ThenInclude(m => m.MatchResult)
                        .FirstOrDefaultAsync(t => t.Id == teamId);

                    if (team == null)
                    {
                        return Result.Fail("Team not found");
                    }

                    // Clear team reference from players
                    foreach (var player in team.Players)
                    {
                        player.TeamId = null;
                    }

                    // Delete all matches where this team is involved
                    var allMatches = team.HomeMatches.Concat(team.AwayMatches);
                    foreach (var match in allMatches)
                    {
                        // First delete MatchResult if exists
                        var matchResult = await _context.MatchResults
                            .FirstOrDefaultAsync(mr => mr.MatchId == match.Id);

                        if (matchResult != null)
                        {
                            _context.MatchResults.Remove(matchResult);
                        }

                        // Then delete the match
                        _context.Matches.Remove(match);
                    }

                    // Finally delete the team
                    _context.Teams.Remove(team);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Result.Ok();
                }
                catch (DbUpdateException ex)
                {
                    await transaction.RollbackAsync();
                    return Result.Fail($"Database error while deleting team: {ex.Message}");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Result.Fail($"Unexpected error while deleting team: {ex.Message}");
                }
            });
        }
    }

    public class Result
    {
        public bool Success { get; }
        public string Error { get; }
        public bool IsFailure => !Success;

        protected Result(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default, false, message);
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        protected internal Result(T value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }
    }
}