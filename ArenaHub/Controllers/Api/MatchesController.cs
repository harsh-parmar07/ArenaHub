using ArenaHub.DTOs;
using ArenaHub.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArenaHub.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchesController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matches = await _matchService.GetMatches();
            return Ok(matches);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var match = await _matchService.GetMatchById(id);
            return match == null ? NotFound() : Ok(match);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MatchCreateDTO matchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdMatch = await _matchService.AddMatch(matchDto);
            return CreatedAtAction(nameof(GetById), new { id = createdMatch.Id }, createdMatch);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MatchUpdateDTO matchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedMatch = await _matchService.UpdateMatch(id, matchDto);
            return updatedMatch == null ? NotFound() : Ok(updatedMatch);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _matchService.DeleteMatch(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("tournament/{tournamentId}")]
        public async Task<IActionResult> GetByTournament(Guid tournamentId)
        {
            var matches = await _matchService.GetMatchesByTournament(tournamentId);
            return Ok(matches);
        }
    }
}