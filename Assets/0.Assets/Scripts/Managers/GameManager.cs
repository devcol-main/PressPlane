
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

    private int bonusLife = 0;

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

        RandRewardEvent();
#endif

    }

    private void RandRewardEvent()
    {
        
        //int rand = Random.Range(0, 3);
        int rand = 1;
        Debug.Log("at RandRewardEvent: " + rand);

        if(rand == 1)
        {
            // popup ask player to wathch ad for luck spin
            uiController.AskUserToSeeAds();

            // //
            // Admob admob = FindFirstObjectByType<Admob>();
            // admob.ShowRewardedAd();

        }
    }

    public void OnSpineWheel()
    {
        
        uiController.OnSpinWheel();
        //spinWheel.enabled = true;
    }

    public void GiveRewardedReward(GlobalString.Prize prize)
    {
        switch (prize)
        {
            // none nothing // off spine at spin wheel
            case GlobalString.Prize.NONE:
                {
                    
                    break;
                }
            
            case GlobalString.Prize.DOUBLE_LIFE:
                {
                    sceneLoader.OnRestartScene();
                    //player.HP +=2;
                    bonusLife +=2;
                    
                    break;
                }
            case GlobalString.Prize.SINGLE_LIFE:
                {
                    sceneLoader.OnRestartScene();
                    //player.HP +=1;
                    bonusLife +=1;
                    break;
                }
        }

        // for none 
        // for prize
        // restart game give life
        

    }

    public int GetBonusLife()
    {
        
        int result = bonusLife;

        // reset
        bonusLife = 0;

        return result;
    }
//
    private void OnApplicationQuit()
    {
        SaveLoadManager.Instance.Save();
    }
    






}
