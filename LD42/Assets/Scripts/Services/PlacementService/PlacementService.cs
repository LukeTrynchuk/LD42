using System;
using System.Collections;
using System.Collections.Generic;
using RoboCorp.Gameboard;
using UnityEngine;
using UnityEngine.EventSystems;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    /// <summary>
    /// The placement service is responsible for placing
    /// entities on the gameboard. 
    /// </summary>
    public class PlacementService : MonoBehaviour, IPlacementService
    {
        #region Public Variables
        public bool IsPlacing => m_isPlacing;
        public float GridSize => m_gridSize;

        public event Action<Entity> OnEntityPlaced;
        #endregion

        #region Private Variables
        [SerializeField]
        private float m_gridSize;
        private bool m_isPlacing = false;
        private GameObject m_currentPlacingEntityObject;
        private Camera m_rayCamera;
        private ServiceReference<IInputService> m_inputService = new ServiceReference<IInputService>();
        private ServiceReference<IGameboardService> m_gameboardService = new ServiceReference<IGameboardService>();
        #endregion

        #region Main Methods
        void Awake() => RegisterService();
        void Start() => m_inputService.AddRegistrationHandle(OnRegisterInput);

        void OnRegisterInput()
        {
            m_inputService.Reference.OnRotationButton -= OnUserRotated;
            m_inputService.Reference.OnRotationButton += OnUserRotated;
        }

        void OnUserRotated(int rotationAmount)
        {
            if (!IsPlacing) return;
            if (m_currentPlacingEntityObject == null) return;

            RotatePlacingObjectByAmount(rotationAmount);
        }

        public void SetCurrentPlacingEntity(GameObject entity)
        {
            DestroyCurrentPlacingObject();

            m_currentPlacingEntityObject = Instantiate(entity);
            m_currentPlacingEntityObject.SetActive(IsPlacing);
            m_currentPlacingEntityObject.GetComponent<Entity>().SetIsPlacing(IsPlacing);
        }

        public void SetPlacingActive(bool value)
        {
            m_isPlacing = value;
            m_currentPlacingEntityObject?.SetActive(m_isPlacing);
            m_currentPlacingEntityObject?.GetComponent<Entity>()?.SetIsPlacing(true);
        }

        void Update()
        {
            if (!IsPlacing) return;
            AttemptPlace();
        }
       
        public void RegisterCamera(Camera cam) => m_rayCamera = cam;

        public void UnregisterCamera(Camera cam)
        {
            if (cam != m_rayCamera) return;
            m_rayCamera = null;
        }

        #endregion

        #region Utility Methods
        public void RegisterService() => ServiceLocator.Register<IPlacementService>(this);

        private void AttemptPlace()
        {
            if (m_rayCamera == null) return;
            if (m_currentPlacingEntityObject == null) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;

            MoveCurrentObject();
            PlaceCurrentObject();
        }

        private void MoveCurrentObject()
        {
            RaycastHit hit;
            Ray ray = m_rayCamera.ScreenPointToRay(m_inputService.Reference.GetPointerScreenPosition());

            if (Physics.Raycast(ray, out hit))
            {
                m_currentPlacingEntityObject.transform.position = SnapToGrid(hit.point);
            }
        }

        private Vector3 SnapToGrid(Vector3 initialPosition)
        {
            initialPosition.x -= initialPosition.x % m_gridSize;
            initialPosition.z -= initialPosition.z % m_gridSize;
            return initialPosition;
        }

        private void PlaceCurrentObject()
        {
            if (!m_inputService.Reference.IsConfirmButtonDown()) return;
            if (!ValidePosition(m_currentPlacingEntityObject.transform.position)) return;

            Entity entity = InstantiatePlaceObject().GetComponent<Entity>();
            entity.Setup();
        }

        private GameObject InstantiatePlaceObject()
        {
            GameObject placedObject = Instantiate(m_currentPlacingEntityObject);
            placedObject.transform.position = m_currentPlacingEntityObject.transform.position;
            placedObject.transform.rotation = m_currentPlacingEntityObject.transform.rotation;
            return placedObject;
        }

        private bool ValidePosition(Vector3 position)
        {
            return m_gameboardService.Reference.IsValidePosition(position, m_gridSize);
        }

        private void DestroyCurrentPlacingObject()
        {
            if (m_currentPlacingEntityObject != null)
            {
                Destroy(m_currentPlacingEntityObject);
            }
        }

        private void RotatePlacingObjectByAmount(int rotationAmount)
        {
            rotationAmount = Mathf.Clamp(rotationAmount, -1, 1);
            float rotateRadians = Mathf.Deg2Rad * (90f * (float)rotationAmount);
            m_currentPlacingEntityObject.transform.RotateAround(Vector3.up, rotateRadians);
        }
        #endregion
    }
}
