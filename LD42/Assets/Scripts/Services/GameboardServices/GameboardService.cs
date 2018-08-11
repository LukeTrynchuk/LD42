using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Gameboard;

namespace RoboCorp.Services
{
    public class GameboardService : MonoBehaviour, IGameboardService {

        private List<Entity> entityList = new List<Entity>();
        #region MainMethods

        void Awake() => RegisterService();
        public void RegisterEntity(Entity entity)
        {
            entityList.Add(entity);
        }

        public void RegisterService()
        {
            ServiceLocator.Register<IGameboardService>(this);
        }

        public bool IsValidePosition(Vector3 position, float radius)
        {
            foreach(Entity e in entityList)
            {
                if (Vector3.Distance(position, e.gameObject.transform.position) < radius *0.9f) return false;
            }
            return true;
        }


        #endregion

    }
}

