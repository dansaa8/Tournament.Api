using Microsoft.AspNetCore.JsonPatch;
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

    Task<GameDto> UpdateGameAsync(int gameId, JsonPatchDocument<GameUpdateDto> patchDocument);
    Task<GameDto> PutGameAsync(int gameId, GameUpdateDto reqBody);
    Task DeleteGameAsync(int gameId);
}