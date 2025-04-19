using ArenaHub.Interfaces;
using ArenaHub.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArenaHub.Controllers.Web
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public async Task<IActionResult> Index()
        {
            var players = await _playerService.GetPlayers();
            return View(players);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var player = await _playerService.GetPlayerById(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }
    }
}