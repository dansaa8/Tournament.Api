using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Contracts;
using Tournament.Core.Dto.Queries;
using Tournament.Core.Entities;
using Tournament.Core.Req;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class TournamentRepository(TournamentApiContext context)
        : RepositoryBase<TournamentDetails>(context), ITournamentRepository
    {
        public async Task<TournamentDetails?> GetTournamentByIdAsync(
            int id, bool includeGames = false, bool trackChanges = false)
        {
            IQueryable<TournamentDetails> query = FindByCondition(t => t.Id == id, trackChanges);

            if (includeGames)
            {
                query = query.Include(t => t.Games);
            }
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<PagedList<TournamentDetails>> GetTournamentsAsync(
            TournamentQueryParameters queryParameters,
            bool trackChanges = false)
        {
            var tournaments = queryParameters.IncludeGames
                ? FindAll(trackChanges).Include(t => t.Games)
                : FindAll();

            return await PagedList<TournamentDetails>.CreateAsync(tournaments, queryParameters.PageNumber,
                queryParameters.PageSize);
        }

        public async Task<int> GetTournamentsCountAsync() => await FindAll().CountAsync();


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