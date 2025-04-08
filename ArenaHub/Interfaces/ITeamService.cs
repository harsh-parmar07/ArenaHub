using ArenaHub.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArenaHub.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamViewDTO>> GetTeams();
        Task<TeamViewDTO> GetTeamById(Guid id);
        Task<TeamViewDTO> AddTeam(TeamCreateDTO teamCreateDTO);
        Task<TeamViewDTO> UpdateTeam(Guid id, TeamUpdateDTO teamUpdateDTO);
        Task<bool> DeleteTeam(Guid id);
        Task<List<PlayerViewDTO>> GetPlayersByTeam(Guid teamId);
    }
}