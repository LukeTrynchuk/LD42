using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboCorp.Gameboard
{
    /// <summary>
    /// The Processor Entity is a processor 
    /// that will process a resource into something
    /// of higher value.
    /// </summary>
    public class ProcessorEntity : Entity
    {
        #region Private Variables
        #endregion

        #region Main Methods
        public override void Animate() {}

        public override void Tick()
        {
            TransportResource();
            Animate();
        }

		public override void TransportResource()
		{
            if(m_forwardOutput == null)
            {
                if (resourceContainer.OldResourceList.Count <= 0) return;
                DestroyResource();
                return;
            }

            resourceContainer.TransferResource(m_forwardOutput.ResourcesContainer);
            m_forwardOutput.TransportResource();
		}
		#endregion
	}
}
