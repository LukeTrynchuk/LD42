using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Services;

namespace RoboCorp.Gameboard
{
    /// <summary>
    /// The output entity is a gameboard entity
    /// that given an input will sell for
    /// money.
    /// </summary>
    public class OutputEntity : Entity
    {
        #region Private Variables
        private ServiceReference<ITickService> m_tickService 
                    = new ServiceReference<ITickService>();
        #endregion

        #region Main Methods
        public override void Animate() {}

        public override void Tick()
        {
            Animate();

            if (resourceContainer.OldResourceList.Count > 0 ||
               resourceContainer.NewResourceList.Count > 0)
            {
                RemoveResources();
                DestroyResource();
            }
        }

		private void OnEnable()
		{
            m_tickService.AddRegistrationHandle(HandleTickServiceRegistered);
		}

		private void OnDisable()
		{
			if(m_tickService.isRegistered())
            {
                m_tickService.Reference.OnTick -= Tick;
            }
		}
		#endregion

		#region Utility Methods
		private void RemoveResources()
        {
            for (int i = resourceContainer.NewResourceList.Count - 1; i >= 0; i--)
            {
                Destroy(resourceContainer.NewResourceList[i]);
            }
            resourceContainer.NewResourceList.Clear();
        }

        private void HandleTickServiceRegistered()
        {
            m_tickService.Reference.OnTick -= Tick;
            m_tickService.Reference.OnTick += Tick;
        }
        #endregion
    }
}
