using ArenaHub.Interfaces;
using ArenaHub.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArenaHub.Controllers.Web
{
    public class TeamsController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            var teams = await _teamService.GetTeams();
            return View(teams);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var team = await _teamService.GetTeamById(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }
    }
}