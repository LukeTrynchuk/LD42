using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoboCorp.Scenes
{
    /// <summary>
    /// The Preload Scene advancer will advance
    /// to the next available scene after all
    /// the services in the preload scene have
    /// been registered.
    /// </summary>
    public class PreloadSceneAdvancer : MonoBehaviour
    {
        #region Main Methods
        private void LateUpdate()
        {
            int sceneIndex = GetCurrentSceneIndex();
            sceneIndex++;
            AttemptSceneLoad(sceneIndex);
        }
        #endregion

        #region Utility Methods
        private int GetCurrentSceneIndex()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            return currentScene.buildIndex;
        }

        private void AttemptSceneLoad(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        #endregion
    }
}
