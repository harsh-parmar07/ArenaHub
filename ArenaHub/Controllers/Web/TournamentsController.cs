using ArenaHub.Interfaces;
using ArenaHub.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArenaHub.Controllers.Web
{
    public class TournamentsController : Controller
    {
        private readonly ITournamentService _tournamentService;
        private readonly IMatchService _matchService;

        public TournamentsController(
            ITournamentService tournamentService,
            IMatchService matchService)
        {
            _tournamentService = tournamentService;
            _matchService = matchService;
        }

        public async Task<IActionResult> Index(string status = "active")
        {
            var tournaments = await _tournamentService.GetTournaments();
            var filteredTournaments = status switch
            {
                "upcoming" => tournaments.Where(t => t.StartDate > DateTime.Now).ToList(),
                "completed" => tournaments.Where(t => t.EndDate < DateTime.Now).ToList(),
                _ => tournaments.Where(t => t.StartDate <= DateTime.Now && t.EndDate >= DateTime.Now).ToList()
            };

            ViewBag.Status = status;
            return View(filteredTournaments);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var tournament = await _tournamentService.GetTournamentById(id);
            if (tournament == null)
            {
                return NotFound();
            }

            // Get matches for this tournament
            var matches = await _matchService.GetMatchesByTournament(id);
            ViewBag.Matches = matches;

            return View(tournament);
        }

        public async Task<IActionResult> Standings(Guid id)
        {
            var tournament = await _tournamentService.GetTournamentById(id);
            if (tournament == null)
            {
                return NotFound();
            }

            // TODO: Implement standings logic
            return View(tournament);
        }
    }
}