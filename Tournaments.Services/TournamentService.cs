using System.Collections;
using AutoMapper;
using Services.Contracts;
using Tournament.Core.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Entities;

namespace Tournaments.Services;

public class TournamentService(IUnitOfWork uow, IMapper mapper) : ITournamentService
{
    public async Task<TournamentDto> GetTournamentByIdAsync(int id)
    {
        TournamentDetails? tournament = await uow.TournamentRepository.GetTournamentByIdAsync(id);

        if (tournament == null)
        {
            // ToDo: Fix later
        }

        return mapper.Map<TournamentDto>(tournament);
    }

    public async Task<IEnumerable<TournamentDto>> GetTournamentsAsync(bool includeGames, bool trackChanges = false)
    {
        return includeGames
            ? mapper.Map<IEnumerable<TournamentWithGamesDto>>(
                await uow.TournamentRepository.GetTournamentsAsync(includeGames = true, trackChanges))
            : mapper.Map<IEnumerable<TournamentDto>>(
                await uow.TournamentRepository.GetTournamentsAsync(includeGames = false, trackChanges));
    }
}