using ArenaHub.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArenaHub.Interfaces
{
    public interface IMatchService
    {
        Task<List<MatchViewDTO>> GetMatches();
        Task<MatchViewDTO> GetMatchById(Guid id);
        Task<MatchViewDTO> AddMatch(MatchCreateDTO matchCreateDTO);
        Task<MatchViewDTO> UpdateMatch(Guid id, MatchUpdateDTO matchUpdateDTO);
        Task<bool> DeleteMatch(Guid id);
        Task<List<MatchViewDTO>> GetMatchesByTournament(Guid tournamentId);
    }
}