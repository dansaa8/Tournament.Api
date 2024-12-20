﻿using Microsoft.EntityFrameworkCore;
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
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(TournamentApiContext context) : base(context)
        {
        }

        public async Task<Game?> GetGameByIdAsync(int id, bool trackChanges = false)
        {
            return await FindByCondition(g => g.Id.Equals(id), trackChanges).FirstOrDefaultAsync();
        }

        public async Task<PagedList<Game>> GetGamesAsync(
            GameQueryParameters queryParams,
            bool trackChanges = false)
        {
            var games = FindAll(trackChanges);
            
            return await PagedList<Game>.CreateAsync(games, queryParams.PageNumber, queryParams.PageSize);
        }
        
        public async Task<int> CountGamesAsync() => await FindAll().CountAsync();

        // public async Task<IEnumerable<Game>> GetGamesByTitle(string title)
        // {
        //     return await _context.Games
        //         .Where(game => game.Title == title).ToListAsync();
        // }

        //
        //     public async Task<bool> AnyAsync(int id)
        //     {
        //         return await _context.Games.AnyAsync(game => game.Id == id);
        //     }
        //
        //     public void Add(Game game)
        //     {
        //         _context.Games.Add(game);
        //     }
        //
        //     public void Remove(Game game)
        //     {
        //         _context.Games.Remove(game);
        //     }
        //
        //     public void Update(Game game)
        //     {
        //         _context.Games.Update(game);
        //     }
        // }
    }
}