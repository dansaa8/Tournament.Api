using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Dto.Queries;

namespace Tournament.Presentation.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<TournamentDto>>> GetTournaments(
            [FromQuery] TournamentQueryParameters queryParams)
        {
            var tournamentsWithMetaData = 
                await _serviceManager.TournamentService.GetTournamentsAsync(queryParams);
            return Ok(tournamentsWithMetaData);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetOneTournament(int id)
        {
            var tournamentDto = await _serviceManager.TournamentService.GetTournamentByIdAsync(id);
            return Ok(tournamentDto);
        }

        // [HttpPost]
        // public async Task<ActionResult<TournamentDetails>> PostTournament(TournamentCreateDto reqBody)
        // {
        //     var newTournament = _mapper.Map<TournamentDetails>(reqBody);
        //     _uow.TournamentRepository.Add(newTournament);
        //     await _uow.CompleteAsync();
        //
        //     var dto = _mapper.Map<TournamentDto>(newTournament);
        //     return CreatedAtAction(nameof(GetOneTournament), new { id = newTournament.Id }, dto);
        // }
        //
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutTournament(int id, TournamentUpdateDto reqBody)
        // {
        //     TournamentDetails? tournament = await _uow.TournamentRepository.GetAsync(id);
        //     if (tournament == null) return NotFound();
        //
        //     _mapper.Map(reqBody, tournament);
        //
        //     await _uow.CompleteAsync();
        //     return Ok(_mapper.Map<TournamentDto>(tournament));
        // }
        //
        // [HttpPatch("{tournamentId}")]
        // public async Task<ActionResult<TournamentDto>> PatchTournament(int tournamentId,
        //     JsonPatchDocument<TournamentUpdateDto> patchDocument)
        // {
        //     if (patchDocument is null) return BadRequest("No patch document");
        //
        //     var tournamentToPatch = await _uow.TournamentRepository.GetAsync(tournamentId);
        //     if (tournamentToPatch == null) return NotFound("Tournament not found");
        //
        //     var dto = _mapper.Map<TournamentUpdateDto>(tournamentToPatch);
        //     patchDocument.ApplyTo(dto, ModelState);
        //
        //     TryValidateModel(dto);
        //     if (!ModelState.IsValid) return UnprocessableEntity(ModelState);
        //
        //     _mapper.Map(dto, tournamentToPatch);
        //     await _uow.CompleteAsync();
        //
        //     return NoContent();
        // }
        //
        //
        // // DELETE: api/TournamentDetails/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteTournamentDetails(int id)
        // {
        //     TournamentDetails? tournamentToDelete = await _uow.TournamentRepository.GetAsync(id);
        //     if (tournamentToDelete == null) return NotFound();
        //
        //     _uow.TournamentRepository.Remove(tournamentToDelete);
        //     await _uow.CompleteAsync();
        //
        //     return NoContent();
        // }

        //private bool TournamentDetailsExists(int id)
        //{
        //    return _context.TournamentDetails.Any(e => e.Id == id);
        //}
    }
}