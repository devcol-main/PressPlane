using UnityEngine;
using UnityEngine.Audio;

//using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{
    // 
    public static SoundManager Instance;

    [SerializeField] private AudioMixer audioMixer;

    //public

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            //Destroy(gameObject);
            Destroy(transform.root.gameObject);

        }
        else
        {

            Instance = this;
            //DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(transform.root.gameObject);


        }
    }

    

}
