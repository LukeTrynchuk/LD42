using System;
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

        [SerializeField]
        private float m_rotationTimeThreshhold;

        private bool m_isPlacing = false;
        private GameObject m_currentPlacingEntityObject;
		private IPlace m_placer = new RaycastPlacementStrategy();
        private float m_timeSinceRotation = 0f;

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
            m_timeSinceRotation += Time.deltaTime;

            if (!IsPlacing) return;
            AttemptPlace();
        }
        #endregion

        #region Utility Methods
        public void RegisterService() => ServiceLocator.Register<IPlacementService>(this);

        private void AttemptPlace()
        {
            if (m_currentPlacingEntityObject == null) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;

            m_placer.Move(ref m_currentPlacingEntityObject);
            PlaceCurrentObject();
        }

        private void PlaceCurrentObject()
        {
            if (!m_inputService.Reference.IsConfirmButtonDown()) return;
            if (!ValidatePosition(m_currentPlacingEntityObject.transform.position)) return;

            m_placer.Place(m_currentPlacingEntityObject);
        }

        private bool ValidatePosition(Vector3 position)
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
            if (m_timeSinceRotation < m_rotationTimeThreshhold) return;

            m_timeSinceRotation = 0f;
            rotationAmount = Mathf.Clamp(rotationAmount, -1, 1);
            float rotateRadians = Mathf.Deg2Rad * (90f * (float)rotationAmount);
            m_currentPlacingEntityObject.transform.RotateAround(Vector3.up, rotateRadians);
        }
        #endregion
    }
}
