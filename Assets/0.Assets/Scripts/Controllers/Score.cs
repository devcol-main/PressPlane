using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Score : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI currentScoreText;

    public TextMeshProUGUI CurrentScoreText { get { return currentScoreText; } }

    private int currentScore = 0;

    void Awake()
    {
        
    }

    public void IncreaseCurrentScore()
    {
        currentScore += 1;
        currentScoreText.text = currentScore.ToString();
    }

    
}
