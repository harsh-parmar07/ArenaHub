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
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MatchService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MatchViewDTO>> GetMatches()
        {
            var matches = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Tournament)
                .Include(m => m.MatchResult)
                    .ThenInclude(mr => mr.MVPPlayer)
                .OrderBy(m => m.MatchDate)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<MatchViewDTO>>(matches);
        }

        public async Task<MatchViewDTO> GetMatchById(Guid id)
        {
            var match = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Tournament)
                .Include(m => m.MatchResult)
                    .ThenInclude(mr => mr.MVPPlayer)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null)
            {
                return null;
            }

            var matchDto = _mapper.Map<MatchViewDTO>(match);

            // Ensure we have display properties
            matchDto.HomeTeamName = match.HomeTeam?.Name ?? "Unknown Team";
            matchDto.HomeTeamLogo = match.HomeTeam?.LogoUrl;
            matchDto.AwayTeamName = match.AwayTeam?.Name ?? "Unknown Team";
            matchDto.AwayTeamLogo = match.AwayTeam?.LogoUrl;
            matchDto.TournamentName = match.Tournament?.Name;

            return matchDto;
        }

        public async Task<MatchViewDTO> AddMatch(MatchCreateDTO matchCreateDTO)
        {
            var match = _mapper.Map<Match>(matchCreateDTO);
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return await GetMatchById(match.Id);
        }

        public async Task<MatchViewDTO> UpdateMatch(Guid id, MatchUpdateDTO matchUpdateDTO)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return null;
            }

            _mapper.Map(matchUpdateDTO, match);
            _context.Matches.Update(match);
            await _context.SaveChangesAsync();

            return await GetMatchById(match.Id);
        }

        public async Task<bool> DeleteMatch(Guid id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return false;
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<MatchViewDTO>> GetMatchesByTournament(Guid tournamentId)
        {
            var matches = await _context.Matches
                .Where(m => m.TournamentId == tournamentId)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Tournament)
                .Include(m => m.MatchResult)
                    .ThenInclude(mr => mr.MVPPlayer)
                .OrderBy(m => m.MatchDate)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<MatchViewDTO>>(matches);
        }
    }
}