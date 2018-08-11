using System;
using System.Collections;
using System.Collections.Generic;
using RoboCorp.Gameboard;
using UnityEngine;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    /// <summary>
    /// The placement service is responsible for placing
    /// down entities on the gameboard. 
    /// </summary>
    public class PlacementService : MonoBehaviour, IPlacementService
    {
        #region Public Variables
        public bool IsPlacing => m_isPlacing;

        public event Action<Entity> OnEntityPlaced;
        #endregion

        #region Private Variables
        private bool m_isPlacing = false;
        private GameObject m_currentPlacingEntityObject;
        #endregion

        #region Main Methods
        void Awake() => RegisterService();

        public void SetCurrentPlacingEntity(GameObject entity) => m_currentPlacingEntityObject = entity;

        public void SetPlacingActive(bool value) => m_isPlacing = value;

        void Update()
        {
            if (!IsPlacing) return;
            AttemptPlace();
        }
        #endregion

        #region Utility Methods
        public void RegisterService() => ServiceLocator.Register<IPlacementService>(this);

        private void AttemptPlace()
        {
            
        }
        #endregion
    }
}
