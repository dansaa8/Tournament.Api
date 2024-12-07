﻿using Tournament.Data.Repositories;

namespace Tournament.Core.Contracts
{
    public interface IUnitOfWork
    {
        ITournamentRepository TournamentRepository { get; }
        IGameRepository GameRepository { get; }
        Task CompleteAsync();
    }
}
