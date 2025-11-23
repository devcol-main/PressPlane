

using UnityEngine;
using DG.Tweening;
using System.Collections;

public class NormalScene : SceneBase, IGameScene
{
    [Header("Controllers")]
    [SerializeField] private EnvironmentController environmentController;
    [SerializeField] private UIController uiController;

    [Header("References")]
    [SerializeField] private Player player;

    [SerializeField] private GameObject titleImage;
    [SerializeField] private GameObject startButton;


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

        Debug.Log("sceneLoader.ComparePreviousCurrentScene(): " + sceneLoader.ComparePreviousCurrentScene());

        if (!sceneLoader.ComparePreviousCurrentScene())
        {
            Debug.Log("Firstime Scene, perfome Scene transition");
            // Perform
            StartCoroutine(PerformFirstimeSceneTransition());


        }
    }

    IEnumerator PerformFirstimeSceneTransition()
    {

        startButton.SetActive(false);    

        player.PerformFirstimeSceneTransition();


        // titleImage
        int rand = Random.Range(0,4);
        Vector3 startingPos = new Vector3(0,0,0);
        switch(rand)
        {
            case 0:
                startingPos = new Vector3(-1000f,0f);
            break;
            
            case 1:
                startingPos = new Vector3(1000f,0f);
            break;
            
            case 2:
                startingPos = new Vector3(0f,1200f);
            break;
                  
            case 3:
                startingPos = new Vector3(0f,-1200f);
            break;
        }
        //titleImage.transform.position =
        
        titleImage.GetComponent<RectTransform>().anchoredPosition = startingPos;

        // move title image;
        float duration = 2f;
        Vector3 pos = new Vector3(0f, 0f,0f);

        titleImage.GetComponent<RectTransform>().DOAnchorPos(pos, duration)
             .SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(duration);

        Debug.Log("PerformFirstimeSceneTransition complete") ;

        startButton.SetActive(true);
        
    }

    // private void PerformFirstimeSceneTransition()
    // {
    //     startButton.SetActive(false);    

    //     player.PerformFirstimeSceneTransition();


    //     // titleImage
    //     int rand = Random.Range(0,4);
    //     Vector3 startingPos = new Vector3(0,0,0);
    //     switch(rand)
    //     {
    //         case 0:
    //             startingPos = new Vector3(-1000f,0f);
    //         break;
            
    //         case 1:
    //             startingPos = new Vector3(1000f,0f);
    //         break;
            
    //         case 2:
    //             startingPos = new Vector3(0f,1200f);
    //         break;
                  
    //         case 3:
    //             startingPos = new Vector3(0f,-1200f);
    //         break;
    //     }
    //     //titleImage.transform.position =

    //     titleImage.transform.position = startingPos;

    //     // move title image;
    //     float duration = 2f;
    //     Vector3 pos = new Vector3(0f, 0f);

    //     titleImage.transform.DOMove(pos, duration)
    //          .SetEase(Ease.InOutSine);

    //     //
    //     startButton.SetActive(true);

    // }



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
