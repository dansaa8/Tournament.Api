using Tournament.Core.Dto;
using Tournament.Core.Dto.Queries;
using Tournament.Core.Req;

namespace Services.Contracts;

public interface IGameService
{
    Task<GameDto> GetGameByIdAsync(int id);

    Task<(IEnumerable<GameDto> gameDtos, MetaData metadata)> GetGamesAsync(
        GameQueryParameters queryParams);

    Task<GameDto> CreateGameAsync(GameCreateDto gameCreateDto, int tournamentId);
}