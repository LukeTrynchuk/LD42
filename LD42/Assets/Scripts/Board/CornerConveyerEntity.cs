using UnityEngine;

namespace RoboCorp.Gameboard
{
    /// <summary>
    /// The Corner conveyer entity is a corner
    /// in a conveyer.
    /// </summary>
    public class CornerConveyerEntity : Entity
    {
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
