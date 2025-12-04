using UnityEngine;

public class InitiateScene : SceneBase
{
    private Timer timer;

    void Start()
    {
        sceneLoader = FindFirstObjectByType<SceneLoader>();

        sceneLoader.OnInitiateScene();
        InitiateSetting();

/*
        Initiate();

        timer = FindFirstObjectByType<Timer>();
        Debug.Log("timer.AwakeTimer from InitiateScene");
        timer.AwakeTimer();
        */

    }
    
    void InitiateSetting()
    {        
        Time.timeScale = 1f;

    
        // // Referencing Managers
        // GameManager.Instance.Referencing();
        // SoundManager.Instance.Referencing();
        // // GraphicManager.Instance.Referencing();
        // EffectManager.Instance.Referencing();
        // SaveLoadManager.Instance.Referencing();

    
  

    }
   

}
