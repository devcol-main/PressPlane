using TMPro;
using UnityEngine;


public class UIController : MonoBehaviour
{

    [Header("Panels")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject endGamePanel;

    //
    private Score score;

    private void Awake()
    {

        
        //
        score = GetComponent<Score>();

    }

    
    public void IncreaseScore()
    {
        score.IncreaseCurrentScore();
    }



}
