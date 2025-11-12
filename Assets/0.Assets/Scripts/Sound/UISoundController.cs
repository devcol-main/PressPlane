using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UISoundController : MonoBehaviour
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
    private float volume_BGM;
    private float volume_Last_BGM;
    private float volume_SFX;
    private float volume_Last_SFX;


    private void Start()
    {
        //Debug.Log("UISoundController");
        masterVolumeSlider.onValueChanged.AddListener(MasterVolumeChanged);
        masterVolumeToggle.onValueChanged.AddListener(ToggleSwitchMasterVolume);

        bgmVolumeSlider.onValueChanged.AddListener(BGMVolumeChanged);
        bgmVolumeToggle.onValueChanged.AddListener(ToggleSwitchBGMVolume);

        // sfxSlider.onValueChanged.AddListener(SFXVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(SFXVolumeChanged);
        sfxVolumeToggle.onValueChanged.AddListener(ToggleSwitchSFXVolume);

        // Set / Load value
        //masterVolumeSlider.value = 0.65f;


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

        masterVolumeToggle.isOn = isOn;

        float length;

        if (isOn)
        {
            length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);
            volume_Master = volume_Last_Master;
        }
        else
        {
            length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);
            volume_Master = GlobalData.Audio.AudioMixerMinVolume;
        }

        masterVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
        masterVolumeSliderFill.SetActive(isOn);

        SwapImage(isOn, masterVolumeToggleOnImage, masterVolumeToggleOffImage);

        StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.Master, volume_Master, length));
        //audioMixer.SetFloat(GlobalString.AudioMixer.Master, Mathf.Log10(volume_Master) * 20);

    }

    public void MasterVolumeChanged(float volume)
    {
        Debug.Log("MasterVolumeChanged");

        SetMasterVolume(volume);
    }

    // Master
    private void SetMasterVolume(float volume)
    {
        if (0 == volume)
        {
            volume_Master = GlobalData.Audio.AudioMixerMinVolume;

            ToggleSwitchMasterVolume(false);

        }
        else
        {
            volume_Master = volume;

            ToggleSwitchMasterVolume(true);
        }

        volume_Last_Master = volume;

        audioMixer.SetFloat(GlobalString.AudioMixer.Master, Mathf.Log10(volume_Master) * 20);

    }

    #endregion


    #region BGM
    public void ToggleSwitchBGMVolume(bool isOn)
    {
        bgmVolumeToggle.isOn = isOn;

        float length;

        if (isOn)
        {
            length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);

            volume_BGM = volume_Last_BGM;

        }
        else
        {
            length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);

            volume_BGM = GlobalData.Audio.AudioMixerMinVolume;
        }

        bgmVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
        bgmVolumeSliderFill.SetActive(isOn);

        SwapImage(isOn, bgmVolumeToggleOnImage, bgmVolumeToggleOffImage);

        StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.BGM, volume_BGM, length));

        // audioMixer.SetFloat(GlobalString.AudioMixer.BGM, Mathf.Log10(volume_BGM) * 20);



    }

    public void BGMVolumeChanged(float volume)
    {

        SetBGMVolume(volume);
    }

    private void SetBGMVolume(float volume)
    {
        if (0 == volume)
        {
            volume_BGM = GlobalData.Audio.AudioMixerMinVolume;

            ToggleSwitchBGMVolume(false);

        }
        else
        {
            volume_BGM = volume;


            ToggleSwitchBGMVolume(true);
        }

        volume_Last_BGM = volume;

        audioMixer.SetFloat(GlobalString.AudioMixer.BGM, Mathf.Log10(volume_BGM) * 20);

    }

    #endregion


    #region SFX

    // sfx dif order of playing sfx from Master And BGM
    public void ToggleSwitchSFXVolume(bool isOn)
    {
        sfxVolumeToggle.isOn = isOn;
        float length;

        if (isOn)
        {
            volume_SFX = volume_Last_SFX;

            audioMixer.SetFloat(GlobalString.AudioMixer.SFX, Mathf.Log10(volume_SFX) * 20);

            length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);

        }
        else
        {
            volume_SFX = GlobalData.Audio.AudioMixerMinVolume;

            length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);

            StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.SFX, volume_SFX, length));


        }

        sfxVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
        sfxVolumeSliderFill.SetActive(isOn);

        SwapImage(isOn, sfxVolumeToggleOnImage, sfxVolumeToggleOffImage);

        //StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.SFX, volume_SFX, length));
        //audioMixer.SetFloat(GlobalString.AudioMixer.SFX, Mathf.Log10(volume_SFX) * 20);



    }

    public void SFXVolumeChanged(float volume)
    {

        SetSFXVolume(volume);
    }

    private void SetSFXVolume(float volume)
    {
        if (0 == volume)
        {
            volume_SFX = GlobalData.Audio.AudioMixerMinVolume;

            ToggleSwitchSFXVolume(false);

        }
        else
        {
            volume_SFX = volume;


            ToggleSwitchSFXVolume(true);
        }

        volume_Last_SFX = volume;

        audioMixer.SetFloat(GlobalString.AudioMixer.SFX, Mathf.Log10(volume_SFX) * 20);

    }

    #endregion
}
