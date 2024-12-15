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
        
        [Route("api/games")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetAllGames(
            [FromQuery] GameQueryParameters queryParams)
        {
            var pagedResult =
                await _serviceManager.GameService.GetGamesAsync(queryParams);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metadata));
            return Ok(pagedResult.gameDtos);
        }

        [Route("api/games")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetOneGame(int id)
        {
            var gameDto = await _serviceManager.GameService.GetGameByIdAsync(id);
            return Ok(gameDto);
        }

        [Route("api/tournament/{tournamentId}/games")]
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(int tournamentId, GameCreateDto reqBody)
        {
            var createdGameDto = await _serviceManager.GameService.CreateGameAsync(reqBody, tournamentId);
            return CreatedAtAction(nameof(GetOneGame), new { id = tournamentId }, createdGameDto);
        }
        //
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutGame(int id, GameUpdateDto reqBody)
        // {
        //     Game? game = await _uow.GameRepository.GetAsync(id);
        //     if (game == null) return NotFound();
        //
        //     _mapper.Map(reqBody, game);
        //
        //     await _uow.CompleteAsync();
        //     return Ok(_mapper.Map<GameDto>(game));
        // }
        //
        //
        // [HttpPatch("{gameId}")]
        // public async Task<ActionResult<GameDto>> PatchGame(int gameId, JsonPatchDocument<GameUpdateDto> patchDocument)
        // {
        //     if (patchDocument is null) return BadRequest("No patch document"); // Kollar patchdocument
        //
        //     var gameToPatch = await _uow.GameRepository.GetAsync(gameId); // Hämtar game som ska patchas
        //     if (gameToPatch == null) return NotFound("Game not found"); // Om inget hittas med angivet id, return NotFound
        //
        //     var dto = _mapper.Map<GameUpdateDto>(gameToPatch); // Mappa om fetchade game till GameUpdateDto (samma som patchdocument)
        //     patchDocument.ApplyTo(dto, ModelState); 
        //
        //     TryValidateModel(dto); // Kolla så att dto:n uppfyller dto-kraven/attributen; exempelvis [required] & [maxlength] för prop
        //     if (!ModelState.IsValid) return UnprocessableEntity(ModelState); // Returnerar felmeddelanden om kraven ej uppfylls.
        //
        //     _mapper.Map(dto, gameToPatch); // Uppdaterar de förändrade/patchde fälten på den ursprungligt hämtade(och trackade) Game-entiten 
        //     await _uow.CompleteAsync(); // Sparar förändringarna och propagerar de till databasen.
        //
        //     return NoContent();
        // }


        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteGame(int id)
        // {
        //     Game? game = await _uow.GameRepository.GetAsync(id);
        //     if (game == null) return NotFound();
        //
        //     _uow.GameRepository.Remove(game);
        //     await _uow.CompleteAsync();
        //
        //     return NoContent();
        // }

        //private bool GameExists(int id)
        //{
        //    return _uow.GameRepository.AnyAsync(id);
        //}
    }
}