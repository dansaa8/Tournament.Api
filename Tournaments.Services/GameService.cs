using AutoMapper;
using Services.Contracts;
using Tournament.Core.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Dto.Queries;
using Tournament.Core.Entities;
using Tournament.Core.Exceptions;
using Tournament.Core.Req;

namespace Tournaments.Services;

public class GameService : IGameService
{
    private IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GameService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<GameDto> GetGameByIdAsync(int id)
    {
        Game? game = await _uow.GameRepository.GetGameByIdAsync(id);

        if (game == null)
        {
            throw new NotFoundException($"Game with id {id} was not found.");
        }

        return _mapper.Map<GameDto>(game);
    }

    public async Task<(IEnumerable<GameDto> gameDtos, MetaData metadata)> GetGamesAsync(
        GameQueryParameters queryParams)
    {
        var pagedList = await _uow.GameRepository.GetGamesAsync(queryParams);
        var gameDtos = _mapper.Map<IEnumerable<GameDto>>(pagedList.Items);
        return (gameDtos, pagedList.MetaData); 
    }
}