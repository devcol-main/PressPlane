using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.Processors;

//using UnityEngine.Audio;

//[RequireComponent(typeof(AudioSource))]
public class BGM : MonoBehaviour
{
    //public AudioMixer audioMixer;

    [Header("BGM_MUSIC")]
    [SerializeField] private GameObject bgm_music;
    [SerializeField] private AudioSource audioSourceMusic;


    [Header("BGM_AMBIENT")]
    [SerializeField] private GameObject bgm_ambient;
    [SerializeField] private AudioSource audioSourceAmbient;

        
    private SoundAsset soundAsset;

    void OnEnable()
    {
        soundAsset = FindAnyObjectByType<SoundAsset>();

        //
        Initialize();
    }

    void Start()
    {

    }

    private void Initialize()
    {
        // MUSIC
        SetupBGMAudioSource(SoundManager.Instance.audioMixer, audioSourceMusic,
        GlobalString.AudioMixer.AMG_BGM_MUSIC, true, GlobalData.Audio.BgmMaxVolume);

        // AMBIENT
        SetupBGMAudioSource(SoundManager.Instance.audioMixer, audioSourceAmbient,
        GlobalString.AudioMixer.AMG_BGM_AMBIENT, true, GlobalData.Audio.BgmMaxVolume);

    }

    private void SetupBGMAudioSource(AudioMixer audioMixer, AudioSource audioSource, string audioMixerGroupPath,
     bool is2DSound = true, float volume = GlobalData.Audio.BgmMaxVolume)
    {
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(audioMixerGroupPath)[0];
        audioSource.playOnAwake = false;
        audioSource.loop = true;

        audioSource.pitch = 1f;
        audioSource.panStereo = 0;

        //controls the blend between a fully 2D sound and a fully 3D sound.
        // fully 2d -> 0 / fully 3d -> 1
        audioSource.spatialBlend = (is2DSound == true) ? 0 : 1;

        audioSource.volume = volume;
    }

    public void PlayBGM(SoundAsset.BGM bgmName,
    SoundAsset.BGM_AMBIENT bgmAmbientName = SoundAsset.BGM_AMBIENT.NONE)
    {
        

        //AudioSource audioSource = (SoundAsset.BGM.NONE == bgmName) ? audioSourceAmbient : audioSourceMusic;
        AudioSource audioSource = audioSourceMusic;

        foreach (var bgmSound in soundAsset.bgmSoundAudioClipArray)
        {
            if (bgmSound.soundName == bgmName)
            {
                if (audioSource.isPlaying)
                {
                    SwapBGMWithFadeOutIn(audioSource, bgmSound.audioClip);
                    // if (bgmSound.audioClip == audioSource.clip)
                    // {
                    //     return;                     

                    // }
                    // else
                    // {
                    //     SwapBGMWithFade(bgmSound.audioClip, audioSource);
                    // }

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


    private void SwapBGMWithFadeOutIn(AudioSource audioSource, AudioClip audioClip)
    {
        float fadeDuration = 0.5f;

        //Debug.Log("SwapBGMWithFade: " + audioClip.ToString());
        //Debug.Log("Fade out");

        StartCoroutine(FadeOutIn(audioSource, audioClip, fadeDuration));

        // StartCoroutine(Fade(audioSource, false, fadeDuration));
        // StartCoroutine(Fade(audioSource, false, fadeDuration: 2f));
        // audioSource.clip = audioClip;
        // audioSource.Play();
        // StartCoroutine(Fade(audioSource, true, fadeDuration: 2f));
    }

    private IEnumerator FadeOutIn(AudioSource audioSource,AudioClip audioClip, float fadeDuration = 2f) //, float duration, float tartgetVolume)
    {
        //Debug.Log("Fade");

        float time = 0f;
        float startVolume = audioSource.volume;
        float endVolume = 0f;

        // // fade out
        // if (!isFadeIn)
        // {
        //     startVolume = audioSource.volume;
        //     endVolume = 0f;
        // }
        // // fade in
        // else
        // {
        //     startVolume = 0f;
        //     endVolume = GlobalData.Audio.BgmMaxVolume;
        // }

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            // smoothly interpolates the volume
            //audioSource.volume = Mathf.Lerp(startVolume, tartgetVolume, t:(time / audioSource.clip.length));

            // Debug.Log("at Fade: " + (time / fadeDuration));
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, (time / fadeDuration));

            yield return null;
        }

        // Debug.Log("Fade Out Complete");

        audioSource.clip = audioClip;
        audioSource.Play();

        time = 0f;
        startVolume = 0f;
        //startVolume = audioSource.volume;
        endVolume = GlobalData.Audio.BgmMaxVolume;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            // smoothly interpolates the volume
            //audioSource.volume = Mathf.Lerp(startVolume, tartgetVolume, t:(time / audioSource.clip.length));
            //Debug.Log("at Fade: " + (time / fadeDuration));
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, (time / fadeDuration));

            yield return null;
        }


        //Debug.Log("Fade In Complete");


        //Debug.Log(" ALL Fade Out In Complete");

        yield return null;
    }
    

    private IEnumerator Fade(AudioSource audioSource, bool isFadeIn = true, float fadeDuration = 2f) //, float duration, float tartgetVolume)
    {
        Debug.Log("Fade");

        float time = 0f;
        float startVolume;
        float endVolume;

        // fade out
        if (!isFadeIn)
        {
            startVolume = audioSource.volume;
            endVolume = 0f;
        }
        // fade in
        else
        {
            startVolume = 0f;
            endVolume = GlobalData.Audio.BgmMaxVolume;
        }


        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            // smoothly interpolates the volume
            //audioSource.volume = Mathf.Lerp(startVolume, tartgetVolume, t:(time / audioSource.clip.length));
            Debug.Log("at Fade: " + (time / fadeDuration));
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, (time / fadeDuration));

            yield return null;
        }

        Debug.Log("Fade Complete");

        yield return null;
    }


    public void PauseAudio(bool isAll, SoundAsset.BGMGroup bgmGroup = SoundAsset.BGMGroup.MUSIC)
    {
        if (isAll)
        {
            audioSourceMusic.pitch = 0f;
            audioSourceAmbient.pitch = 0f;

        }
        else
        {
            switch (bgmGroup)
            {
                case SoundAsset.BGMGroup.MUSIC:
                    {
                        audioSourceMusic.pitch = 0f;
                    }
                    break;
                case SoundAsset.BGMGroup.AMBIENT:
                    {
                        audioSourceAmbient.pitch = 0f;
                    }
                    break;

            }
        }
    }

    public void ResumeAudio(bool isAll = true, SoundAsset.BGMGroup bgmGroup = SoundAsset.BGMGroup.MUSIC)
    {
        if (isAll)
        {
            audioSourceMusic.pitch = 1f;
            audioSourceAmbient.pitch = 1f;
        }
        else
        {
            switch (bgmGroup)
            {
                case SoundAsset.BGMGroup.MUSIC:
                    {
                        audioSourceMusic.pitch = 1f;
                    }
                    break;
                case SoundAsset.BGMGroup.AMBIENT:
                    {
                        audioSourceAmbient.pitch = 1f;
                    }
                    break;

            }
        }
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
