using System.Collections;
using AutoMapper;
using Services.Contracts;
using Tournament.Core.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Dto.Queries;
using Tournament.Core.Entities;
using Tournament.Core.Exceptions;

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

    public async Task<PagedResult<TournamentDto>> GetTournamentsAsync(
        TournamentQueryParameters queryParameters)
    {
        var tournaments = await _uow.TournamentRepository.GetTournamentsAsync(queryParameters);

        return new PagedResult<TournamentDto>
        {
            Data = queryParameters.IncludeGames
                ? _mapper.Map<IEnumerable<TournamentWithGamesDto>>(tournaments)
                : _mapper.Map<IEnumerable<TournamentDto>>(tournaments),
            TotalItems = await _uow.TournamentRepository.GetTournamentsCountAsync(),
            PageNumber = (int)queryParameters.PageNumber,
            PageSize = (int)queryParameters.PageSize
        };
    }
}