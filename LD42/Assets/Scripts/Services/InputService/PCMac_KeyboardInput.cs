using System;
using System.Collections;
using System.Collections.Generic;
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
        #endregion

        #region Private Variables
        private bool m_confirmDown = false;
        #endregion

        #region Main Methods
        public bool IsConfirmButtonDown() => m_confirmDown;

        public void RegisterService() => ServiceLocator.Register<IInputService>(this);

        void Awake() => RegisterService();

        void Update()
        {
            ResetButtonValues();

            CheckForConfirmButton();
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

        private void ResetButtonValues()
        {
            m_confirmDown = false;
        }
        #endregion
    }
}
