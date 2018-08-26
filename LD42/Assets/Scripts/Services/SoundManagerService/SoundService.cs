using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    /// <summary>
    /// The Sound Manager is responsible for handeling 
    /// audio requests. The Sound Service must be able
    /// to find an available audio sounce and play 
    /// the requested audio file.
    /// </summary>
    public class SoundService : MonoBehaviour, ISoundService
    {
        #region Private Variables
        [SerializeField]
        private List<ScritableAudioObject> m_audioFileList = new List<ScritableAudioObject>();

        private List<AudioSource> m_sourceList = new List<AudioSource>();
        #endregion

        #region Main Methods
        public void Play(string id)
        {
            ScritableAudioObject audioFile = FindObject(id);
            AudioSource audioSource = FindAvailibleAudiSource();
            if (audioFile == null || audioSource == null) return;

            PlayAudioOnSource(audioFile, audioSource);
        }

        public void RegisterService() => ServiceLocator.Register<ISoundService>(this);

        void Start()
        {
            RegisterService();
            m_sourceList = gameObject.GetComponentsInChildren<AudioSource>().ToList();
        }
        #endregion

        #region Utility Methods
        private ScritableAudioObject FindObject(string id)
        {
            for (int i = 0; i < m_audioFileList.Count; i++)
            {
                if (id.Equals(m_audioFileList[i].id)) return m_audioFileList[i];
            }
            return null;
        }

        private AudioSource FindAvailibleAudiSource()
        {
            for (int i = 0; i < m_sourceList.Count; i++)
            {
                if (!m_sourceList[i].isPlaying) return m_sourceList[i];
            }

            return null;
        }

        private void PlayAudioOnSource(ScritableAudioObject audio, AudioSource source)
        {
            source.clip = audio.clip;
            source.Play();
        }
        #endregion
    }
}
