using Tournament.Core.Dto;
using Tournament.Core.Dto.Queries;
using Tournament.Core.Req;

namespace Services.Contracts;

public interface ITournamentService
{
    Task<TournamentDto> GetTournamentByIdAsync(int id);

    Task<(IEnumerable<TournamentDto> tournamentDtos, MetaData metadata)> GetTournamentsAsync(
        TournamentQueryParameters queryParams);
}