using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class IngameSceneUI : MonoBehaviour
{
   [Header("Panels")]
   
    [SerializeField] private GameObject initiatePanel;
    [SerializeField] private GameObject duringPlayingGamePanel;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject ingameSettingPanel;

    //[Header("Setting UIs")]    


    [Header("InGame UIs")]
    [SerializeField] private GameObject currentScore;
    [SerializeField] private GameObject startButton;

    [Header("Buttons")]
    [SerializeField] private Button ingameSettingButton;
    


    private Score score;
    private TextMeshProUGUI currentScoreText;

    private void Awake()
    {
        // called once in application, 
        // ideally at a point where it will execute before any tweens are created.    
        DOTween.Init();

        SwitchAllPanel(false);

    }
    private void Start()
    {        
        score = GetComponent<Score>();
        currentScoreText = score.CurrentScoreText;

        ingameSettingButton.onClick.AddListener(OnInGameSettingPanel);
    }

    //

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
        ingameSettingPanel.SetActive(true);

        Debug.Log("OnInGameSettingPanel");

        SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);

        //SoundManager.Instance.PauseAudio(true);
        //SoundManager.Instance.PauseAudio(false,SoundAsset.BGMGroup.NONE, SoundAsset.SFXGroup.PLAYER & SoundAsset.SFXGroup.ENEMY & SoundAsset.SFXGroup.OBSTACLE);
        SoundManager.Instance.PauseAudio(false,SoundAsset.BGMGroup.NONE, SoundAsset.SFXGroup.PLAYER | SoundAsset.SFXGroup.ENEMY | SoundAsset.SFXGroup.OBSTACLE);// = 7

        //SoundManager.Instance.PauseAudio(false,SoundAsset.BGMGroup.NONE, SoundAsset.SFXGroup.ALL);
        //
        //SoundManager.Instance.ResumeAudio(false,SoundAsset.BGMGroup.NONE, SoundAsset.SFXGroup.SFX);
        //SoundManager.Instance.ResumeAudio(false,SoundAsset.BGMGroup.NONE, SoundAsset.SFXGroup.UI);

        GameManager.Instance.PauseGame();
    }


    public void OffInGameSettingPanel()
    {   
        ingameSettingPanel.SetActive(false);


        //SoundManager.Instance.ResumeAudio(true);
        SoundManager.Instance.ResumeAudio(false,SoundAsset.BGMGroup.ALL, SoundAsset.SFXGroup.PLAYER | SoundAsset.SFXGroup.ENEMY | SoundAsset.SFXGroup.OBSTACLE);// = 7

        SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);
        
        GameManager.Instance.ResumeGame();

        SaveLoadManager.Instance.Save();

        
    }

    public void OnRestartButton()
    {
        SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Restart);

        SaveLoadManager.Instance.Save();
        GameManager.Instance.RestartScene();

    }

    public void OnMainMenuSceneButton()
    {
        SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI,SoundAsset.SFXUIName.MainMenu);

        SaveLoadManager.Instance.Save();
        
        GameManager.Instance.MainMenuScene();
        
    }


    //

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
        
        initiatePanel.SetActive(isOn);
        duringPlayingGamePanel.SetActive(isOn);
        endGamePanel.SetActive(isOn);
        ingameSettingPanel.SetActive(isOn);

    }
}
