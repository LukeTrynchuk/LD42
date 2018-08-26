using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Services;

namespace RoboCorp.Audio
{
    /// <summary>
    /// The Play Audio On Start component
    /// can be attached to any object
    /// that should request an audio
    /// file be played when this object
    /// is first created.
    /// </summary>
    public class PlayAudioOnStart : MonoBehaviour
    {
        #region Private Variables
        [SerializeField]
        private string m_audioRequestID;

        private ServiceReference<ISoundService> m_soundService = new ServiceReference<ISoundService>();
        #endregion

        #region Main Methods
        void Start()
        {
            m_soundService.AddRegistrationHandle(HandleSoundServiceRegistered);
        }

        private void HandleSoundServiceRegistered()
        {
            m_soundService.Reference.Play(m_audioRequestID);
        }
        #endregion  
    }
}
