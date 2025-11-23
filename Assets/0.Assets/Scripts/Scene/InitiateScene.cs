using UnityEngine;

public class InitiateScene : SceneBase
{
    public override void SetBGM()
    {
        // play by logo anim
    }

    protected override void Start()
    {
        base.Start();
        sceneLoader.OnInitiateScene();

        
    }

}
