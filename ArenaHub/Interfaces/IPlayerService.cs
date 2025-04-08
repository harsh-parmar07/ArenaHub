using ArenaHub.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArenaHub.Interfaces
{
    public interface IPlayerService
    {
        Task<List<PlayerViewDTO>> GetPlayers();
        Task<PlayerViewDTO> GetPlayerById(Guid id);
        Task<PlayerViewDTO> AddPlayer(PlayerCreateDTO playerCreateDTO);
        Task<PlayerViewDTO> UpdatePlayer(Guid id, PlayerUpdateDTO playerUpdateDTO);
        Task<bool> DeletePlayer(Guid id);
    }
}