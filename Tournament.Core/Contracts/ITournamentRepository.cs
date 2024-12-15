using Tournament.Core.Dto.Queries;
using Tournament.Core.Entities;
using Tournament.Core.Req;

namespace Tournament.Core.Contracts;

public interface ITournamentRepository
{
    Task<TournamentDetails?> GetTournamentByIdAsync(int id, bool trackChanges = false);

    Task<PagedList<TournamentDetails>> GetTournamentsAsync(
        TournamentQueryParameters queryParameters,
        bool trackChanges = false);

    Task<int> GetTournamentsCountAsync();
}