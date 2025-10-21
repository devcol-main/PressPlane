
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using NUnit.Framework;



public class UIController : MonoBehaviour
{

    [Header("Panels")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject initiatePanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject endGamePanel;

    [Header("Setting UIs")]
    [SerializeField] private Button settingButton;
    private bool isSettingButtonOn = false;

    [Header("InGame UIs")]
    [SerializeField] private GameObject currentScore;
    [SerializeField] private GameObject startButton;
 


    //
    private Score score;
    private TextMeshProUGUI currentScoreText;


    private void Awake()
    {
        DOTween.Init();

        settingButton.onClick.AddListener(ToggleSettingButton);

    }
    


    private void Start()
    {
        score = GetComponent<Score>();

        //currentScore.SetActive(true);
        currentScoreText = score.CurrentScoreText;

        SwitchAllPanel(true);
        //startButton.SetActive(true);


        settingButton.enabled = true;
        settingPanel.SetActive(false);

    }

    public void GameSceneInitiate()
    {
        // off
        SwitchAllPanel(false);

        initiatePanel.SetActive(true);
    }

    // =====
    public void GameStart()
    {
        SwitchAllPanel(false);

        inGamePanel.SetActive(true);
    }

    public void GameOver()
    {
        SwitchAllPanel(false);

        endGamePanel.SetActive(true);
    }

    // =====
    private void ToggleSettingButton()
    {
        //settingButton.interactable = !settingButton.interactable;
        //Debug.Log("ToggleSettingButton: " + settingButton.interactable);

        if (!isSettingButtonOn)
        {
            isSettingButtonOn = true;
            settingPanel.SetActive(true);

            GameManager.Instance.PauseGame();
            //settingButton.spriteState.pressedSprite = ;



        }
        else
        {
            isSettingButtonOn = false;
            settingPanel.SetActive(false);

            GameManager.Instance.ResumeGame();


        }
    }

    public void OnInGameSettingPanel()
    {

    }

    public void OffInGameSettingPanel()
    {

    }

    //
    public void OnMenuSceneSettingPanel()
    {

    }
    
     public void OffMenuSceneSettingPanel()
    {
        
    }


    // =====

    public void IncreaseScore()
    {
        score.IncreaseCurrentScore();


        // Animation
        /*
        currentScoreText.rectTransform.DOScale(1.5f, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            currentScoreText.rectTransform.DOScale(1f, 0.5f).SetEase(Ease.InOutSine);
        });
        */

        currentScoreText.rectTransform.DOScale(endValue: 1.5f, duration: 0.25f).SetEase(Ease.InOutSine).SetLoops(loops: 2, LoopType.Yoyo);

    }

    // except Setting Panel
    private void SwitchAllPanel(bool isOn)
    {
        //settingPanel.SetActive(isOn);
        inGamePanel.SetActive(isOn);
        endGamePanel.SetActive(isOn);
        initiatePanel.SetActive(isOn);

    }
    
    // for ui Time.unscaledDeltaTime
    
    #if UNITY_EDITOR
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.C))
        {
            IncreaseScore();

        }
    }
#endif
    




}
