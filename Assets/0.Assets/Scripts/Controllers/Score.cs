using UnityEngine;
using TMPro;

// for normal scene
public class Score : MonoBehaviour, ISaveable
{
    // Get Set
    public TextMeshProUGUI CurrentScoreText { get { return currentScoreText; } }
    public int HighScore { get { return highScore; } set { highScore = value; } }
    public int CurrentScore { get { return currentScore; } set { currentScore = value; } }
    
    public int NumGamePlayed { get { return numGamePlayed;} set { numGamePlayed = value;}  }


    public bool IsAchieveHighScore { get { return isAchieveHighScore;}}
    //
    [SerializeField] private TextMeshProUGUI currentScoreText;

    //

    [Header("Displaying Only For DEBUG")]
    [SerializeField] [ReadOnly] private int currentScore;
    [SerializeField] [ReadOnly]private int highScore;
    [SerializeField] [ReadOnly] private int numGamePlayed;
    [SerializeField] [ReadOnly] private int numTotalEarnedScore;

    //
    private bool isAchieveHighScore = false;



    public void IncreaseCurrentScore()
    {       

        ++currentScore;// += 1;
        currentScoreText.text = currentScore.ToString();

        ++numTotalEarnedScore;
        
#if UNITY_ANDROID
        GPGSManager.Instance.CheckScoreAchievement(currentScore);
#endif

        if (CheckHighScore())
        {            
            // save at GM
            //SaveLoadManager.Instance.Save();

            // only once per play
            if (!isAchieveHighScore )
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

            
        }

        return result;
    }

    public void PopulateSaveData(SaveDataCollection saveDataCollection)
    {
        //Debug.Log("PopulateSaveData from" + this.gameObject.name);
 
        saveDataCollection.normalSceneData.highScore = highScore;
        saveDataCollection.normalSceneData.numGamePlayed += numGamePlayed;
        saveDataCollection.normalSceneData.numTotalEarnedScore += numTotalEarnedScore;
        
    }

    public void LoadFromSaveData(SaveDataCollection saveDataCollection)
    {
        //Debug.Log("LoadFromSaveData from" + this.gameObject.name);
        highScore = saveDataCollection.normalSceneData.highScore;
        numGamePlayed = saveDataCollection.normalSceneData.numGamePlayed;
        numTotalEarnedScore = saveDataCollection.normalSceneData.numTotalEarnedScore;

        
    }
    







}
