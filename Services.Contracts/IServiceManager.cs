using Services.Contracts;

namespace Tournaments.Services;

public interface IServiceManager
{
    IGameService GameService { get; }
    ITournamentService TournamentService { get; }
}