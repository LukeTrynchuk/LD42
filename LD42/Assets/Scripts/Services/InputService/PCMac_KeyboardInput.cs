using System;
using UnityEngine;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    /// <summary>
    /// The PC Mac Keyboard input service is responsible
    /// for detecting keyboard input from a PC or mac.
    /// </summary>
    public class PCMac_KeyboardInput : MonoBehaviour, IInputService
    {
        #region Public Variables
        public event Action OnConfirmButton;
        public event Action<int> OnRotationButton;
        #endregion

        #region Private Variables
		[SerializeField]
        private float m_scrollSensitivity;
  
        private bool m_confirmDown = false;
        private int m_rotationAmount = 0;
        private const string SCROLL_WHEEL_AXIS = "Mouse ScrollWheel";
        #endregion

        #region Main Methods
        public bool IsConfirmButtonDown()           => m_confirmDown;
		void Awake()                                => RegisterService();
        public void RegisterService()               => ServiceLocator.Register<IInputService>(this);
		public int RotationButtonDown()             => m_rotationAmount;
		public Vector3 GetPointerScreenPosition()   => Input.mousePosition;

        void Update()
        {
            ResetButtonValues();
            CheckForConfirmButton();
            CheckForRotationButton();
        }
        #endregion

        #region Utility Methods
        private void CheckForConfirmButton()
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_confirmDown = true;
                OnConfirmButton?.Invoke();
            }
        }

        private void CheckForRotationButton()
        {
            if(UserScrolled())
            {
                m_rotationAmount = (int)(Input.GetAxis(SCROLL_WHEEL_AXIS) / m_scrollSensitivity);
                OnRotationButton?.Invoke(m_rotationAmount);
            }
        }

        private bool UserScrolled()
        {
            return Mathf.Abs(Input.GetAxis(SCROLL_WHEEL_AXIS)) > m_scrollSensitivity;
        }

        private void ResetButtonValues()
        {
            m_confirmDown = false;
            m_rotationAmount = 0;
        }
        #endregion
    }
}
