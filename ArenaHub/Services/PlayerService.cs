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
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PlayerService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PlayerViewDTO>> GetPlayers()
        {
            var players = await _context.Players
                .Include(p => p.Team)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<PlayerViewDTO>>(players);
        }

        public async Task<PlayerViewDTO> GetPlayerById(Guid id)
        {
            // Get basic player info
            var player = await _context.Players
                .Include(p => p.Team)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (player == null)
            {
                return null;
            }

            var playerDto = _mapper.Map<PlayerViewDTO>(player);

            // Get match history separately for better query efficiency
            playerDto.MatchHistory = await _context.MatchResults
                .Where(mr => mr.MVPPlayerId == id ||
                             mr.Match.HomeTeam.Players.Any(p => p.Id == id) ||
                             mr.Match.AwayTeam.Players.Any(p => p.Id == id))
                .OrderByDescending(mr => mr.Match.MatchDate)
                .Select(mr => new PlayerMatchHistoryDTO
                {
                    MatchId = mr.MatchId,
                    MatchDate = mr.Match.MatchDate,
                    HomeTeamName = mr.Match.HomeTeam.Name,
                    AwayTeamName = mr.Match.AwayTeam.Name,
                    HomeTeamScore = mr.HomeTeamScore,
                    AwayTeamScore = mr.AwayTeamScore,
                    WasMVP = mr.MVPPlayerId == id
                })
                .AsNoTracking()
                .ToListAsync();

            return playerDto;
        }

        public async Task<PlayerViewDTO> AddPlayer(PlayerCreateDTO playerCreateDTO)
        {
            var player = _mapper.Map<Player>(playerCreateDTO);
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return await GetPlayerById(player.Id);
        }

        public async Task<PlayerViewDTO> UpdatePlayer(Guid id, PlayerUpdateDTO playerUpdateDTO)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return null;
            }

            _mapper.Map(playerUpdateDTO, player);
            _context.Players.Update(player);
            await _context.SaveChangesAsync();

            return await GetPlayerById(player.Id);
        }

        public async Task<bool> DeletePlayer(Guid id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return false;
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}