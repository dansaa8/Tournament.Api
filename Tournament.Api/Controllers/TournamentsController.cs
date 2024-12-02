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

        [HttpPost]
        public async Task<ActionResult<TournamentDetails>> PostTournament(TournamentCreateDto reqBody)
        {
            var newTournament = _mapper.Map<TournamentDetails>(reqBody);
            _uow.TournamentRepository.Add(newTournament);
            await _uow.CompleteAsync();

            var dto = _mapper.Map<TournamentDto>(newTournament);
            return CreatedAtAction(nameof(GetOneTournament), new { id = newTournament.Id }, dto);
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

        [HttpPatch("{tournamentId}")]
        public async Task<ActionResult<TournamentDto>> PatchTournament(int tournamentId, JsonPatchDocument<TournamentUpdateDto> patchDocument)
        {
            if (patchDocument is null) return BadRequest("No patch document");

            var tournamentToPatch = await _uow.TournamentRepository.GetAsync(tournamentId);
            if (tournamentToPatch == null) return NotFound("Tournament not found");

            var dto = _mapper.Map<TournamentUpdateDto>(tournamentToPatch);
            patchDocument.ApplyTo(dto, ModelState);

            TryValidateModel(dto);
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            _mapper.Map(dto, tournamentToPatch);
            await _uow.CompleteAsync(); 

            return NoContent(); 
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
