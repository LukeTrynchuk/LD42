using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoboCorp.Scenes
{

    /// <summary>
    /// The Advance Scene component 
    /// will advance the scene
    /// to the next scene in the 
    /// scene build list.
    /// </summary>
    public class AdvanceScene : MonoBehaviour
    {
        #region Main Methods
        public void Advance()
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
