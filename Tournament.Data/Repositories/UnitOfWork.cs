using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Contracts;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TournamentApiContext _context;
        private readonly Lazy<ITournamentRepository> _tournamentRepository;
        private readonly Lazy<IGameRepository> _gameRepository;

        public ITournamentRepository TournamentRepository => _tournamentRepository.Value;
        public IGameRepository GameRepository => _gameRepository.Value;

        public UnitOfWork(TournamentApiContext context)
        {
            _context = context;
            _tournamentRepository = new Lazy<ITournamentRepository>(() => new TournamentRepository(_context));
            _gameRepository = new Lazy<IGameRepository>(() => new GameRepository(_context));
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}