
using UnityEngine;



public class UIController : MonoBehaviour
{
    private IngameSceneUI ingameSceneUI;

    #region Ingame

    public void GameSceneInitiate()
    {
        ingameSceneUI = GetComponent<IngameSceneUI>();

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
        ingameSceneUI.GameStart();
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
    
#endregion



}
