using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Contracts;
using Tournament.Core.Dto.Queries;
using Tournament.Core.Entities;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class TournamentRepository(TournamentApiContext context)
        : RepositoryBase<TournamentDetails>(context), ITournamentRepository
    {
        public async Task<TournamentDetails?> GetTournamentByIdAsync(int id, bool trackChanges = false)
        {
            return await FindByCondition(t => t.Id == id, trackChanges).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TournamentDetails>> GetTournamentsAsync(
            TournamentQueryParams queryParams,
            bool trackChanges = false)
        {
            var query = FindAll(trackChanges);
            if (queryParams.IncludeGames)
                query = query.Include(t => t.Games);

            // Om det t.ex finns 20 sidor och pageNumber har 2 och pageSize är 3.
            // då blir pagesToSkip = 6...
            uint pagesToSkip = (queryParams.PageNumber - 1) * queryParams.PageSize;

            // Och vi plockar ut 7, 8, 9 från DB.
            query = query.Skip((int)pagesToSkip).Take((int)queryParams.PageSize);

            return await query.ToListAsync();
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