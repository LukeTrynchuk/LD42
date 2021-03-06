﻿using RoboCorp.Core.Services;
using RoboCorp.Services;
using UnityEngine;
using RoboCorp.Resources;

namespace RoboCorp.Gameboard
{
    /// <summary>
    /// Input Entity is a gameboard entity that
    /// gets the raw resources.
    /// </summary>
    public class InputEntity : Entity
    {
        #region Private Variables
        private ServiceReference<ITickService> m_tickService
                = new ServiceReference<ITickService>();
        private GameObject m_currentResource;
        #endregion

        #region Main Methods
        public override void Tick()
        {
            TransportResource();
            GenerateResource();
            TickOutputs();
            Animate();
        }
        public override void TransportResource()
        {
            if (m_forwardOutput == null) return;
            resourceContainer.TransferResource(m_forwardOutput.ResourcesContainer);
            m_forwardOutput.TransportResource();
        }
        public void SetCurrentResource(GameObject resource)
        {
            m_currentResource = resource;
        }
        public override void Animate() { }

        public override void Awake()
        {
            base.Awake();
            m_tickService.AddRegistrationHandle(OnTickServiceRegistered);
        }
        #endregion

        #region Utility Methods
        void OnTickServiceRegistered()
        {
            m_tickService.Reference?.Register(this);    
        }
        private void GenerateResource()
        {
            if (m_currentResource == null) return;
            if (m_forwardOutput == null) return;

            GameObject newResource = Instantiate(m_currentResource);
            newResource.transform.position = m_transportTransform.position;
            newResource.GetComponent<Resource>().SetTargetPosition(m_forwardOutput.TransportTransform.position);
            resourceContainer.TransferTo(newResource.GetComponent<Resource>());
        }
        #endregion
    }
}
