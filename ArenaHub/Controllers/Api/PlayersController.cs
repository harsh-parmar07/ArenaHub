using ArenaHub.DTOs;
using ArenaHub.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArenaHub.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var players = await _playerService.GetPlayers();
            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var player = await _playerService.GetPlayerById(id);
            return player == null ? NotFound() : Ok(player);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlayerCreateDTO playerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPlayer = await _playerService.AddPlayer(playerDto);
            return CreatedAtAction(nameof(GetById), new { id = createdPlayer.Id }, createdPlayer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PlayerUpdateDTO playerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedPlayer = await _playerService.UpdatePlayer(id, playerDto);
            return updatedPlayer == null ? NotFound() : Ok(updatedPlayer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _playerService.DeletePlayer(id);
            return result ? NoContent() : NotFound();
        }
    }
}