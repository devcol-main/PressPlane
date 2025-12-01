using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UISoundController : MonoBehaviour, ISaveable
{
    [SerializeField] private AudioMixer audioMixer;


    [Header("Master Volume")]
    [SerializeField] private Toggle masterVolumeToggle;
    [SerializeField] private Slider masterVolumeSlider;

    [SerializeField] private Image masterVolumeToggleOnImage;
    [SerializeField] private Image masterVolumeToggleOffImage;

    [SerializeField] private GameObject masterVolumeSliderHandleImage;
    [SerializeField] private GameObject masterVolumeSliderFill;

    [Header("BGM")]
    [SerializeField] private Toggle bgmVolumeToggle;
    [SerializeField] private Slider bgmVolumeSlider;

    [SerializeField] private Image bgmVolumeToggleOnImage;
    [SerializeField] private Image bgmVolumeToggleOffImage;

    [SerializeField] private GameObject bgmVolumeSliderHandleImage;
    [SerializeField] private GameObject bgmVolumeSliderFill;



    [Header("SFX")]
    [SerializeField] private Toggle sfxVolumeToggle;
    [SerializeField] private Slider sfxVolumeSlider;

    [SerializeField] private Image sfxVolumeToggleOnImage;
    [SerializeField] private Image sfxVolumeToggleOffImage;

    [SerializeField] private GameObject sfxVolumeSliderHandleImage;
    [SerializeField] private GameObject sfxVolumeSliderFill;


    //
    private float volume_Master;
    private float volume_Last_Master;


    //
    private float volume_BGM;
    private float volume_Last_BGM;
    private float volume_SFX;
    private float volume_Last_SFX;

    void OnEnable()
    {
        // can't change onValueChanged directly
        //Debug.Log("UISoundController OnEnable");

    }


    private void Start()
    {

        //Debug.Log("Start from " + this.gameObject.name);
        AddingListeners();


    }
    private void AddingListeners()
    {
        masterVolumeSlider.onValueChanged.AddListener(MasterVolumeChanged);
        masterVolumeToggle.onValueChanged.AddListener(ToggleSwitchMasterVolume);

        bgmVolumeSlider.onValueChanged.AddListener(BGMVolumeChanged);
        bgmVolumeToggle.onValueChanged.AddListener(ToggleSwitchBGMVolume);

        sfxVolumeSlider.onValueChanged.AddListener(SFXVolumeChanged);
        sfxVolumeToggle.onValueChanged.AddListener(ToggleSwitchSFXVolume);
    }

    private void SwapImage(bool isOn, Image onImage, Image offImage)
    {
        if (isOn)
        {
            onImage.enabled = true;
            offImage.enabled = false;
        }
        else
        {
            onImage.enabled = false;
            offImage.enabled = true;
        }
    }

    private IEnumerator DelayVolumeChange(string audioMixerName, float volume, float delay)
    {

        // should use WaitForSecondsRealtime instead of WaitForSeconds()
        // b/c it can play while game pause (Time.timeScale =0)
        yield return new WaitForSecondsRealtime(delay);

        //Debug.Log("delayed: " + audioMixerName + " | " + volume);

        audioMixer.SetFloat(audioMixerName, Mathf.Log10(volume) * 20);
    }

    #region MasterVolume
    public void ToggleSwitchMasterVolume(bool isOn)
    {
        //Debug.Log("at ToggleSwitchMasterVolume 1");
        //ToggleMasterVolume(isOn, true);
        ToggleVolume(GlobalString.AudioMixer.Master, isOn, true);

    }

    public void ToggleSwitchMasterVolume(bool isOn, bool isTogglingSoundOn)
    {
        //Debug.Log("at ToggleSwitchMasterVolume 2");
        //ToggleMasterVolume(isOn, isTogglingSoundOn);        
        ToggleVolume(GlobalString.AudioMixer.Master, isOn, isTogglingSoundOn);

    }

    // private void ToggleMasterVolume(bool isOn, bool isTogglingSoundOn = true)
    // {
    //     float length = 0f;

    //     if (isOn)
    //     {
    //         volume_Master = volume_Last_Master;

    //         if (isTogglingSoundOn)
    //             length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);

    //     }
    //     else
    //     {

    //         volume_Master = GlobalData.Audio.AudioMixerMinVolume;

    //         if (isTogglingSoundOn)
    //             length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);

    //     }

    //     //

    //     masterVolumeToggle.isOn = isOn;

    //     masterVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
    //     masterVolumeSliderFill.SetActive(isOn);

    //     SwapImage(isOn, masterVolumeToggleOnImage, masterVolumeToggleOffImage);

    //     StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.Master, volume_Master, length));
    // }


    public void MasterVolumeChanged(float volume)
    {
        //Debug.Log("MasterVolumeChanged");
        //SetMasterVolume(volume, isTogglingSoundOn: false);
        SetVolume(GlobalString.AudioMixer.Master, volume, isTogglingSoundOn: false);
    }


    // private void SetMasterVolume(float volume, bool isTogglingSoundOn = false)
    // {
    //     Debug.Log("SetMasterVolume" + volume + " " + isTogglingSoundOn);

    //     // it has to come before ToggleSwitchVolume();
    //     volume_Last_Master = volume;

    //     if (GlobalData.Audio.AudioMixerMinVolume > volume)
    //     {
    //         volume_Master = GlobalData.Audio.AudioMixerMinVolume;

    //         ToggleSwitchMasterVolume(false, isTogglingSoundOn);

    //     }
    //     else
    //     {
    //         volume_Master = volume;

    //         ToggleSwitchMasterVolume(true, isTogglingSoundOn);
    //     }

    //     audioMixer.SetFloat(GlobalString.AudioMixer.Master, Mathf.Log10(volume_Master) * 20);

    // }

    #endregion

    #region BGM
    public void ToggleSwitchBGMVolume(bool isOn)
    {
        ToggleVolume(GlobalString.AudioMixer.BGM, isOn, true);
    }

    public void ToggleSwitchBGMVolume(bool isOn, bool isTogglingSoundOn)
    {
        ToggleVolume(GlobalString.AudioMixer.BGM, isOn, isTogglingSoundOn);
    }

    //     float length = 0f;

    //     if (isOn)
    //     {
    //         volume_BGM = volume_Last_BGM;

    //         if (isTogglingSoundOn)
    //         {
    //             length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);
    //         }


    //     }
    //     else
    //     {
    //         volume_BGM = GlobalData.Audio.AudioMixerMinVolume;

    //         if (isTogglingSoundOn)
    //         {
    //             length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);
    //         }
    //     }

    //     bgmVolumeToggle.isOn = isOn;

    //     bgmVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
    //     bgmVolumeSliderFill.SetActive(isOn);

    //     SwapImage(isOn, bgmVolumeToggleOnImage, bgmVolumeToggleOffImage);

    //     StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.BGM, volume_BGM, length));


    // }

    public void BGMVolumeChanged(float volume)
    {
        SetVolume(GlobalString.AudioMixer.BGM, volume, isTogglingSoundOn: false);

    }

    private void SetBGMVolume(float volume, bool isTogglingSoundOn = false)
    {
        // it has to come before ToggleSwitchVolume();
        volume_Last_BGM = volume;

        if (GlobalData.Audio.AudioMixerMinVolume > volume)
        {
            volume_BGM = GlobalData.Audio.AudioMixerMinVolume;

            ToggleSwitchBGMVolume(false, isTogglingSoundOn);

        }
        else
        {
            volume_BGM = volume;


            ToggleSwitchBGMVolume(true, isTogglingSoundOn);
        }

        audioMixer.SetFloat(GlobalString.AudioMixer.BGM, Mathf.Log10(volume_BGM) * 20);

    }

    #endregion


    #region SFX

    public void ToggleSwitchSFXVolume(bool isOn)
    {
        ToggleVolume(GlobalString.AudioMixer.SFX, isOn, true);

    }

    // to avoid continuous sound while changing volume with slider
    public void ToggleSwitchSFXVolume(bool isOn, bool isTogglingSoundOn)
    {
        ToggleVolume(GlobalString.AudioMixer.SFX, isOn, isTogglingSoundOn);

    }


    public void SFXVolumeChanged(float volume)
    {
        SetVolume(GlobalString.AudioMixer.SFX, volume, isTogglingSoundOn: false);
    }

    #endregion


    private void ToggleVolume(string audioMixerName, bool isOn, bool isTogglingSoundOn)
    {
        float length = 0f;
        float newVolume = 0f;

        TogglingSound(audioMixerName, isOn, isTogglingSoundOn,ref newVolume, ref length);

        // Toggle Image & volume
        switch (audioMixerName)
        {
            case GlobalString.AudioMixer.Master:
                {
                    volume_Master = newVolume;
                    masterVolumeToggle.isOn = isOn;

                    masterVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
                    masterVolumeSliderFill.SetActive(isOn);

                    SwapImage(isOn, masterVolumeToggleOnImage, masterVolumeToggleOffImage);

                    //StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.Master, volume_Master, length));
                    //StartCoroutine(DelayVolumeChange(audioMixerName, newVolume, length));
                }
                break;
            case GlobalString.AudioMixer.BGM:
                {
                    volume_BGM = newVolume;

                    bgmVolumeToggle.isOn = isOn;

                    bgmVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
                    bgmVolumeSliderFill.SetActive(isOn);

                    SwapImage(isOn, bgmVolumeToggleOnImage, bgmVolumeToggleOffImage);
                    

                }
                break;
            case GlobalString.AudioMixer.SFX:
                {
                    volume_SFX = newVolume;
                    sfxVolumeToggle.isOn = isOn;
                    sfxVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
                    sfxVolumeSliderFill.SetActive(isOn);

                    SwapImage(isOn, sfxVolumeToggleOnImage, sfxVolumeToggleOffImage);
                    
                }
                break;

        }

        StartCoroutine(DelayVolumeChange(audioMixerName, newVolume, length));
    }
    private void TogglingSound(string audioMixerName, bool isOn, bool isTogglingSoundOn, ref float newVolume, ref float length)
    {
        if (isOn)
        {
            switch (audioMixerName)
            {
                case GlobalString.AudioMixer.Master:
                    {
                        newVolume = volume_Last_Master;
                    }
                    break;
                case GlobalString.AudioMixer.BGM:
                    {
                        newVolume = volume_Last_BGM;

                    }
                    break;
                case GlobalString.AudioMixer.SFX:
                    {
                        newVolume = volume_Last_SFX;

                    }
                    break;
            }

            if (isTogglingSoundOn)
                length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);

        }
        else
        {
            newVolume = GlobalData.Audio.AudioMixerMinVolume;

            if (isTogglingSoundOn)
                length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);

        }

        
    }

    private void SetVolume(string audioMixerName, float volume, bool isTogglingSoundOn = false)
    {

        switch (audioMixerName)
        {
            case GlobalString.AudioMixer.Master:
                {
                    Debug.Log("SetVolume: " + audioMixerName);
                    volume_Last_Master = volume;

                    if (GlobalData.Audio.AudioMixerMinVolume > volume)
                    {
                        volume = GlobalData.Audio.AudioMixerMinVolume;
                        volume_Master = volume;

                        ToggleSwitchMasterVolume(false, isTogglingSoundOn);

                    }
                    else
                    {
                        volume_Master = volume;
                        ToggleSwitchMasterVolume(true, isTogglingSoundOn);
                    }
                }
                break;
            case GlobalString.AudioMixer.BGM:
                {
                    Debug.Log("SetVolume: " + audioMixerName);

                    volume_Last_BGM = volume;

                    if (GlobalData.Audio.AudioMixerMinVolume > volume)
                    {
                        volume = GlobalData.Audio.AudioMixerMinVolume;
                        volume_BGM = volume;

                        ToggleSwitchBGMVolume(false, isTogglingSoundOn);

                    }
                    else
                    {
                        volume_BGM = volume;
                        ToggleSwitchBGMVolume(true, isTogglingSoundOn);
                    }

                }
                break;
            case GlobalString.AudioMixer.SFX:
                {
                    Debug.Log("SetVolume: " + audioMixerName);

                    volume_Last_SFX = volume;

                    if (GlobalData.Audio.AudioMixerMinVolume > volume)
                    {
                        volume = GlobalData.Audio.AudioMixerMinVolume;
                        volume_SFX = volume;

                        ToggleSwitchSFXVolume(false, isTogglingSoundOn);

                    }
                    else
                    {
                        volume_SFX = volume;

                        ToggleSwitchSFXVolume(true, isTogglingSoundOn);
                    }


                }
                break;
        }

        Debug.Log("audioMixerName: " + audioMixerName + " | Volume: " + volume);
        audioMixer.SetFloat(audioMixerName, Mathf.Log10(volume) * 20);
    }
    //

    public void PopulateSaveData(SaveDataCollection saveData)
    {

        Debug.Log("PopulateSaveData from" + this.gameObject.name);
        // Master Mixer
        saveData.settingSoundData.isMasterVolumeOn = masterVolumeToggle.isOn;
        saveData.settingSoundData.masterVolume = volume_Master;
        saveData.settingSoundData.masterVolumeSliderValue = masterVolumeSlider.value;

        // BGM Mixer
        saveData.settingSoundData.isBGMVolumeOn = bgmVolumeToggle.isOn;
        saveData.settingSoundData.bgmVolume = volume_BGM;
        saveData.settingSoundData.bgmVolumeSliderValue = bgmVolumeSlider.value;

        // SFX Mixer
        saveData.settingSoundData.isSFXVolumeOn = sfxVolumeToggle.isOn;
        saveData.settingSoundData.sfxVolume = volume_SFX;
        saveData.settingSoundData.sfxVolumeSliderValue = sfxVolumeSlider.value;

    }

    public void LoadFromSaveData(SaveDataCollection saveData)
    {
        Debug.Log("LoadFromSaveData from" + this.gameObject.name);
        // Loading Orders Matter !!!
        
        // Master Mixer
        SetVolume(GlobalString.AudioMixer.Master, saveData.settingSoundData.masterVolume, isTogglingSoundOn: false);
        ToggleSwitchMasterVolume(saveData.settingSoundData.isMasterVolumeOn, isTogglingSoundOn: false);
        masterVolumeSlider.value = saveData.settingSoundData.masterVolumeSliderValue;

        // BGM Mixer
        SetVolume(GlobalString.AudioMixer.BGM, saveData.settingSoundData.bgmVolume, isTogglingSoundOn: false);
        ToggleSwitchBGMVolume(saveData.settingSoundData.isBGMVolumeOn, isTogglingSoundOn: false);
        bgmVolumeSlider.value = saveData.settingSoundData.bgmVolumeSliderValue;

        // SFX Mixer
        SetVolume(GlobalString.AudioMixer.SFX, saveData.settingSoundData.sfxVolume, isTogglingSoundOn: false);
        ToggleSwitchSFXVolume(saveData.settingSoundData.isSFXVolumeOn, isTogglingSoundOn: false);
        sfxVolumeSlider.value = saveData.settingSoundData.sfxVolumeSliderValue;

    }

}
