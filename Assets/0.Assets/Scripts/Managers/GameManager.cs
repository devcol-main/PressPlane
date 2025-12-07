
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsPaused { get; set; }

    // Reference
    // private Player player;
    private UIController uiController;
    private SceneLoader sceneLoader;
    private Score score;
    private Player player;

    private void Awake()
    {
        // to Graphic Manager
        // for mobile to 30
        Application.targetFrameRate = 30;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            //Destroy(transform.root.gameObject);
            //Destroy(this.gameObject);

        }
        else
        {

            Instance = this;
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(transform.root.gameObject);


        }

        // player = FindAnyObjectByType<Player>();

        IsPaused = false;
    }

    void OnEnable()
    {
        Referencing();
    }

     void Start()
    {
        

    }


    public void Referencing()
    {
        if(null == uiController)
        {
            //Debug.Log("GM Referencing uiController");
            uiController = FindAnyObjectByType<UIController>();
        }

        if(null == sceneLoader)
        {
            //Debug.Log("GM Referencing sceneLoader");
            sceneLoader = FindAnyObjectByType<SceneLoader>();

        }

        score = FindAnyObjectByType<Score>();
        player = FindAnyObjectByType<Player>();


    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        //Time.unscaledTime = 0f;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void RestartScene()
    {
        sceneLoader.OnRestartScene();

        // resume bgm

    }

    public void MainMenuScene()
    {
        sceneLoader.OnLoadMainMenuScene();
    }

    public void NormalScene()
    {
        sceneLoader.OnLoadNormalScene();
    }
    


    public void IncreaseScore()
    {
        uiController.IncreaseScore();

    }
    //

    public void GameOver()
    {
        
        uiController.GameOver();
        // 
        //SoundManager.Instance.PlayBGM(SoundAsset.BGM.NONE);
        //SoundManager.Instance.PauseBGM();

        SaveLoadManager.Instance.Save();



#if UNITY_ANDROID
        //GPGPS
        // aheiveement check
        GPGSManager.Instance.CheckDeathAchievement(player.NumDeath);

        // put one leaderboard
        if(score.IsAchieveHighScore && 10 <= score.HighScore)
        {
            GPGSManager.Instance.AddHighScoreToLeaderboard(score.HighScore);
        }
#endif


    }

    private void OnApplicationQuit()
    {
        SaveLoadManager.Instance.Save();
    }
    






}
