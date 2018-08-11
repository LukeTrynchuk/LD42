using UnityEngine;
using RoboCorp.Gameboard;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    /// <summary>
    /// ITick Service is a contract that all
    /// tick services must implement. A tick
    /// service is responsible for sending out 
    /// the ticks of the game.
    /// </summary>
    public interface ITickService : IService
    {
        event System.Action OnTick;

        TickState State { get; }

        void Register(Entity entity);
        void Unregister(Entity entity);

        void DisableTick();
        void EnableTick();
    }

    public enum TickState
    {
        TICK_ENABLED,
        TICK_DISABLED
    }
}
