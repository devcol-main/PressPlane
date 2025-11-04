using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;

//using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    // 
    public static SoundManager Instance { get; private set; }

    // It brings erros on unity edior
    //[SerializeField] private AudioMixer audioMixer;
    public AudioMixer audioMixer;        

    //
    private SoundAsset soundAsset;
    
    //
    private AudioSource audioSource;

    //
    private const float bgmMaxVolume = 0.8f;

    private void Awake()
    {    
        //
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            //Destroy(transform.root.gameObject);          

        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            //DontDestroyOnLoad(transform.root.gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        
    }

    private void Start()
    {       
        //
        soundAsset = FindAnyObjectByType<SoundAsset>();    
        //
        SetupBGMAudioSource();

        //PlayBGM(SoundAsset.BGM.NORMAL);
        //StartCoroutine(Fade(true));

    }

    private void SetupBGMAudioSource()
    {
        //audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master/BGM/Music")[0];
        audioSource.playOnAwake = false;
        audioSource.loop = true;
        audioSource.volume = bgmMaxVolume;
        audioSource.pitch = 1f;
        audioSource.panStereo = 0;

        //controls the blend between a fully 2D sound and a fully 3D sound.
        // fully 2d -> 0 / fully 3d -> 1
        audioSource.spatialBlend = 0f;       

    }

    public void PlayBGM(SoundAsset.BGM bgmName)
    {
        print("PlayBGM: " + bgmName.ToString());

        foreach (var bgmSound in soundAsset.bgmSoundAudioClipArray)
        {
            if (bgmSound.bgmName == bgmName)
            {            
                

                //if (audioSource.isPlaying && (bgmSound.audioClip == audioSource.clip))
                if (audioSource.isPlaying)
                {
                    if(bgmSound.audioClip == audioSource.clip)
                    {
                        //Debug.Log("from BGM: " + bgmSound.audioClip + " is on play returned");

                        return;
                    }
                    else
                    {                       

                        SwapBGMWithFade(bgmSound.audioClip);

                        // fade out current bgm
                        
                        // fade in selected bgm

                    }                    

                }
                else
                {
                    print("play");

                    audioSource.clip = bgmSound.audioClip;
                    audioSource.Play();

                    print("audioSource.clip.name: " + audioSource.clip.name);
                }

                return;
            }
        }

    }

    private IEnumerator Fade(bool isFadeIn) //, float duration, float tartgetVolume)
    {
        float fadeDuration = 2f;
        float time = 0f;
        float startVolume;
        float endVolume;

        // fade out
        if(!isFadeIn)
        {
            startVolume = audioSource.volume;
            endVolume = 0f;
        }
        // fade in
        else
        {
            startVolume = 0f;
            endVolume = bgmMaxVolume;
        }     
        

        while(time < fadeDuration)
        {
            time += Time.deltaTime;

            // smoothly interpolates the volume
            //audioSource.volume = Mathf.Lerp(startVolume, tartgetVolume, t:(time / audioSource.clip.length));

            audioSource.volume = Mathf.Lerp(startVolume, endVolume, (time/fadeDuration));


            yield return null;
        }        

    }

    private void SwapBGMWithFade(AudioClip audioClip)
    {
        print("SwapBGMWithFade: " + audioClip.ToString());


        Fade(false);
        audioSource.clip = audioClip;
        Fade(true);
    }

    


    // =======
    // creating gameobject for bgm under soundmanager
    /*
    private void CreateBgmAudioSource()
    {
        // creating BGM audiosource child under this gameobject(sound manager)
        GameObject soundGameObject = new GameObject("Created BGM");
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
    */

}

    




