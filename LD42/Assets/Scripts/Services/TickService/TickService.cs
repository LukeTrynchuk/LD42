using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Services.General;
using RoboCorp.Gameboard;
using System;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    /// <summary>
    /// Tick service is responsible for keeping
    /// a list of entities and calling their
    /// Tick method every X number of seconds.
    /// </summary>
    public class TickService : RegisterManager<InputEntity> , ITickService
    {
        #region Public Variables
        public event Action OnTick;
        public TickState State => m_state;
        #endregion

        #region Private Variables
        [SerializeField]
        private float m_tickLength;

        private float m_currentTime = 0f;

        TickState m_state = TickState.TICK_DISABLED;
        #endregion

        #region Main Methods
        void Awake() => RegisterService();

        void Update()
        {
            UpdateTimer();
            Tick();
        }

        public void DisableTick() => m_state = TickState.TICK_DISABLED;
		public void EnableTick()  => m_state = TickState.TICK_ENABLED;	

        public void RegisterService() => ServiceLocator.Register<ITickService>(this);
        #endregion

        #region Utility Methods
        private void TickInputCollection()
        {
            foreach (InputEntity entity in m_valueList) entity.Tick();
        }

        private void Tick()
        {
            if (!CanTick()) return;

            m_currentTime = 0f;
            TickInputCollection();
            OnTick?.Invoke();
        }

        private bool CanTick()
        {
            if (m_state != TickState.TICK_ENABLED) return false;
            if (m_currentTime < m_tickLength) return false;
            return true;
        }

        private void UpdateTimer() => m_currentTime += Time.unscaledDeltaTime;
        #endregion
    }
}
