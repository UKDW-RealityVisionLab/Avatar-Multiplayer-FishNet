using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // look at File > Build Scene to find the sceneId
    public void ChangeSceneById(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
