using UnityEngine;

namespace RoboCorp.Services
{
    /// <summary>
    /// An IPlace interface is an
    /// interface that all placement
    /// strategies must implement. 
    /// </summary>
    public interface IPlace
    {
        void SetGridSize(float gridSize);
        void Move(ref GameObject PlaceObject);
        void Place(GameObject PlaceObject);
    }
}
