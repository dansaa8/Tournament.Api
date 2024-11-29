using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentApiContext _context;

        public TournamentRepository(TournamentApiContext context)
        {
            _context = context;
        }


        public async Task<bool> AnyAsync(int id)
        {
            return await _context.TournamentDetails.AnyAsync();
        }

        public async Task<IEnumerable<TournamentDetails>> GetAllAsync(bool includeGames = false)
        {
            return includeGames ?
                await _context.TournamentDetails.Include(td => td.Games).ToListAsync() :
                await _context.TournamentDetails.ToListAsync();
        }

        public async Task<TournamentDetails?> GetAsync(int id)
        {
            return await _context.TournamentDetails.FindAsync(id);
        }

        public void Add(TournamentDetails tournament)
        {
            _context.TournamentDetails.Add(tournament);
        }

        public void Remove(TournamentDetails tournament)
        {
            _context.TournamentDetails.Remove(tournament);
        }

        public void Update(TournamentDetails tournament)
        {
            _context.TournamentDetails.Update(tournament);
        }
    }
}
