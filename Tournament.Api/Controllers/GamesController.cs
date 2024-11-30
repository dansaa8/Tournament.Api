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

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
        {
            var allGames = _mapper.Map<IEnumerable<GameDto>>(await _uow.GameRepository.GetAllAsync());
            return Ok(allGames);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            Game? game = await _uow.GameRepository.GetAsync(id);
            var dto = _mapper.Map<GameDto>(game);
            return game == null ? NotFound() : Ok(dto);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game updatedGame)
        {
            if (id != updatedGame.Id)
            {
                return BadRequest();
            }
            Game? existingGame = await _uow.GameRepository.GetAsync(id);
            if (existingGame == null) return NotFound();

            existingGame = updatedGame;
            await _uow.CompleteAsync();
            return Ok(updatedGame);
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game newGame)
        {
            _uow.GameRepository.Add(newGame);
            await _uow.CompleteAsync();

            return CreatedAtAction("GetGame", new { id = newGame.Id }, newGame);
        }

        // DELETE: api/Games/5
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
