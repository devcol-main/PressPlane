using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public bool CueLoadScene { get; set; }
    public static string CurrentSceneName { get; private set; }
    public static SceneType CurrentSceneType { get; private set; }

    // Private References
    private string sceneToLoad;

    private bool isLoadSceneReady = false;
    //private 

    void Awake()
    {

    }

    void Start()
    {
        // Re-Reference DDOLs
        //GameManager.Instance.Referencing();
        //EffectManager.Instance.Referencing();
    }

    public void GetCurrentSceneName()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;
    }

    public void GetCurrentSceneType()
    {
        if(SceneName.MainMenu == SceneManager.GetActiveScene().name)
        {
            CurrentSceneType = SceneType.MENU;
        }
        else
        {
            CurrentSceneType = SceneType.INGAME;
        }
    }

    public void OnRestartScene()
    {
        // !!!!!!
        Time.timeScale = 1f;

        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void OnRestartScene(float delayTime)
    {
        Time.timeScale = 1f;
        PreLoadSelectedScene(SceneManager.GetActiveScene().name);

    }

    public void OnLoadMainMenuScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneName.MainMenu);
    }

     public void OnLoadNormalScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneName.Normal);
    }
    
    public void LoadSelectedScene(SceneName sceneName)
    {
        Time.timeScale = 1f;
        switch(sceneName.ToString())
        {
            case SceneName.MainMenu:
                {
                    OnLoadMainMenuScene();

                }
                break;

            case SceneName.Normal:
                {
                    OnLoadNormalScene();
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
                isLoadSceneReady = true;

                //asyncOperation.allowSceneActivation = true;



            }

            yield return null;
        }

        
    }
}
