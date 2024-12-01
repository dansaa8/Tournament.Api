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
    public class TournamentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public TournamentsController(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetAllTournaments()
        {
            var allTournaments = _mapper.Map<IEnumerable<TournamentDto>>(
                await _uow.TournamentRepository.GetAllAsync(true));
            return Ok(allTournaments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetOneTournament(int id)
        {
            var tournament = _mapper.Map<TournamentDto>(await _uow.TournamentRepository.GetAsync(id));
            return tournament == null ? NotFound() : Ok(tournament);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, TournamentUpdateDto reqBody)
        {

            TournamentDetails? tournament = await _uow.TournamentRepository.GetAsync(id);
            if (tournament == null) return NotFound();

            _mapper.Map(reqBody, tournament);

            await _uow.CompleteAsync();
            return Ok(_mapper.Map<TournamentDto>(tournament));
        }

        // POST: api/TournamentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDetails>> PostTournamentDetails(TournamentDetails newTournament)
        {
            _uow.TournamentRepository.Add(newTournament);
            await _uow.CompleteAsync();

            return CreatedAtAction("GetTournamentDetails", new { id = newTournament.Id }, newTournament);
        }

        // DELETE: api/TournamentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournamentDetails(int id)
        {
            TournamentDetails? tournamentToDelete = await _uow.TournamentRepository.GetAsync(id);
            if (tournamentToDelete == null) return NotFound();

            _uow.TournamentRepository.Remove(tournamentToDelete);
            await _uow.CompleteAsync();

            return NoContent();
        }

        //private bool TournamentDetailsExists(int id)
        //{
        //    return _context.TournamentDetails.Any(e => e.Id == id);
        //}
    }
}
