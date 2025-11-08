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
    private BGM bgm;
    private SFX sfx;



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



    }

    void OnEnable()
    {
        soundAsset = FindAnyObjectByType<SoundAsset>();
        bgm = FindAnyObjectByType<BGM>();
        sfx = FindAnyObjectByType<SFX>();
    }

    void Start()
    {

    }

    public void PlayBGM(SoundAsset.BGM bgmName,
    SoundAsset.BGM_AMBIENT bgmAmbientName = SoundAsset.BGM_AMBIENT.NONE)
    {
        bgm.PlayBGM(bgmName, bgmAmbientName);

    }

    //
    public void SetupContinuousAudioSource(GameObject gameObject, SoundAsset.SFXGroup sfxGroup)
    {
        sfx.SetupContinuousAudioSource(gameObject, sfxGroup);
    }
    public void SetupContinuousSFXFly()
    {
        sfx.SetupContinuousSFXFly();
    }

    public void ContinuousSFXFly(bool isOn, float power = GlobalData.Player.SoundPower)
    {
        sfx.ContinuousSFXFly(isOn, power);
    }
    //

    public void PlaySFXOneShot(SoundAsset.SFXGroup sfxGroup, SoundAsset.SFXUIName sfxName)
    {
        sfx.PlaySFXOneShot(sfxGroup,sfxName);
    }
    public void PlaySFXOneShot(SoundAsset.SFXGroup sfxGroup, SoundAsset.SFXPlayerName sfxName)
    {
        sfx.PlaySFXOneShot(sfxGroup,sfxName);
    }
    //=====

    // ALL / BGM (ALL or Music or Ambient) / SFX (ALL of except UI or except specific one)
    // BGM Control audiosource pitch -> to pause -> bc few audiosource
    // SFX control audio mixer group vol (can't mute audiomixer by script)
    // converts a linear volume value (from a slider, typically 0.0001 to 1) into a logarithmic scale in decibels (dB) 
    public void PauseAudio(bool isAll, SoundAsset.BGMGroup bgmGroup = SoundAsset.BGMGroup.ALL, SoundAsset.SFXGroup sfxGroup = SoundAsset.SFXGroup.ALL)
    {
        print("PauseAudio " + "bgmGroup: "+ bgmGroup +  "sfxGroup: " + sfxGroup);

        bgm.PauseAudio(isAll, bgmGroup);

        SetAudioMixerVolume(sfxGroup, GlobalData.Audio.AudioMixerMinVolume);
    }

    public void ResumeAudio(bool isAll, SoundAsset.BGMGroup bgmGroup = SoundAsset.BGMGroup.ALL, SoundAsset.SFXGroup sfxGroup = SoundAsset.SFXGroup.ALL)
    {
        print("ResumeAudio: ");

        bgm.ResumeAudio(isAll, bgmGroup);
        SetAudioMixerVolume(sfxGroup, GlobalData.Audio.AudioMixerMaxVolume);

    }

    /// <summary>
    /// SetAudioMixerVolume -> converts a linear volume value (from a slider, typically 0.0001 to 1) into a logarithmic scale in decibels (dB) 
    /// </summary>
    /// <param name= "audioMixerName">audioMixerName (GlobalString.AudioMixer). </param>
    /// <param name= "volume">volume 0.0f ~ 1f. </param>
    /// <returns> SetAudioMixerVolume
    public void SetAudioMixerVolume(SoundAsset.SFXGroup sfxGroup, float volume)
    {
        switch (sfxGroup)
        {
            case SoundAsset.SFXGroup.ALL:
                {
                    audioMixer.SetFloat(GlobalString.AudioMixer.SFX, Mathf.Log10(volume) * 20f);
                    audioMixer.SetFloat(GlobalString.AudioMixer.UI, Mathf.Log10(volume) * 20f);
                    audioMixer.SetFloat(GlobalString.AudioMixer.PLAYER, Mathf.Log10(volume) * 20f);
                    audioMixer.SetFloat(GlobalString.AudioMixer.ENEMY, Mathf.Log10(volume) * 20f);
                    audioMixer.SetFloat(GlobalString.AudioMixer.OBSTACLE, Mathf.Log10(volume) * 20f);
                    
                }
                break;
            case SoundAsset.SFXGroup.SFX:
                {
                    audioMixer.SetFloat(GlobalString.AudioMixer.SFX, Mathf.Log10(volume) * 20f);

                }
                break;    

            case SoundAsset.SFXGroup.UI:
                {
                    audioMixer.SetFloat(GlobalString.AudioMixer.UI, Mathf.Log10(volume) * 20f);

                }
                break;

            case SoundAsset.SFXGroup.PLAYER:
                {
                    audioMixer.SetFloat(GlobalString.AudioMixer.PLAYER, Mathf.Log10(volume) * 20f);

                }
                break;

            case SoundAsset.SFXGroup.ENEMY:
                {
                    audioMixer.SetFloat(GlobalString.AudioMixer.ENEMY, Mathf.Log10(volume) * 20f);

                }
                break;
            case SoundAsset.SFXGroup.OBSTACLE:
                {
                    audioMixer.SetFloat(GlobalString.AudioMixer.OBSTACLE, Mathf.Log10(volume) * 20f);

                }
                break;
        }

    }
    //







}






