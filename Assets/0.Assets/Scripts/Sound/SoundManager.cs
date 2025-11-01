using UnityEngine;
using UnityEngine.Audio;

//using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    // 
    public static SoundManager Instance;

    // It brings erros on unity edior
    //[SerializeField] private AudioMixer audioMixer;
    public AudioMixer audioMixer;
    
    //
    private SoundAsset soundAsset;

    private void Awake()
    {
        soundAsset = FindAnyObjectByType<SoundAsset>();    


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

    // testing
    private void Start()
    {
        // creating BGM audiosource child under this gameobject(sound manager)

        print("BGM");
        GameObject soundGameObject = new GameObject("Created BGM TEst");
        soundGameObject.transform.SetParent(this.gameObject.transform);
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

        audioSource.clip = soundAsset.bgmSoundAudioClipArray[0].audioClip;
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.spatialBlend = 0f;
        audioSource.volume = 0.8f;

        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];



        audioSource.Play();
        //Destroy(soundGameObject, logoBgmAudioClip.length);   
    }

    /*
    public void PlayBgm(SoundAsset.BGM BgmName)
    {
        foreach (var bgmSound in soundAsset.bgmSoundAudioClipArray)
        {
            if (bgmSound.bgmName == BgmName)
            {
                //print(bgmSound.bgmName);

                if (audioSource.isPlaying && (bgmSound.audioClip == audioSource.clip))
                {
                    print("from BGM: " + bgmSound.audioClip + " is on play returned");



                    return;
                }
                else
                {
                    audioSource.clip = bgmSound.audioClip;


                    audioSource.Play();
                }



                return;
            }
        }
    }
    */
}

    




