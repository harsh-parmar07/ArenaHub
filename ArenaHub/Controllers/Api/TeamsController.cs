using ArenaHub.DTOs;
using ArenaHub.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArenaHub.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teams = await _teamService.GetTeams();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var team = await _teamService.GetTeamById(id);
            return team == null ? NotFound() : Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeamCreateDTO teamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTeam = await _teamService.AddTeam(teamDto);
            return CreatedAtAction(nameof(GetById), new { id = createdTeam.Id }, createdTeam);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TeamUpdateDTO teamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedTeam = await _teamService.UpdateTeam(id, teamDto);
            return updatedTeam == null ? NotFound() : Ok(updatedTeam);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _teamService.DeleteTeam(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("{teamId}/players")]
        public async Task<IActionResult> GetTeamPlayers(Guid teamId)
        {
            var players = await _teamService.GetPlayersByTeam(teamId);
            return Ok(players);
        }
    }
}