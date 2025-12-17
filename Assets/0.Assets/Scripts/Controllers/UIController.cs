using UnityEngine;


public class UIController : MonoBehaviour
{
    [SerializeField] private IngameSceneUI ingameSceneUI;
    

    #region  Menu Scene
    public void MenuSceneInitiate()
    {
        
    }


    #endregion


    #region Ingame Scene

    public void GameSceneInitiate()
    {
        //ingameSceneUI = GetComponent<IngameSceneUI>();

        ingameSceneUI.GameSceneInitiate();

    }

    public void PauseGame()
    {
        GameManager.Instance.PauseGame();
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }

    // =====
    public void GameStart()
    {
        ingameSceneUI.GameStart();
    }

    public void GameOver()
    {
        ingameSceneUI.GameOver();
    }


    public void OnInGameSettingPanel()
    {
        ingameSceneUI.OnInGameSettingPanel();
    }

    public void OffInGameSettingPanel()
    {
        
        ingameSceneUI.OffInGameSettingPanel();
    }


    // =====

    public void IncreaseScore()
    {
        ingameSceneUI.IncreaseScore();
        

    }
    public void OnSpinWheel()
    {
        ingameSceneUI.OnSpinWheel();
    }
    public void SetPlayerBonusLifeUI(int bonusLife)
    {
        ingameSceneUI.SetPlayerBonusLifeUI(bonusLife);
    }

    public void AskUserToSeeAds()
    {
        ingameSceneUI.AskUserToSeeAds();
    }
    
    
//     // for ui Time.unscaledDeltaTime    
//     #if UNITY_EDITOR
//     void Update()
//     {        

//         if (Input.GetKeyDown(KeyCode.C))
//         {
//             IncreaseScore();

//         }
//     }
// #endif
    
#endregion



}
