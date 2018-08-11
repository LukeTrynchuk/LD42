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
        [SerializeField]
        private float gridSize;
        private bool m_isPlacing = false;
        private GameObject m_currentPlacingEntityObject;
        private Camera m_rayCamera;
        private ServiceReference<IInputService> inputService = new ServiceReference<IInputService>();
        private ServiceReference<IGameboardService> gameboardService = new ServiceReference<IGameboardService>();
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
            m_currentPlacingEntityObject.GetComponent<Entity>().SetIsPlacing(IsPlacing);
        }

        public void SetPlacingActive(bool value)
        {
            m_isPlacing = value;
            m_currentPlacingEntityObject?.SetActive(m_isPlacing);
            m_currentPlacingEntityObject.GetComponent<Entity>().SetIsPlacing(true);
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

            MoveCurrentObject();
            PlaceCurrentObject();
        }

        private void MoveCurrentObject()
        {
            RaycastHit hit;
            Ray ray = m_rayCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                m_currentPlacingEntityObject.transform.position = SnapToGrid(hit.point);
            }
        }
        private Vector3 SnapToGrid(Vector3 initialPosition)
        {
            initialPosition.x -= initialPosition.x % gridSize;
            initialPosition.z -= initialPosition.z % gridSize;
            return initialPosition;
        }

        private void PlaceCurrentObject()
        {
            if (inputService.Reference.IsConfirmButtonDown())
            {
                if (!ValidePosition(m_currentPlacingEntityObject.transform.position)) return;
                GameObject placedObject = Instantiate(m_currentPlacingEntityObject);
                placedObject.transform.position = m_currentPlacingEntityObject.transform.position;
                placedObject.transform.rotation = m_currentPlacingEntityObject.transform.rotation;
                gameboardService.Reference.RegisterEntity(placedObject.GetComponent<Entity>());
                placedObject.GetComponent<Entity>().SetIsPlacing(false);
            }
        }
        private bool ValidePosition(Vector3 position)
        {
            return gameboardService.Reference.IsValidePosition(position, gridSize);
        }

        #endregion
    }
}
