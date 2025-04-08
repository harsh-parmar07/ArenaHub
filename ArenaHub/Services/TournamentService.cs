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
    public class TournamentService : ITournamentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TournamentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TournamentViewDTO>> GetTournaments()
        {
            var tournaments = await _context.Tournaments
                .Include(t => t.Matches)
                .OrderBy(t => t.StartDate)
                .ToListAsync();

            var tournamentDTOs = _mapper.Map<List<TournamentViewDTO>>(tournaments);
            tournamentDTOs.ForEach(t => t.MatchCount = tournaments.First(tournament => tournament.Id == t.Id).Matches?.Count ?? 0);

            return tournamentDTOs;
        }

        public async Task<TournamentViewDTO> GetTournamentById(Guid id)
        {
            var tournament = await _context.Tournaments
                .Include(t => t.Matches)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tournament == null)
            {
                return null;
            }

            var tournamentDTO = _mapper.Map<TournamentViewDTO>(tournament);
            tournamentDTO.MatchCount = tournament.Matches?.Count ?? 0;

            return tournamentDTO;
        }

        public async Task<TournamentViewDTO> AddTournament(TournamentCreateDTO tournamentCreateDTO)
        {
            var tournament = _mapper.Map<Tournament>(tournamentCreateDTO);
            _context.Tournaments.Add(tournament);
            await _context.SaveChangesAsync();

            return await GetTournamentById(tournament.Id);
        }

        public async Task<TournamentViewDTO> UpdateTournament(Guid id, TournamentUpdateDTO tournamentUpdateDTO)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return null;
            }

            _mapper.Map(tournamentUpdateDTO, tournament);
            _context.Tournaments.Update(tournament);
            await _context.SaveChangesAsync();

            return await GetTournamentById(tournament.Id);
        }

        public async Task<bool> DeleteTournament(Guid id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return false;
            }

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}