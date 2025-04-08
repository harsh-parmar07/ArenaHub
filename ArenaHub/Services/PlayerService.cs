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
                .ToListAsync();

            return _mapper.Map<List<PlayerViewDTO>>(players);
        }

        public async Task<PlayerViewDTO> GetPlayerById(Guid id)
        {
            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (player == null)
            {
                return null;
            }

            return _mapper.Map<PlayerViewDTO>(player);
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