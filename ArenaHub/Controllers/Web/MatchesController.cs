using ArenaHub.Interfaces;
using ArenaHub.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArenaHub.Controllers.Web
{
    public class MatchesController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly ITournamentService _tournamentService;
        private readonly ITeamService _teamService;

        public MatchesController(
            IMatchService matchService,
            ITournamentService tournamentService,
            ITeamService teamService)
        {
            _matchService = matchService;
            _tournamentService = tournamentService;
            _teamService = teamService;
        }

        public async Task<IActionResult> Index(string filter = "upcoming")
        {
            var matches = await _matchService.GetMatches();
            var filteredMatches = filter switch
            {
                "past" => matches.Where(m => m.MatchDate < DateTime.Now).ToList(),
                "live" => matches.Where(m => m.MatchDate.Date == DateTime.Now.Date).ToList(),
                _ => matches.Where(m => m.MatchDate > DateTime.Now).ToList()
            };

            ViewBag.Filter = filter;
            return View(filteredMatches);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var match = await _matchService.GetMatchById(id);
            if (match == null)
            {
                return NotFound();
            }

            // Get related data for the view
            ViewBag.Tournaments = await _tournamentService.GetTournaments();
            ViewBag.Teams = await _teamService.GetTeams();

            return View(match);
        }

        public async Task<IActionResult> ByTournament(Guid tournamentId)
        {
            var matches = await _matchService.GetMatchesByTournament(tournamentId);
            var tournament = await _tournamentService.GetTournamentById(tournamentId);

            ViewBag.Tournament = tournament;
            return View(matches);
        }
    }
}