using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using AutoMapper;
using Tournament.Core.Dto;
using Microsoft.AspNetCore.JsonPatch;

namespace Tournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public GamesController(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetAllGames(string? title = null)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                var allGames = _mapper.Map<IEnumerable<GameDto>>(await _uow.GameRepository.GetAllAsync());
                return Ok(allGames);
            }
            else
            {
                var allGamesByTitle = _mapper.Map<IEnumerable<GameDto>>(await _uow.GameRepository.GetAllByTitleAsync(title));
                return Ok(allGamesByTitle);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetOneGame(int id)
        {
            Game? game = await _uow.GameRepository.GetAsync(id);
            var dto = _mapper.Map<GameDto>(game);
            return game == null ? NotFound() : Ok(dto);
        }


        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(GameCreateDto reqBody)
        {
            var newGame = _mapper.Map<Game>(reqBody);
            _uow.GameRepository.Add(newGame);
            await _uow.CompleteAsync();

            var dto = _mapper.Map<GameDto>(newGame);
            return CreatedAtAction(nameof(GetOneGame), new { id = newGame.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameUpdateDto reqBody)
        {
            Game? game = await _uow.GameRepository.GetAsync(id);
            if (game == null) return NotFound();

            _mapper.Map(reqBody, game);

            await _uow.CompleteAsync();
            return Ok(_mapper.Map<GameDto>(game));
        }


        [HttpPatch("{gameId}")]
        public async Task<ActionResult<GameDto>> PatchGame(int gameId, JsonPatchDocument<GameUpdateDto> patchDocument)
        {
            if (patchDocument is null) return BadRequest("No patch document"); // Kollar patchdocument

            var gameToPatch = await _uow.GameRepository.GetAsync(gameId); // Hämtar game som ska patchas
            if (gameToPatch == null) return NotFound("Game not found"); // Om inget hittas med angivet id, return NotFound

            var dto = _mapper.Map<GameUpdateDto>(gameToPatch); // Mappa om fetchade game till GameUpdateDto (samma som patchdocument)
            patchDocument.ApplyTo(dto, ModelState); // Kolla så att dto:n uppfyller dto-kraven/attributen; exempelvis [required] & [maxlength] för prop

            if (!ModelState.IsValid) return UnprocessableEntity(ModelState); // Returnerar felmeddelanden om kraven ej uppfylls.

            _mapper.Map(dto, gameToPatch); // Uppdaterar de förändrade/patchde fälten på den ursprungligt hämtade(och trackade) Game-entiten 
            await _uow.CompleteAsync(); // Sparar förändringarna och propagerar de till databasen.

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            Game? game = await _uow.GameRepository.GetAsync(id);
            if (game == null) return NotFound();

            _uow.GameRepository.Remove(game);
            await _uow.CompleteAsync();

            return NoContent();
        }

        //private bool GameExists(int id)
        //{
        //    return _uow.GameRepository.AnyAsync(id);
        //}
    }
}
