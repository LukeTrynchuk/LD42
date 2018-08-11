using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Services;

namespace RoboCorp.General
{
    /// <summary>
    /// The Switch placement service is placing 
    /// component will switch the value of IsPlacing
    /// to the opposite value. 
    /// </summary>
    public class SwitchPlacementServiceIsPlacing : MonoBehaviour
    {
        #region Private Variables
        private ServiceReference<IPlacementService> m_placementService
                        = new ServiceReference<IPlacementService>();
        #endregion

        #region Main Methods
        public void SwitchIsPlacingValue()
        {
            if (!m_placementService.isRegistered()) return;

            bool currentValue = m_placementService.Reference.IsPlacing;
            m_placementService.Reference.SetPlacingActive(!currentValue);
        }
        #endregion
    }
}
