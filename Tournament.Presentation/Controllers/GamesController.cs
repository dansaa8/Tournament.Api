using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Tournament.Core.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Dto.Queries;
using Tournament.Core.Entities;

namespace Tournament.Presentation.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<GameDto>>> GetAllGames(
            [FromQuery] GameQueryParameters queryParams)
        {
            // ToDo: Skicka med sök condition om title-queryparam är medskickad?
            var gamesWithMetaData =
                await _serviceManager.GameService.GetGamesAsync(queryParams);
            return Ok(gamesWithMetaData);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetOneGame(int id)
        {
            var gameDto = await _serviceManager.GameService.GetGameByIdAsync(id);
            return Ok(gameDto);
        }
        //
        //
        // [HttpPost]
        // public async Task<ActionResult<Game>> PostGame(GameCreateDto reqBody)
        // {
        //     var newGame = _mapper.Map<Game>(reqBody);
        //     _uow.GameRepository.Add(newGame);
        //     await _uow.CompleteAsync();
        //
        //     var dto = _mapper.Map<GameDto>(newGame);
        //     return CreatedAtAction(nameof(GetOneGame), new { id = newGame.Id }, dto);
        // }
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