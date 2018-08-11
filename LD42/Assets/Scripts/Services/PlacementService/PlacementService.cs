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
        private Camera m_rayCamera;
        #endregion

        #region Main Methods
        void Awake() => RegisterService();

        public void SetCurrentPlacingEntity(GameObject entity)
        {
            if(m_currentPlacingEntityObject != null)
            {
                Destroy(m_currentPlacingEntityObject);
            }

            m_currentPlacingEntityObject = Instantiate(entity);
            m_currentPlacingEntityObject.SetActive(IsPlacing);
        }

        public void SetPlacingActive(bool value)
        {
            m_isPlacing = value;
            m_currentPlacingEntityObject?.SetActive(m_isPlacing);
        }

        void Update()
        {
            if (!IsPlacing) return;
            AttemptPlace();
        }
        public void RegisterCamera(Camera cam) => m_rayCamera = cam;
        public void UnregisterCamera(Camera cam)
        {
            if(cam == m_rayCamera)
            {
                m_rayCamera = null;
            }
        }
        #endregion

        #region Utility Methods
        public void RegisterService() => ServiceLocator.Register<IPlacementService>(this);

        private void AttemptPlace()
        {
            if (m_rayCamera == null) return;
            if (m_currentPlacingEntityObject == null) return;

            Debug.Log("I made it Mom");

            RaycastHit hit;
            Ray ray = m_rayCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit))
            {
                m_currentPlacingEntityObject.transform.position = hit.point;
            }
        }
        #endregion
    }
}
