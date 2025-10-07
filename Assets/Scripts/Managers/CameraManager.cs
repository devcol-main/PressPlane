using UnityEngine;
using Unity.Cinemachine;
public class CameraManager : MonoBehaviour
{
    //Singleton instance
    public static CameraManager Instance { get; private set; }

    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private CinemachineImpulseSource impulseSource;

    void Awake()
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

        impulseSource = cinemachineCamera.GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeCamera(float intensity = 1.0f)
    {
        if (null != impulseSource)
        {
            impulseSource.GenerateImpulse(intensity);
        }
        else
        {
            Debug.LogWarning("Cinemachine Impulse Source is null");
            impulseSource = cinemachineCamera.GetComponent<CinemachineImpulseSource>();
        }

  
    }
    private void OnApplicationQuit()
    {   
        // prevent cinemachine error on exit     
        mainCamera.GetComponent<CinemachineBrain>().enabled = false;
    }



    //for testing
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C Pressed, shake camera");
            ShakeCamera();

        }
    }
#endif





}
