using Tournament.Core.Dto;

namespace Services.Contracts;

public interface IGameService
{
    Task<GameDto> GetGameByIdAsync(int id);
    Task<IEnumerable<GameDto>> GetGamesAsync(bool trackChanges = false);
}