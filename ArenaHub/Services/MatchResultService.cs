using ArenaHub.Data;
using ArenaHub.DTOs;
using ArenaHub.Interfaces;
using ArenaHub.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ArenaHub.Services
{
    public class MatchResultService : IMatchResultService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MatchResultService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MatchResultViewDTO> GetMatchResultByMatchId(Guid matchId)
        {
            var matchResult = await _context.MatchResults
                .Include(mr => mr.MVPPlayer)
                .FirstOrDefaultAsync(mr => mr.MatchId == matchId);

            if (matchResult == null)
            {
                return null;
            }

            return _mapper.Map<MatchResultViewDTO>(matchResult);
        }

        public async Task<MatchResultViewDTO> AddMatchResult(MatchResultCreateDTO matchResultCreateDTO)
        {
            var matchResult = _mapper.Map<MatchResult>(matchResultCreateDTO);
            _context.MatchResults.Add(matchResult);
            await _context.SaveChangesAsync();

            return await GetMatchResultByMatchId(matchResult.MatchId);
        }

        public async Task<MatchResultViewDTO?> UpdateMatchResult(Guid id, MatchResultUpdateDTO matchResultUpdateDTO)
        {
            var matchResult = await _context.MatchResults.FindAsync(id);
            if (matchResult == null)
            {
                return null;
            }

            _mapper.Map(matchResultUpdateDTO, matchResult);
            _context.MatchResults.Update(matchResult);
            await _context.SaveChangesAsync();

            return await GetMatchResultByMatchId(matchResult.MatchId);
        }

        public async Task<bool> DeleteMatchResult(Guid id)
        {
            var matchResult = await _context.MatchResults.FindAsync(id);
            if (matchResult == null)
            {
                return false;
            }

            _context.MatchResults.Remove(matchResult);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}