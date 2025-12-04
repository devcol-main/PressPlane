using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHistory
{
    public static string PreviousSceneName { get; private set; }
    
    public static void SetPreviousScene(string sceneName)
    {
        PreviousSceneName = sceneName;
    }

}

public class SceneLoader : MonoBehaviour
{

    // Public References
    public bool CueLoadScene { get; set; }
    public static string CurrentSceneName { get; private set; }
    public static SceneType CurrentSceneType { get; private set; }

    // References
    [SerializeField] private LoadingScreen loadingScreen;


    public bool ComparePreviousCurrentScene()
    {
        //bool isPreviousCurrentSceneSame;

        GetCurrentSceneName();
        /*
        if(CurrentSceneName == SceneHistory.PreviousSceneName)
            isPreviousCurrentSceneSame = true;
        else
            isPreviousCurrentSceneSame = false;
        */
        //isPreviousCurrentSceneSame = (CurrentSceneName == SceneHistory.PreviousSceneName) ? true : false;

        //Debug.Log("CurrentSceneName: " + CurrentSceneName);
        //Debug.Log("PreviousSceneName: " + SceneHistory.PreviousSceneName);
        
        return (CurrentSceneName == SceneHistory.PreviousSceneName) ? true : false;
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
    //
    public void OnInitiateScene()
    {
        //SceneHistory.SetPreviousScene(SceneManager.GetActiveScene().name);

        loadingScreen.SelectLoadingType(LOADING_TRANSITION_TYPE.LOGO);
        PreLoadSelectedScene(SceneName.MainMenu);
        //PreLoadSelectedScene(SceneName.Normal);
        
    }

    public void OnInitiateMainMenuScene()
    {
        // fadeout
        loadingScreen.SelectLoadingType(LOADING_TRANSITION_TYPE.CROSS_FADE_IN);
        
        // prepare other scene

        //PreLoadSelectedScene(SceneName.Normal);
        //SceneManager.LoadSceneAsync(SceneName.MainMenu);


    }


    //
    public void OnRestartScene()
    {
        // !!!!!!
        PrepareBeforeLoading();

        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void OnLoadMainMenuScene()
    {
        PrepareBeforeLoading();

        SceneManager.LoadScene(SceneName.MainMenu);
        //SceneManager.LoadSceneAsync(SceneName.MainMenu);
    }

     public void OnLoadNormalScene()
    {
        Debug.Log("OnLoadNormalScene");
        PrepareBeforeLoading();

        SceneManager.LoadScene(SceneName.Normal);


    }
    
    public void LoadSelectedScene(SceneName sceneName)
    {
        PrepareBeforeLoading();

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

    private void PrepareBeforeLoading()
    {
        Time.timeScale = 1f;
        SaveLoadManager.Instance.Save();

        SceneHistory.SetPreviousScene(SceneManager.GetActiveScene().name);
        
    }

    // only with loadingscreen
    private void PreLoadSelectedScene(string sceneName)
    {

        //PrepareBeforeLoading();
        SceneHistory.SetPreviousScene(SceneManager.GetActiveScene().name);

        StartCoroutine(PreLoadAsyncScene(sceneName));
    }
    
    IEnumerator PreLoadAsyncScene(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName); //, LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = false;

        while(!asyncOperation.isDone)
        {
            // for debug
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            //Debug.Log("Scene Loading Progress: " + progress);

            if(loadingScreen.IsLoadingAnimationDone)
            {
                // load to next screen
                if(asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;

                }
                else
                {
                    // display loading screen
                }
            }

            yield return null;
        }        

        // AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        // asyncOperation.allowSceneActivation = false;

        // while(!asyncOperation.isDone)
        // {
        //     // for debug
        //     float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
        //     //Debug.Log("Scene Loading Progress: " + progress);

        //     if(loadingScreen.IsLoadingAnimationDone)
        //     {
        //         // load to next screen
        //         if(asyncOperation.progress >= 0.9f)
        //         {
        //             asyncOperation.allowSceneActivation = true;

        //         }
        //         else
        //         {
        //             // display loading screen
        //         }
        //     }

        //     yield return null;
        // }        
    }
    


}
