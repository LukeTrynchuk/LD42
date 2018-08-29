using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    /// <summary>
    /// The Entity Manager is a service
    /// that contains all the entities
    /// in the game. Other services and 
    /// classes may ask for the different
    /// entites in a game.
    /// </summary>
    public class EntityManager : MonoBehaviour, IEntityManager
    {
        #region Private Variables
        [SerializeField]
        private EntityInstance[] m_entityInstanceList;
        #endregion

        #region Main Methods
        public EntityInstance[] GetEntities() => m_entityInstanceList;

        public void RegisterService()
        {
            ServiceLocator.Register<IEntityManager>(this);
        }

        void Start() => RegisterService();
        #endregion
    }
}
