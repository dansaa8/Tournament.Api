using Tournament.Core.Dto;

namespace Services.Contracts;

public interface ITournamentService
{
    Task<TournamentDto> GetTournamentByIdAsync(int id);
    Task<IEnumerable<TournamentDto>> GetTournamentsAsync(bool includeGames, bool trackChanges = false);
}