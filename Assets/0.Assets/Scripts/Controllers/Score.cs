using UnityEngine;
using TMPro;


public class Score : MonoBehaviour
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
        HighScore = GameData.Instance.ingameData.HighScore;

        Debug.Log("High Score: " + HighScore);

        highScore = HighScore;

    }

    public void IncreaseCurrentScore()
    {
        Debug.Log("IncreaseCurrentScore at Score");

        currentScore += 1;
        currentScoreText.text = currentScore.ToString();

        if (CheckHighScore())
        {
            // save
            SaveLoadManager.Instance.Save();

            // only once per play
            if (!isAchieveHighScore)
            {
                isAchieveHighScore = true;
                // sound            
                SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.INGAME, SoundAsset.SFXIngame.HighScore);
                // highscore effect

            }
            else
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



        }
        else
        {
            //
            if (0 == (currentScore % 10))
            {
                SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.INGAME, SoundAsset.SFXIngame.ScoreLong);
            }
            else
            {
                SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.INGAME, SoundAsset.SFXIngame.Score);
            }

        }

        Debug.Log("currentScoreText: " + currentScore + " | " + " highScore: " + highScore);

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

            GameData.Instance.ingameData.HighScore = highScore;
            Debug.Log("At High Score");
        }

        return result;
    }







}
