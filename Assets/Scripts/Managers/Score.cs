using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;

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
