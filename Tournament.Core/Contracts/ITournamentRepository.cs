using Tournament.Core.Dto.Queries;
using Tournament.Core.Entities;

namespace Tournament.Data.Repositories;

public interface ITournamentRepository
{
    Task<TournamentDetails?> GetTournamentByIdAsync(int id, bool trackChanges = false);

    Task<IEnumerable<TournamentDetails>> GetTournamentsAsync(
        TournamentPagingQueryParams pagingQueryParams,
        bool trackChanges = false);

    Task<int> GetTournamentsCountAsync();
}