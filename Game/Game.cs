using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Game.Interfaces;
using System.ComponentModel;

namespace Game
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class Game : Actor, IGame
    {
        private ActorState _state;

        /// <summary>
        /// Initializes a new instance of Game
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public Game(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        } 

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");
            if (_state == null)
            {
                _state = new ActorState
                {
                    Board = new int[9],
                    Winner = string.Empty,
                    Players = new List<Tuple<long, string>>(),
                    NextPlayerIndex = 0,
                    NumberOfMoves = 0
                };
            }
            return Task.FromResult(true);
        }

        [ReadOnly(true)]
        Task<int[]> IGame.GetGameBoardAsync()
        {
            throw new NotImplementedException();
        }

        [ReadOnly(true)]
        Task<string> IGame.GetWinnderAsync()
        {
            throw new NotImplementedException();
        }

        Task<bool> IGame.JoinGameAsync(long playerId, string playerName)
        {
            if (_state.Players.Count >= 2 || _state.Players.FirstOrDefault(player => player.Item2 == playerName ) != null)
                return Task.FromResult(false);
            _state.Players.Add(new Tuple<long, string>(playerId, playerName));
            return Task.FromResult(true);
        }

        Task<bool> IGame.MakeMoveAsycn(long playerId, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
