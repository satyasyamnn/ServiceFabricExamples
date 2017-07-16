﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace Game.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IGame : IActor
    {
        Task<bool> JoinGameAsync(long playerId, string playerName);
        Task<int[]> GetGameBoardAsync();
        Task<string> GetWinnderAsync();
        Task<bool> MakeMoveAsycn(long playerId, int x, int y);
    }
}
