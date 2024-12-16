using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Tournament.Core.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Dto.Queries;
using Microsoft.AspNetCore.Http;
using Tournament.Core.Entities;

namespace Tournament.Presentation.Controllers
{
    // [Route("api/games")]
    [ApiController]
    public class GamesController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet("api/games")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetAllGames(
            [FromQuery] GameQueryParameters queryParams)
        {
            var pagedResult =
                await _serviceManager.GameService.GetGamesAsync(queryParams);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metadata));
            return Ok(pagedResult.gameDtos);
        }

        [HttpGet("api/games/{id}")]
        public async Task<ActionResult<GameDto>> GetOneGame(int id)
        {
            var gameDto = await _serviceManager.GameService.GetGameByIdAsync(id);
            return Ok(gameDto);
        }

        [HttpPost("api/tournament/{tournamentId}/games")]
        public async Task<ActionResult<Game>> PostGame(int tournamentId, GameCreateDto reqBody)
        {
            var createdGameDto = await _serviceManager.GameService.CreateGameAsync(reqBody, tournamentId);
            return CreatedAtAction(nameof(GetOneGame), new { id = tournamentId }, createdGameDto);
        }

        [HttpPut("api/games/{gameId}")]
        public async Task<IActionResult> PutGame(int gameId, GameUpdateDto reqBody)
        {
            var updatedGame = await _serviceManager.GameService.PutGameAsync(gameId, reqBody);
            return Ok(updatedGame);
        }
      
        [HttpPatch("api/games/{gameId}")]
        public async Task<ActionResult<GameDto>> PatchGame(int gameId, JsonPatchDocument<GameUpdateDto> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest("No patch document provided");

            var gameToPatch = new GameUpdateDto(); // Dummy instance for validation
            patchDocument.ApplyTo(gameToPatch, ModelState);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var updatedGame = await _serviceManager.GameService.PatchGameAsync(gameId, patchDocument);

            return Ok(updatedGame);
        }
        
        [HttpDelete("api/games/{gameId}")]
        public async Task<IActionResult> DeleteGame(int gameId)
        {
            await _serviceManager.GameService.DeleteGameAsync(gameId);
            return NoContent();
        }
    }
}