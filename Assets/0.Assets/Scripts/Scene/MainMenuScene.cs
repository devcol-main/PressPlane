using UnityEngine;

public class MainMenuScene : SceneBase, IMenuScene
{
    [Header("Controllers")]
    [SerializeField] private EnvironmentController environmentController;
    [SerializeField] private UIController uiController;
    
    [Tooltip("Scroll Menu Scene Speed: 1f")]
    [SerializeField]
    private float scrollSpeed = 1f;    
    protected override void Start()
    {
        base.Start();

        InitiateScene();

        //
        
    }

    protected override void InitiateScene()
    {
        // SetBGM & Time.timeScale = 1f;
        base.InitiateScene();

        Initiate();

    }

    private void Initiate()
    {
        environmentController.SetScrollSpeed(scrollSpeed);

        uiController.MenuSceneInitiate();
    }

    public override void SetBGM()
    {       
        SoundManager.Instance.PlayBGM(SoundAsset.BGM.MENU);
        
    }

    public void OnLoadNormalScene()
    {
        SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.SelectHighPitch);
        GameManager.Instance.NormalScene();
        
    }
    
}
