﻿using Tournament.Core.Entities;

namespace Tournament.Core.Contracts
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetGamesAsync(bool trackChanges = false);

        // Task<IEnumerable<Game>> GetGamesByTitle(string title);
        Task<Game?> GetGameByIdAsync(int id, bool trackChanges = false);
        // Task<bool> AnyAsync(int id);
        // void Add(Game game);
        // void Update(Game game);
        // void Remove(Game game);
    }
}