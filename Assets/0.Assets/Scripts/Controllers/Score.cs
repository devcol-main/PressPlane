using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Score : MonoBehaviour
{
    //
    public TextMeshProUGUI CurrentScoreText { get { return currentScoreText; } }
    public int HighScore {get;set;}
    public int CurrentScore { get {return currentScore;} set {currentScore = value;}}
    //
    [SerializeField] private TextMeshProUGUI currentScoreText;

    //

    [Header("Only For DEBUG")]

    [SerializeField] private int currentScore;
    [SerializeField] private int testCur;
    [SerializeField] private int testHigh;
    

    void Awake()
    {
        
    }

    void Start()
    {
        HighScore = GameData.Instance.ingameData.HighScore;

        Debug.Log("High Score: " + HighScore);

        testHigh = HighScore;
        
    }

    public void IncreaseCurrentScore()
    {
        Debug.Log("IncreaseCurrentScore at Score");

        currentScore += 1;
        currentScoreText.text = currentScore.ToString();

        CheckHighScore();

        testCur = currentScore;
        testHigh = HighScore;

        Debug.Log("currentScoreText: " + currentScore + " | testcur: " + testCur + " | testhigh: " + testHigh  );

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
            HighScore = currentScore;
            
            //GameData.Instance.ingameData.HighScore = 
            Debug.Log("At High Score");
        }

        return result;
    }






    
}
