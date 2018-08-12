using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Services;

namespace RoboCorp.Resources
{
    /// <summary>
    /// A resource is a class that contains all
    /// the resources on one entity. 
    /// </summary>
    public class ResourceContainer
    {
        #region Public Variables
        public List<Resource> NewResourceList => m_newResourceList;
        public List<Resource> OldResourceList => m_oldResourceList;
        #endregion

        #region Private Variables
        private List<Resource> m_newResourceList = new List<Resource>();
        private List<Resource> m_oldResourceList = new List<Resource>();
        private Vector3 m_transportPoint;
        private ServiceReference<ITickService> m_tickService = new ServiceReference<ITickService>();
        #endregion

        #region Main Methods
        public ResourceContainer(Vector3 startPosition)
        {
            m_transportPoint = startPosition;
            m_tickService.AddRegistrationHandle(RegisterTick);
        }

        ~ResourceContainer()
        {
            if (m_tickService.isRegistered())
            {
                m_tickService.Reference.OnTick -= OnTick;
            }
        }

        public void TransferResource(ResourceContainer container)
        {
            if (!(m_oldResourceList.Count > 0)) return;
            container.TransferTo(m_oldResourceList[0]);
            m_oldResourceList.RemoveAt(0);
        }

        public void TransferTo(Resource resource)
        {
            resource.TranslatePoint = m_transportPoint;
            m_newResourceList.Add(resource);
        }
        #endregion

        #region Utility Methods
        private void RegisterTick()
        {
            m_tickService.Reference.OnTick -= OnTick;
            m_tickService.Reference.OnTick += OnTick;
        }

        private void OnTick()
        {
            foreach (Resource resource in m_newResourceList)
            {
                m_oldResourceList.Add(resource);
            }

            for (int i = m_newResourceList.Count - 1; i >= 0; i--)
                m_newResourceList.RemoveAt(i);
        }
        #endregion
    }
}
