using ArenaHub.DTOs;
using ArenaHub.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArenaHub.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchResultsController : ControllerBase
    {
        private readonly IMatchResultService _matchResultService;

        public MatchResultsController(IMatchResultService matchResultService)
        {
            _matchResultService = matchResultService;
        }

        [HttpGet("match/{matchId}")]
        public async Task<IActionResult> GetByMatchId(Guid matchId)
        {
            var result = await _matchResultService.GetMatchResultByMatchId(matchId);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MatchResultCreateDTO resultDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdResult = await _matchResultService.AddMatchResult(resultDto);
            
            // Fix: Use the MatchId from the DTO we received instead
            return CreatedAtAction(
                nameof(GetByMatchId), 
                new { matchId = resultDto.MatchId }, 
                createdResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MatchResultUpdateDTO resultDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedResult = await _matchResultService.UpdateMatchResult(id, resultDto);
            return updatedResult == null ? NotFound() : Ok(updatedResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _matchResultService.DeleteMatchResult(id);
            return result ? NoContent() : NotFound();
        }
    }
}