using System.Collections;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    private CameraController cameraController;

    [SerializeField]
    //private ParticleSystem[] confettiPSGroup;
    private GameObject confettiPSGroup;


    //

    // player
    private bool isPlayerDustPSDelayDone = true;


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
        //Debug.Log("EffectManager Referencing");

        if(null == cameraController)
        {
            //Debug.Log("EffectManager Referencing");
            cameraController = FindAnyObjectByType<CameraController>();
        }

        if (null == confettiPSGroup)
        {
            // GameObject.Find("ParentName/ChildName")) 
            confettiPSGroup = GameObject.Find("VFXs/Particle Systems Group/ConfettiPS");

            if(confettiPSGroup == null)
            {
                Debug.LogError("confettiPSGroup is null: " + "VFXs/Particle Systems Group/ConfettiPS");
            }
        }

    }

    // Player
    public void PlayerDamagedEffect(float camShakeIntensity = 2.0f)
    {
        cameraController.ShakeCamera(camShakeIntensity);
        
    }

    public void SpawnDustPS(GameObject gameObject, ParticleSystem particleSystem)
    {
        //Debug.Log("from effectManager spawndust()");

        StartCoroutine(DelaySpawnPlayerDestPS(gameObject, particleSystem));
    }

    IEnumerator DelaySpawnPlayerDestPS(GameObject gameObject, ParticleSystem particleSystem)
    {
        if (isPlayerDustPSDelayDone)
        {
            //dustPSDelayTime
            isPlayerDustPSDelayDone = false;            

            float randY = UnityEngine.Random.Range(0.2f, 0.4f);
            float randx = UnityEngine.Random.Range(0.2f, 0.6f);
            //Vector2 trans = new Vector2((transform.position.x - 0.6f), (transform.position.y - 0.2f));
            Vector2 trans = new Vector2((gameObject.transform.position.x - randx), (gameObject.transform.position.y - randY));

            ParticleSystem ps = Instantiate(particleSystem, trans, Quaternion.identity);
            ps.transform.SetParent(this.transform);
            ps.Play();

            // Destroy by Particle System: Stop Action -> Destroy

            yield return new WaitForSeconds(GlobalData.Player.PlayerDustPSDelayTime);

            isPlayerDustPSDelayDone = true;
        }
    }

    public void PlayConfettiPS()
    {
        Debug.Log("PlayConfettiPS : " + confettiPSGroup.name);


        //confettiPS.Play();
        confettiPSGroup.SetActive(true);
        // foreach(var v in confettiPSGroup)
        // {
        //     v.Play();            
        // }
    }
}
