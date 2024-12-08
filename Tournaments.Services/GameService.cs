using AutoMapper;
using Services.Contracts;
using Tournament.Core.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Dto.Queries;
using Tournament.Core.Entities;

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
            // ToDo: Fix later
        }

        return _mapper.Map<GameDto>(game);
    }

    public async Task<PagedResult<GameDto>> GetGamesAsync(GameQueryParameters queryParams)
    {
        var games = await _uow.GameRepository.GetGamesAsync(queryParams);

        return new PagedResult<GameDto>()
        {
            Data = _mapper.Map<List<GameDto>>(games),
            TotalItems = await _uow.GameRepository.CountGamesAsync(),
            PageNumber = (int)queryParams.PageNumber,
            PageSize = (int)queryParams.PageSize
        };
    }
}