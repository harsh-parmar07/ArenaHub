using ArenaHub.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArenaHub.Interfaces
{
    public interface ITournamentService
    {
        Task<List<TournamentViewDTO>> GetTournaments();
        Task<TournamentViewDTO> GetTournamentById(Guid id);
        Task<TournamentViewDTO> AddTournament(TournamentCreateDTO tournamentCreateDTO);
        Task<TournamentViewDTO> UpdateTournament(Guid id, TournamentUpdateDTO tournamentUpdateDTO);
        Task<bool> DeleteTournament(Guid id);
    }
}