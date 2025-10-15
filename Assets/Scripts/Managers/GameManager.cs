using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // private Player player;
    UIController uiController;

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

        // player = FindAnyObjectByType<Player>();

        uiController = FindAnyObjectByType<UIController>();


    }
    
    public void IncreaseScore()
    {
        uiController.IncreaseScore();
        
    }





}
