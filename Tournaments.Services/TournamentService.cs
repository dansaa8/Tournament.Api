using System.Collections;
using AutoMapper;
using Services.Contracts;
using Tournament.Core.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Dto.Queries;
using Tournament.Core.Entities;
using Tournament.Core.Exceptions;
using Tournament.Core.Req;

namespace Tournaments.Services;

public class TournamentService(IUnitOfWork _uow, IMapper _mapper) : ITournamentService
{
    public async Task<TournamentDto> GetTournamentByIdAsync(int id)
    {
        TournamentDetails? tournament = await _uow.TournamentRepository.GetTournamentByIdAsync(id);

        if (tournament == null)
        {
            throw new NotFoundException($"Tournament with id {id} was not found.");
        }

        return _mapper.Map<TournamentDto>(tournament);
    }

    public async Task<(IEnumerable<TournamentDto> tournamentDtos, MetaData metadata)> GetTournamentsAsync(
        TournamentQueryParameters queryParams)
    {
        var pagedList = await _uow.TournamentRepository.GetTournamentsAsync(queryParams);
        
        var tournamentDtos = queryParams.IncludeGames
            ? _mapper.Map<IEnumerable<TournamentWithGamesDto>>(pagedList.Items)
            : _mapper.Map<IEnumerable<TournamentDto>>(pagedList.Items);
        return (tournamentDtos, pagedList.MetaData);
    }
}