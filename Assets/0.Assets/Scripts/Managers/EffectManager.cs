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

        //cameraController = FindAnyObjectByType<CameraController>();

    }    

    private void Start()
    {
        Referencing();
    }


    public void Referencing()
    {

        if(null == cameraController)
        {
            Debug.Log("EffectManager Referencing");

            cameraController = FindAnyObjectByType<CameraController>();
        }

        

    }


    public void PlayerDamagedEffect()
    {
        Debug.Log("from effectManager PlayerDamagedEffect()");
        if(null == cameraController)
        {
            Debug.Log("cameraController null");

        }

        cameraController.ShakeCamera();
    }
}
