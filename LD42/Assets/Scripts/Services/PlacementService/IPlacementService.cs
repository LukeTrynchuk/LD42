using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Gameboard;

namespace RoboCorp.Services
{
    /// <summary>
    /// IPlacement Service is a contract for all
    /// placement services. A placement service is
    /// responsible for placing a gameboard object
    /// onto the gameboard.
    /// </summary>
    public interface IPlacementService : IService
    {
        event System.Action<Entity> OnEntityPlaced;

        bool IsPlacing { get; }

        void SetCurrentPlacingEntity(GameObject entity);
        void SetPlacingActive(bool value);
        void RegisterCamera(Camera cam);
        void UnregisterCamera(Camera cam);
    }
}
