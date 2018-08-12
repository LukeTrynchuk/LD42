using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceScene : MonoBehaviour {

    public void AdvanceTheScene()
    {
        int sceneIndex = GetCurrentSceneIndex();
        sceneIndex++;
        AttemptSceneLoad(sceneIndex);
    }
    private int GetCurrentSceneIndex()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.buildIndex;
    }

    private void AttemptSceneLoad(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
