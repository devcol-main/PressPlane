using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class IngameSceneUI : MonoBehaviour
{
   [Header("Panels")]
   //[SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject initiatePanel;
    [SerializeField] private GameObject duringPlayingGamePanel;
    [SerializeField] private GameObject endGamePanel;

    [Header("Setting UIs")]
    [SerializeField] private Button settingButton;
    private bool isSettingButtonOn = false;  

    [Header("InGame UIs")]
    [SerializeField] private GameObject currentScore;
    [SerializeField] private GameObject startButton;

    private Score score;
    private TextMeshProUGUI currentScoreText;

    private void Awake()
    {
        DOTween.Init();

        SwitchAllPanel(false);

    }
    private void Start()
    {        
        score = GetComponent<Score>();
        currentScoreText = score.CurrentScoreText;

    }

    public void GameSceneInitiate()
    {
        SwitchAllPanel(false);
        initiatePanel.SetActive(true);
    }

    public void GameStart()
    {
        SwitchAllPanel(false);
        duringPlayingGamePanel.SetActive(true);
    }

    public void GameOver()
    {
        SwitchAllPanel(false);
        endGamePanel.SetActive(true);
    }

    public void OnInGameSettingPanel()
    {
        GameManager.Instance.PauseGame();
        //settingPanel.SetActive(true);
    }


    public void OffInGameSettingPanel()
    {
        
        GameManager.Instance.ResumeGame();
        //settingPanel.SetActive(false);


    }



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

    private void SwitchAllPanel(bool isOn)
    {
        //settingPanel.SetActive(isOn);
        initiatePanel.SetActive(isOn);
        duringPlayingGamePanel.SetActive(isOn);
        endGamePanel.SetActive(isOn);

    }
}
