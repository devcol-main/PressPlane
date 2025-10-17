
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public bool IsPaused { get; set; }

    // Reference
    // private Player player;
    private UIController uiController;

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

        IsPaused = false;
    }

    private void Start()
    {
        Referencing();
    }


    public void Referencing()
    {

        if(null == uiController)
        {
            Debug.Log("GM Referencing");

            uiController = FindAnyObjectByType<UIController>();
        }


    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        IsPaused = false;
    }
    
    public void GameOver()
    {
        uiController.GameOver();
    }

    public void IncreaseScore()
    {
        uiController.IncreaseScore();

    }
    






}
