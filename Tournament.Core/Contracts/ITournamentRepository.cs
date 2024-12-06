using Tournament.Core.Entities;

namespace Tournament.Core.Contracts
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<TournamentDetails>> GetTournamentsAsync(bool includeGames, bool trackChanges = false);
        Task<TournamentDetails?> GetTournamentByIdAsync(int id,  bool trackChanges = false);
        // Task<bool> AnyAsync(int id);
        // void Add(TournamentDetails tournament);
        // void Update(TournamentDetails tournament);
        // void Remove(TournamentDetails tournament);
    }
}
