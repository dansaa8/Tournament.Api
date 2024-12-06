using AutoMapper;
using Services.Contracts;
using Tournament.Core.Contracts;

namespace Tournaments.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITournamentService> _tournamentService;
        private readonly Lazy<IGameService> _gameService;

        public IGameService GameService => _gameService.Value;
        public ITournamentService TournamentService => _tournamentService.Value;

        public ServiceManager(IUnitOfWork uow, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(nameof(uow));

            _tournamentService = new Lazy<ITournamentService>(() => new TournamentService(uow, mapper));
            _gameService = new Lazy<IGameService>(() => new GameService(uow, mapper));
        }
    }
}