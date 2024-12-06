using System.Collections;
using AutoMapper;
using Services.Contracts;
using Tournament.Core.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Dto.Queries;
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

    public async Task<IEnumerable<TournamentDto>> GetTournamentsAsync(
        TournamentQueryParams queryParams)
    {
        var tournaments = await uow.TournamentRepository.GetTournamentsAsync(queryParams);
        return queryParams.IncludeGames
            ? mapper.Map<IEnumerable<TournamentWithGamesDto>>(
                await uow.TournamentRepository.GetTournamentsAsync(queryParams))
            : mapper.Map<IEnumerable<TournamentDto>>(
                await uow.TournamentRepository.GetTournamentsAsync(queryParams));
    }
}