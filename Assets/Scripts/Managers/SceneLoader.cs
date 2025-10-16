using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public bool CueLoadScene { get; set; }
    public string CurrentSceneName { get; private set; }

    // Private References
    private string sceneToLoad;


    void Awake()
    {
        //SceneManager.LoadScene
    }

    public void GetCurrentScene()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;
    }

    public void RestartScene()
    {
        // !!!!!!
        //Time.timeScale = 1f;

        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    
    public void LoadSelectedScene(SceneName sceneName)
    {
        switch(sceneName.ToString())
        {
            case SceneName.MainMenu:
                {

                }
                break;

            case SceneName.Normal:
                {

                }
                break;

        }
    }

    public void PreLoadSelectedScene(string selectedScene)
    {
        sceneToLoad = selectedScene;

        StartCoroutine(PreLoadAsyncScene(sceneToLoad));

    }

    
    IEnumerator PreLoadAsyncScene(string sceneToLoad)
    {

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        asyncOperation.allowSceneActivation = false;

        while(!asyncOperation.isDone)
        {
            // for debug
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            Debug.Log("Scene Loading Progress: " + progress);

            if(asyncOperation.progress >= 0.9f)
            {
                Debug.Log("Scene " + sceneToLoad + " is Ready");

                //asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        
    }
}
