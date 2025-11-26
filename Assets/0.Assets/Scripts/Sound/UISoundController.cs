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
    private float volume_BGM;
    private float volume_Last_BGM;
    private float volume_SFX;
    private float volume_Last_SFX;

    void OnEnable()
    {
                //
        //Debug.Log("UISoundController");
        masterVolumeSlider.onValueChanged.AddListener(MasterVolumeChanged);
        masterVolumeToggle.onValueChanged.AddListener(ToggleSwitchMasterVolume);

        bgmVolumeSlider.onValueChanged.AddListener(BGMVolumeChanged);
        bgmVolumeToggle.onValueChanged.AddListener(ToggleSwitchBGMVolume);


        // can't change onValueChanged directly
        //sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);

        sfxVolumeSlider.onValueChanged.AddListener(SFXVolumeChanged);
        sfxVolumeToggle.onValueChanged.AddListener(ToggleSwitchSFXVolume);
    }


    private void Start()
    {
        // load    





        // Set / Load value
        //masterVolumeSlider.value = 0.65f;

        //SetMasterVolume(volume_Master);
        //ToggleSwitchMasterVolume(masterVolumeToggle.isOn, false);


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
        //GameData.Instance.settingData.isMasterVolumeOn = isOn;

        float length;

        if (isOn)
        {
            length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);
            volume_Master = volume_Last_Master;
        }
        else
        {
            Debug.Log("here");
            length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);
            volume_Master = GlobalData.Audio.AudioMixerMinVolume;
        }

        masterVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
        masterVolumeSliderFill.SetActive(isOn);

        SwapImage(isOn, masterVolumeToggleOnImage, masterVolumeToggleOffImage);

        StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.Master, volume_Master, length));
        //audioMixer.SetFloat(GlobalString.AudioMixer.Master, Mathf.Log10(volume_Master) * 20);

    }

    public void ToggleSwitchMasterVolume(bool isOn, bool isSoundOn)
    {

        masterVolumeToggle.isOn = isOn;
        //GameData.Instance.settingData.isMasterVolumeOn = isOn;

        float length;

        if (isOn)
        {
            
            volume_Master = volume_Last_Master;

            if(isSoundOn)
            {
                length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);    
            }
            
        }
        else
        {            
            volume_Master = GlobalData.Audio.AudioMixerMinVolume;

            if(isSoundOn)
            {

                
                length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);
            }
        }

        masterVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
        masterVolumeSliderFill.SetActive(isOn);

        SwapImage(isOn, masterVolumeToggleOnImage, masterVolumeToggleOffImage);

        //StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.Master, volume_Master, length));
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

            ToggleSwitchMasterVolume(false,false);

        }
        else
        {
            volume_Master = volume;

            ToggleSwitchMasterVolume(true, false);
        }

        volume_Last_Master = volume;

        //GameData.Instance.settingData.masterVolume = volume;

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

            //Debug.Log("ToggleSwitchBGMVolume");
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

    public void ToggleSwitchBGMVolume(bool isOn, bool isSoundOn)
    {
        bgmVolumeToggle.isOn = isOn;
        float length;

        if (isOn)
        {            
            volume_BGM = volume_Last_BGM;

            if(isSoundOn)
            {
                length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);               
            }
            

        }
        else
        {           
            volume_BGM = GlobalData.Audio.AudioMixerMinVolume;

            if(isSoundOn)
            {
                length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);
            }
        }

        bgmVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
        bgmVolumeSliderFill.SetActive(isOn);

        SwapImage(isOn, bgmVolumeToggleOnImage, bgmVolumeToggleOffImage);

        //StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.BGM, volume_BGM, length));

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

            ToggleSwitchBGMVolume(false,false);

        }
        else
        {
            volume_BGM = volume;


            ToggleSwitchBGMVolume(true,false);
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

    // to avoid continuous sound while changing volume with slider
    public void ToggleSwitchSFXVolume(bool isOn, bool isSoundOn)
    {
        sfxVolumeToggle.isOn = isOn;
        float length;

        if (isOn)
        {
            volume_SFX = volume_Last_SFX;
            audioMixer.SetFloat(GlobalString.AudioMixer.SFX, Mathf.Log10(volume_SFX) * 20);

            if(isSoundOn)
            {
                length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);

            }

        }
        else
        {
            volume_SFX = GlobalData.Audio.AudioMixerMinVolume;

            if(isSoundOn)
            {
                length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);
                StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.SFX, volume_SFX, length));
    
            }
            

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

    public void SetSFXVolume(float volume)
    {
        if (0 == volume)
        {
            volume_SFX = GlobalData.Audio.AudioMixerMinVolume;

            ToggleSwitchSFXVolume(false,isSoundOn: false);

        }
        else
        {
            volume_SFX = volume;

            ToggleSwitchSFXVolume(true, isSoundOn: false);
        }

        volume_Last_SFX = volume;

        audioMixer.SetFloat(GlobalString.AudioMixer.SFX, Mathf.Log10(volume_SFX) * 20);

    }
    #endregion


    public void PopulateSaveData(SaveDataCollection saveData)
    {        

        Debug.Log("PopulateSaveData from" + this.gameObject.name);
        saveData.settingSoundData.isMasterVolumeOn = masterVolumeToggle.isOn;
        saveData.settingSoundData.masterVolume = volume_Master;

        saveData.settingSoundData.masterVolumeSliderValue = masterVolumeSlider.value;

    }

    public void LoadFromSaveData(SaveDataCollection saveData)
    {
        Debug.Log("LoadFromSaveData from" + this.gameObject.name);
        //volume_Master = saveData.settingSoundData.masterVolume;        
        //masterVolumeToggle.isOn = saveData.settingSoundData.isMasterVolumeOn;
        
        //ToggleSwitchMasterVolume(saveData.settingSoundData.isMasterVolumeOn, false);

        masterVolumeSlider.value = saveData.settingSoundData.masterVolumeSliderValue;

        //SetMasterVolume(saveData.settingSoundData.masterVolume);
        ToggleSwitchMasterVolume(saveData.settingSoundData.isMasterVolumeOn, false);
        
    }

}
