using UnityEngine;

public abstract class SceneBase : MonoBehaviour
{
    protected SceneLoader sceneLoader;

}

 public interface IGameScene
 {
    void SetScrollSpeed();
    
    
 }
public interface IMenuScene
{


}