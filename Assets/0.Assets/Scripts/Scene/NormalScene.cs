
using UnityEngine;

public class NormalScene : SceneBase, IGameScene
{
    [Header("Controllers")]
    [SerializeField] private EnvironmentController environmentController;
    [SerializeField] private UIController uiController;

    [Header("References")]
    [SerializeField] private Player player;


    [Header("=For Debug= Settings")]

    [Tooltip("Scroll Speed: 3f")]
    [SerializeField]
    private float scrollSpeed = 3f;    

    void Awake()
    {
        
    }

    protected override void Start()
    {
        base.Start();

        InitiateScene();
    }

    protected override void InitiateScene()
    {
        // SetBGM & Time.timeScale = 1f;
        base.InitiateScene();

        Initiate();

    }

    private void Initiate()
    {
        environmentController.SetScrollSpeed(0);

        uiController.GameSceneInitiate();

        player.Initiate();
        // Scene Transition
        // if last scene was not this scene to inititae transition

              
        if(sceneLoader.ComparePreviousCurrentScene())
        {
            Debug.Log("Firstime Scene, perfome Scene transition");
                
        }


    }

    public void SetScrollSpeed()
    {
        throw new System.NotImplementedException();
    }

    public override void SetBGM()
    {       
        SoundManager.Instance.PlayBGM(SoundAsset.BGM.NORMAL);
        
    }
    
    // from start button
    public void OnStartGame()
    {
        environmentController.SetScrollSpeed(scrollSpeed);
        
        uiController.GameStart();

        player.GameStart();

    }

 
}
