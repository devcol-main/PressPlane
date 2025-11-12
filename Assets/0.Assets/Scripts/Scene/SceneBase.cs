using UnityEngine;

public abstract class SceneBase : MonoBehaviour
{

    private SceneLoader sceneLoader;

    protected virtual void Start()
    {
        sceneLoader = FindAnyObjectByType<SceneLoader>();
    }

    public abstract void SetBGM();     
    
    protected virtual void InitiateScene()
    {
        Time.timeScale = 1f;

        // Referencing Managers
        GameManager.Instance.Referencing();
        SoundManager.Instance.Referencing();
        // GraphicManager.Instance.Referencing();
        EffectManager.Instance.Referencing();
        // SaveLoadManager.Instance.Referencing();

        SetBGM();
    }

    //public void 

    
}

 public interface IGameScene
 {
    void SetScrollSpeed();
    
 }
public interface IMenuScene
{
        
}