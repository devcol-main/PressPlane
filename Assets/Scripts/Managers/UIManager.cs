using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    //
    [SerializeField] private TextMeshProUGUI scoreText;

    //
    private int score = 0;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        scoreText.text = score.ToString();

    }

    public void IncreaseScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }



}
