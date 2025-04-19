using ArenaHub.DTOs;
using ArenaHub.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArenaHub.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;

        public TournamentsController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tournaments = await _tournamentService.GetTournaments();
            return Ok(tournaments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tournament = await _tournamentService.GetTournamentById(id);
            return tournament == null ? NotFound() : Ok(tournament);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TournamentCreateDTO tournamentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTournament = await _tournamentService.AddTournament(tournamentDto);
            return CreatedAtAction(nameof(GetById), new { id = createdTournament.Id }, createdTournament);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TournamentUpdateDTO tournamentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedTournament = await _tournamentService.UpdateTournament(id, tournamentDto);
            return updatedTournament == null ? NotFound() : Ok(updatedTournament);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _tournamentService.DeleteTournament(id);
            return result ? NoContent() : NotFound();
        }
    }
}