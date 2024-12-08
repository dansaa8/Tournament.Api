using Tournament.Core.Dto.Queries;
using Tournament.Core.Entities;

namespace Tournament.Core.Contracts;

public interface ITournamentRepository
{
    Task<TournamentDetails?> GetTournamentByIdAsync(int id, bool trackChanges = false);

    Task<IEnumerable<TournamentDetails>> GetTournamentsAsync(
        TournamentQueryParameters queryParameters,
        bool trackChanges = false);

    Task<int> GetTournamentsCountAsync();
}