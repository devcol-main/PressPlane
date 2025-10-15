using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    private CameraController cameraController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);


        }

        cameraController = FindAnyObjectByType<CameraController>();
        
    }
    


    public void PlayerDamagedEffect()
    {
        cameraController.ShakeCamera();
    }
}
