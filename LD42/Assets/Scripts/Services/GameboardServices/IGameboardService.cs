using UnityEngine;
using RoboCorp.Gameboard;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    /// <summary>
    /// A gameboard service is responsible for
    /// keeping track of all entites on the
    /// game board. Other services / classes
    /// will ask wether certain positions on 
    /// the gameboard are valid positions to 
    /// place new entites at. 
    /// </summary>
    public interface IGameboardService : IService 
    {
        void RegisterEntity(Entity entity);
        void UnregisterEntity(Entity entity);

        bool IsValidePosition(Vector3 position);

        Entity GetEntityAt(Vector3 position);
    }
}

