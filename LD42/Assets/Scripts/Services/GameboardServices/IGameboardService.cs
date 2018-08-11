using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Gameboard;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    public interface IGameboardService : IService {

        void RegisterEntity(Entity entity);
        bool IsValidePosition(Vector3 position, float radius);
    }
}

