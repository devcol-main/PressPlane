using UnityEngine;
using TMPro;


public class Score : MonoBehaviour
{
    //
    public TextMeshProUGUI CurrentScoreText { get { return currentScoreText; } }
    public int HighScore {get {return highScore;} set {highScore = value;} }
    public int CurrentScore { get {return currentScore;} set {currentScore = value;}}
    //
    [SerializeField] private TextMeshProUGUI currentScoreText;

    //

    [Header("Displaying Only For DEBUG")]
    [SerializeField] private int currentScore;
    [SerializeField] private int highScore;
    

    void Awake()
    {
        
    }

    void Start()
    {
        HighScore = GameData.Instance.ingameData.HighScore;

        Debug.Log("High Score: " + HighScore);

        highScore = HighScore;
        
    }

    public void IncreaseCurrentScore()
    {
        Debug.Log("IncreaseCurrentScore at Score");

        currentScore += 1;
        currentScoreText.text = currentScore.ToString();

        if(CheckHighScore())
        {
            SaveLoadManager.Instance.Save();
            // save
            // highscore effect
        }

        Debug.Log("currentScoreText: " + currentScore + " | "  +  " highScore: " + highScore);

    }

    public bool CheckHighScore()
    {
        bool result;

        // since High Score occurs less often
        if(currentScore < HighScore)
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
