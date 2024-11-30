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

namespace Tournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentDetailsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public TournamentDetailsController(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDetails>>> GetAllTournamentDetails()
        {
            var allTournaments = await _uow.TournamentRepository.GetAllAsync(true);
            return Ok(allTournaments);
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDetails>> GetTournamentDetails(int id)
        {
            TournamentDetails? tournament = await _uow.TournamentRepository.GetAsync(id);
            return tournament == null ? NotFound() : Ok(tournament);
        }

        // PUT: api/TournamentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournamentDetails(int id, TournamentDetails updatedTournament)
        {
            if (id != updatedTournament.Id) return BadRequest();

            TournamentDetails? existingTournament = await _uow.TournamentRepository.GetAsync(id);
            if (existingTournament == null) return NotFound();

            existingTournament = updatedTournament;

            await _uow.CompleteAsync();
            return Ok(updatedTournament);
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
