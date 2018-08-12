using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Gameboard;

namespace RoboCorp.Gameboard
{
    /// <summary>
    /// A straight conveyer entity is responsible
    /// for moving any resources it has on it to
    /// a forward output. 
    /// </summary>
    public class StraightConveyerEntity : Entity
    {

        #region MainMethods
        public override void Animate() {}

        public override void Tick() 
        {
            TransportResource();
            Animate();
        }

		public override void TransportResource()
		{
            if (m_forwardOutput == null) return;
            resourceContainer.TransferResource(m_forwardOutput.ResourcesContainer);
            m_forwardOutput.TransportResource();
		}
		#endregion
	}
}
