using System.Collections;
using AutoMapper;
using Services.Contracts;
using Tournament.Core.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Dto.Queries;
using Tournament.Core.Entities;

namespace Tournaments.Services;

public class TournamentService(IUnitOfWork _uow, IMapper _mapper) : ITournamentService
{
    public async Task<TournamentDto> GetTournamentByIdAsync(int id)
    {
        TournamentDetails? tournament = await _uow.TournamentRepository.GetTournamentByIdAsync(id);

        if (tournament == null)
        {
            // ToDo: Fix later
        }

        return _mapper.Map<TournamentDto>(tournament);
    }

    public async Task<PagedResult<TournamentDto>> GetTournamentsAsync(
        TournamentQueryParams queryParams)
    {
        var tournaments = await _uow.TournamentRepository.GetTournamentsAsync(queryParams);

        return new PagedResult<TournamentDto>
        {
            Data = queryParams.IncludeGames
                ? _mapper.Map<IEnumerable<TournamentWithGamesDto>>(tournaments)
                : _mapper.Map<IEnumerable<TournamentDto>>(tournaments),
            TotalItems = await _uow.TournamentRepository.GetTournamentsCountAsync(),
            PageNumber = (int)queryParams.PageNumber,
            PageSize = (int)queryParams.PageSize
        };
    }
}