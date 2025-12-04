using UnityEngine;

public class MainMenuScene : SceneBase, IMenuScene
{
    //Reference
    private Timer timer;

    [Header("Controllers")]
    [SerializeField] private EnvironmentController environmentController;
    [SerializeField] private UIController uiController;
    
    [Tooltip("Scroll Menu Scene Speed: 1f")]
    [SerializeField]
    private float scrollSpeed = 1f;    
    void Start()
    {
        sceneLoader = FindFirstObjectByType<SceneLoader>();

        InitiateSetting();

        //
        sceneLoader.OnInitiateMainMenuScene();
        
    }

    private void InitiateSetting()
    {
        Time.timeScale = 1f;

        // Referencing Managers
        GameManager.Instance.Referencing();
        SoundManager.Instance.Referencing();
        // GraphicManager.Instance.Referencing();
        EffectManager.Instance.Referencing();
        SaveLoadManager.Instance.Referencing();

    
        SaveLoadManager.Instance.Load();

        environmentController.SetScrollSpeed(scrollSpeed);
        uiController.MenuSceneInitiate();

        Debug.Log("from " + this.gameObject.name + ", PlayBGM");
        
        SoundManager.Instance.PlayBGM(SoundAsset.BGM.MENU);
    }



    public void OnLoadNormalScene()
    {
        SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.SelectHighPitch);
        //GameManager.Instance.NormalScene();
        sceneLoader.OnLoadNormalScene();
        
    }
    
}
