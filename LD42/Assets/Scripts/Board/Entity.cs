using UnityEngine;

namespace RoboCorp.Gameboard
{
    /// <summary>
    /// The entity base class represents 
    /// any kind of object that can be
    /// placed, moved and manipulated 
    /// on the game board.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        #region Protected Variables
        protected Entity m_backInput = null;
        protected Entity m_forwardInput = null;
        protected Entity m_leftInput = null;
        protected Entity m_rightInput = null;

        protected Entity m_backOutput = null;
        protected Entity m_forwardOutput = null;
        protected Entity m_leftOutput = null;
        protected Entity m_rightOutput = null;
        #endregion

        #region PrivateVariables
        [SerializeField]
        private GameObject m_inputHelper;

        [SerializeField]
        private GameObject m_outputHelper;

        [SerializeField]
        private GameObject m_placementHelper;

        [SerializeField]
        private InputOutputSetting m_inputOutputSettings;

        private bool IsPlacing = false;
        #endregion
        #region Main Methods
        public abstract void Tick();
        public abstract void Animate();

        public virtual void TickOutputs()
        {
            if (m_backOutput != null)    m_backInput.Tick();
            if (m_forwardOutput != null) m_backInput.Tick();
            if (m_leftOutput != null)    m_backInput.Tick();
            if (m_rightOutput != null)   m_backInput.Tick();
        }

        public virtual void SetIsPlacing(bool placingValue)
        {
            IsPlacing = placingValue;
            m_placementHelper.SetActive(IsPlacing);
            m_inputHelper.SetActive(IsPlacing);
            m_outputHelper.SetActive(IsPlacing);
        }
        #endregion
    }
}
