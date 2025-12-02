using UnityEngine;
using TMPro;


public class Score : MonoBehaviour, ISaveable
{
    //
    public TextMeshProUGUI CurrentScoreText { get { return currentScoreText; } }
    public int HighScore { get { return highScore; } set { highScore = value; } }
    public int CurrentScore { get { return currentScore; } set { currentScore = value; } }
    //
    [SerializeField] private TextMeshProUGUI currentScoreText;

    //

    [Header("Displaying Only For DEBUG")]
    [SerializeField] private int currentScore;
    [SerializeField] private int highScore;

    private bool isAchieveHighScore = false;


    void Awake()
    {

    }

    void Start()
    {
        // load
        //HighScore = GameData.Instance.ingameData.HighScore;

        Debug.Log("High Score: " + highScore);

        //highScore = HighScore;

    }

    public void IncreaseCurrentScore()
    {
        Debug.Log("IncreaseCurrentScore at Score");

        currentScore += 1;
        currentScoreText.text = currentScore.ToString();

        if (CheckHighScore())
        {
            //GameData.Instance.ingameData.HighScore = highScore;
            // save
            SaveLoadManager.Instance.Save();

            // only once per play
            if (!isAchieveHighScore)
            {
                isAchieveHighScore = true;
                // sound            
                SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.INGAME, SoundAsset.SFXIngame.HighScore);

                // highscore effect
                EffectManager.Instance.PlayConfettiPS();

            }
            else
            {
                PlayScoreSound();
            }

        }
        else
        {
            PlayScoreSound();

        }

        Debug.Log("currentScoreText: " + currentScore + " | " + " highScore: " + highScore);

    }

    private void PlayScoreSound()
    {
        if (0 == (currentScore % 10))
        {
            SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.INGAME, SoundAsset.SFXIngame.ScoreLong);
        }
        else
        {
            SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.INGAME, SoundAsset.SFXIngame.Score);
        }
    }

    public bool CheckHighScore()
    {
        bool result;

        // since High Score occurs less often
        if (currentScore <= HighScore)
        {
            result = false;
        }
        else
        {
            result = true;
            highScore = currentScore;            

            Debug.Log("At High Score");
        }

        return result;
    }

    public void PopulateSaveData(SaveDataCollection saveDataCollection)
    {
        Debug.Log("PopulateSaveData from" + this.gameObject.name);
        saveDataCollection.normalSceneData.highScore = highScore;
        
    }

    public void LoadFromSaveData(SaveDataCollection saveDataCollection)
    {
        Debug.Log("LoadFromSaveData from" + this.gameObject.name);
        highScore = saveDataCollection.normalSceneData.highScore;
        
    }
    







}
