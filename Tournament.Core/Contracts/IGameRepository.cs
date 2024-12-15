using Tournament.Core.Dto.Queries;
using Tournament.Core.Entities;
using Tournament.Core.Req;

namespace Tournament.Core.Contracts
{
    public interface IGameRepository
    {
        Task<PagedList<Game>> GetGamesAsync(GameQueryParameters queryParams, bool trackChanges = false);

        // Task<IEnumerable<Game>> GetGamesByTitle(string title);
        Task<Game?> GetGameByIdAsync(int id, bool trackChanges = false);

        // Task<bool> AnyAsync(int id);
        // void Add(Game game);
        // void Update(Game game);
        // void Remove(Game game);
        Task<int> CountGamesAsync();
    }
}