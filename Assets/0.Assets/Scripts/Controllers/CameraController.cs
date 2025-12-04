using UnityEngine;
using Unity.Cinemachine;
public class CameraController : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private CinemachineImpulseSource impulseSource;

    void Awake()
    {
        Initiate();
    }
    private void Initiate()
    {
        cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
        impulseSource = cinemachineCamera.GetComponent<CinemachineImpulseSource>();

    }


    public void ShakeCamera(float intensity = 2.0f)
    {
        if (null != impulseSource)
        {
            impulseSource.GenerateImpulse(intensity);
        }
        else
        {
            Debug.LogWarning("Cinemachine Impulse Source is null");

            Initiate();
            
            impulseSource.GenerateImpulse(intensity);

        }


    }
    
    private void OnApplicationQuit()
    {   
        // prevent cinemachine error on exit     
        mainCamera.GetComponent<CinemachineBrain>().enabled = false;
    }


/*
    //for testing
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C Pressed, shake camera");
            ShakeCamera();

        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            EffectManager.Instance.PlayConfettiPS();
        }
    }
#endif
*/




}
