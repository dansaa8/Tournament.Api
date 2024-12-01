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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetAllGames()
        {
            var allGames = _mapper.Map<IEnumerable<GameDto>>(await _uow.GameRepository.GetAllAsync());
            return Ok(allGames);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetOneGame(int id)
        {
            Game? game = await _uow.GameRepository.GetAsync(id);
            var dto = _mapper.Map<GameDto>(game);
            return game == null ? NotFound() : Ok(dto);
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

        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(GameCreateDto reqBody)
        {
            var game = _mapper.Map<Game>(reqBody);
            _uow.GameRepository.Add(game);
            await _uow.CompleteAsync();

            var dto = _mapper.Map<GameDto>(game);
            return CreatedAtAction(nameof(GetOneGame), new { id = game.Id }, dto);
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
