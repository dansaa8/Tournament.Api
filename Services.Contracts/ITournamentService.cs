using Tournament.Core.Dto;
using Tournament.Core.Dto.Queries;

namespace Services.Contracts;

public interface ITournamentService
{
    Task<TournamentDto> GetTournamentByIdAsync(int id);
    Task<PagedResult<TournamentDto>> GetTournamentsAsync(TournamentQueryParameters queryParameters);
}