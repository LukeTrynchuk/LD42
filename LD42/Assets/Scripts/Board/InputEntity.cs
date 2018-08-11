using RoboCorp.Core.Services;
using RoboCorp.Services;

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
        #endregion

        #region Main Methods
        public override void Tick()
        {
            TickOutputs();
            Animate();
        }

        public override void Animate() { }

        public void Awake()
        {
            m_tickService.AddRegistrationHandle(OnTickServiceRegistered);
        }
        #endregion

        #region Utility Methods
        void OnTickServiceRegistered()
        {
            m_tickService.Reference?.Register(this);    
        }
        #endregion
    }
}
