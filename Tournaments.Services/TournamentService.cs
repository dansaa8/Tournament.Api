using AutoMapper;
using Services.Contracts;
using Tournament.Core.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Entities;

namespace Tournaments.Services;

public class TournamentService : ITournamentService
{
    private IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public TournamentService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<TournamentDto> GetTournamentByIdAsync(int id)
    {
        TournamentDetails? tournament = await _uow.TournamentRepository.GetTournamentByIdAsync(id);

        if (tournament == null)
        {
            // ToDo: Fix later
        }

        return _mapper.Map<TournamentDto>(tournament);
    }

    public async Task<IEnumerable<TournamentDto>> GetTournamentsAsync(bool includeGames, bool trackChanges = false)
    {
        return _mapper.Map<IEnumerable<TournamentDto>>(
            await _uow.TournamentRepository.GetTournamentsAsync(includeGames, trackChanges));
    }
}