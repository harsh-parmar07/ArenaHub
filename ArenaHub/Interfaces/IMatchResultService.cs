using ArenaHub.DTOs;
using System;
using System.Threading.Tasks;

namespace ArenaHub.Interfaces
{
    public interface IMatchResultService
    {
        Task<MatchResultViewDTO> GetMatchResultByMatchId(Guid matchId);
        Task<MatchResultViewDTO> AddMatchResult(MatchResultCreateDTO matchResultCreateDTO);
        Task<MatchResultViewDTO> UpdateMatchResult(Guid id, MatchResultUpdateDTO matchResultUpdateDTO);
        Task<bool> DeleteMatchResult(Guid id);
    }
}