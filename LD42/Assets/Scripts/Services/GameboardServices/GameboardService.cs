using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Gameboard;
using RoboCorp.Services.General;

namespace RoboCorp.Services
{
    /// <summary>
    /// The Game board service is responsible for
    /// keeping track of the entities on the gameboard. 
    /// Other classes will ask if a position on the
    /// gameboard is a valid position for a new 
    /// entity.
    /// 
    /// TODO : Come up with a better name.
    /// </summary>
    public class GameboardService : RegisterManager<Entity>, IGameboardService 
    {
        #region Private Variables
        private ServiceReference<IPlacementService> m_placementService = new ServiceReference<IPlacementService>();
        private float GridSize => m_placementService.Reference.GridSize;
        #endregion

        #region MainMethods
        void Awake() => RegisterService();
        public void RegisterEntity(Entity entity) => Register(entity);
        public void UnregisterEntity(Entity entity) => Unregister(entity);
        public void RegisterService() => ServiceLocator.Register<IGameboardService>(this);

        public bool IsValidePosition(Vector3 position)
        {
            Entity entity = GetEntityAt(position);
            return (entity == null);
        }

        public Entity GetEntityAt(Vector3 position)
        {
            foreach(Entity e in m_valueList)
            {
                if (Vector3.Distance(position, e.gameObject.transform.position) < GridSize * 0.9f) return e;
            }

            return null;
        }
        #endregion
    }
}

