using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Contracts;
using Tournament.Core.Entities;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class TournamentRepository : RepositoryBase<TournamentDetails>, ITournamentRepository
    {
        public TournamentRepository(TournamentApiContext context) : base(context)
        {
        }

        public async Task<TournamentDetails?> GetTournamentByIdAsync(int id, bool trackChanges = false)
        {
            return await FindByCondition(t => t.Id == id, trackChanges).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TournamentDetails>> GetTournamentsAsync(bool includeGames,
            bool trackChanges = false)
        {
            return includeGames
                ? await FindAll(trackChanges)
                    .Include(t => t.Games).ToListAsync()
                : await FindAll(trackChanges).ToListAsync();
        }


        // public async Task<bool> AnyAsync(int id)
        // {
        //     return await _context.TournamentDetails.AnyAsync();
        // }
        //
        //     public void Add(TournamentDetails tournament)
        //     {
        //         _context.TournamentDetails.Add(tournament);
        //     }
        //
        //     public void Remove(TournamentDetails tournament)
        //     {
        //         _context.TournamentDetails.Remove(tournament);
        //     }
        //
        //     public void Update(TournamentDetails tournament)
        //     {
        //         _context.TournamentDetails.Update(tournament);
        //     }
        // }
    }
}