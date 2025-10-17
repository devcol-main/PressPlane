using UnityEngine;

public class NormalScene : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private EnvironmentController environmentController;
    [SerializeField] private UIController uiController;

    [Header("References")]
    [SerializeField] private Player player;


    [Header("=For Debug= Settings")]

    [Tooltip("Scroll Speed: 3f")]
    [SerializeField]
    private float scrollSpeed = 3f;
    

    void Awake()
    {
        
    }

    void Start()
    {
        //
        Initiate();

    }



    private void Initiate()
    {
        environmentController.SetScrollSpeed(0);

        uiController.GameSceneInitiate();


        player.Initiate();

    }
    
    public void OnStartGame()
    {
        environmentController.SetScrollSpeed(scrollSpeed);
        
        uiController.GameStart();

        player.GameStart();

    }
    
}
