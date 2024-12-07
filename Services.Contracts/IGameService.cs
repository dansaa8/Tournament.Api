using Tournament.Core.Dto;
using Tournament.Core.Dto.Queries;

namespace Services.Contracts;

public interface IGameService
{
    Task<GameDto> GetGameByIdAsync(int id);
    Task<PagedResult<GameDto>> GetGamesAsync(PagingQueryParams queryParams);
}