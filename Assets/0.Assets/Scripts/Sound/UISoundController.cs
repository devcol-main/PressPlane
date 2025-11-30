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
        Debug.Log("UISoundController OnEnable");

    }


    private void Start()
    {
        
        Debug.Log("Start from " + this.gameObject.name);
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
        Debug.Log("at ToggleSwitchMasterVolume 1");
        ToggleMasterVolume(isOn, true);

    }

    public void ToggleSwitchMasterVolume(bool isOn, bool isTogglingSoundOn)
    {       
        Debug.Log("at ToggleSwitchMasterVolume 2");
        ToggleMasterVolume(isOn, isTogglingSoundOn);        

    }   
    private void ToggleMasterVolume(bool isOn, bool isTogglingSoundOn = true)
    {
        float length = 0f;

        if (isOn)
        {            
            volume_Master = volume_Last_Master;

            if(isTogglingSoundOn)
            length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);                

        }
        else
        {
                        
            volume_Master = GlobalData.Audio.AudioMixerMinVolume;

            if(isTogglingSoundOn)
            length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);                
            
        }    

        //

        masterVolumeToggle.isOn = isOn;

        masterVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
        masterVolumeSliderFill.SetActive(isOn);

        SwapImage(isOn, masterVolumeToggleOnImage, masterVolumeToggleOffImage);

        StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.Master, volume_Master, length));
    }
   
    
    public void MasterVolumeChanged(float volume)
    {
        Debug.Log("MasterVolumeChanged");

        SetMasterVolume(volume, isTogglingSoundOn: false);
    }

    // Master
    private void SetMasterVolume(float volume, bool isTogglingSoundOn = false)
    {
        Debug.Log("SetMasterVolume" + volume + " " + isTogglingSoundOn);

        // it has to come before ToggleSwitchVolume();
        volume_Last_Master = volume;

        if (GlobalData.Audio.AudioMixerMinVolume > volume)
        {
            volume_Master = GlobalData.Audio.AudioMixerMinVolume;

            ToggleSwitchMasterVolume(false,isTogglingSoundOn);

        }
        else
        {
            volume_Master = volume;

            ToggleSwitchMasterVolume(true,isTogglingSoundOn);
        }

        audioMixer.SetFloat(GlobalString.AudioMixer.Master, Mathf.Log10(volume_Master) * 20);

    }

    #endregion


    #region BGM
    public void ToggleSwitchBGMVolume(bool isOn)
    {
        ToggleBGMVolum(isOn, true);
    }

    //   

    public void ToggleSwitchBGMVolume(bool isOn, bool isTogglingSoundOn)
    {
        ToggleBGMVolum(isOn, isTogglingSoundOn);
    }

    private void ToggleBGMVolum(bool isOn, bool isTogglingSoundOn = true)
    {
        float length = 0f;
        
        if (isOn)
        {            
            volume_BGM = volume_Last_BGM;

            if(isTogglingSoundOn)
            {
                length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);               
            }
            

        }
        else
        {           
            volume_BGM = GlobalData.Audio.AudioMixerMinVolume;

            if(isTogglingSoundOn)
            {
                length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);
            }
        }

        bgmVolumeToggle.isOn = isOn;

        bgmVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
        bgmVolumeSliderFill.SetActive(isOn);

        SwapImage(isOn, bgmVolumeToggleOnImage, bgmVolumeToggleOffImage);

        StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.BGM, volume_BGM, length));

        
    }
    public void BGMVolumeChanged(float volume)
    {

        SetBGMVolume(volume, isTogglingSoundOn: false);
    }

    private void SetBGMVolume(float volume, bool isTogglingSoundOn = false)
    {
        // it has to come before ToggleSwitchVolume();
        volume_Last_BGM = volume;

        if (GlobalData.Audio.AudioMixerMinVolume > volume)
        {
            volume_BGM = GlobalData.Audio.AudioMixerMinVolume;

            ToggleSwitchBGMVolume(false,isTogglingSoundOn);

        }
        else
        {
            volume_BGM = volume;


            ToggleSwitchBGMVolume(true,isTogglingSoundOn);
        }

        audioMixer.SetFloat(GlobalString.AudioMixer.BGM, Mathf.Log10(volume_BGM) * 20);

    }

    #endregion


    #region SFX

    public void ToggleSwitchSFXVolume(bool isOn)
    {
        ToggleSFXVolume(isOn,true);
    }

    // to avoid continuous sound while changing volume with slider
    public void ToggleSwitchSFXVolume(bool isOn, bool isTogglingSoundOn)
    {
        ToggleSFXVolume(isOn,isTogglingSoundOn);

    }

    private void ToggleSFXVolume(bool isOn, bool isTogglingSoundOn = true)
    {
        
        float length = 0f;

        if (isOn)
        {
            volume_SFX = volume_Last_SFX;
            
            if(isTogglingSoundOn)
            {
                length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);

            }

        }
        else
        {
            volume_SFX = GlobalData.Audio.AudioMixerMinVolume;

            if(isTogglingSoundOn)
            {
                length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);
                   
            }
            

        }

        sfxVolumeToggle.isOn = isOn;

        sfxVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
        sfxVolumeSliderFill.SetActive(isOn);

        SwapImage(isOn, sfxVolumeToggleOnImage, sfxVolumeToggleOffImage);

        StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.SFX, volume_SFX, length));
        

        
    }

    public void SFXVolumeChanged(float volume)
    {
        SetSFXVolume(volume);
    }

    public void SetSFXVolume(float volume, bool isTogglingSoundOn = false)
    {
        // it has to come before ToggleSwitchVolume();
        volume_Last_SFX = volume;

        if (GlobalData.Audio.AudioMixerMinVolume > volume)
        {
            volume_SFX = GlobalData.Audio.AudioMixerMinVolume;

            ToggleSwitchSFXVolume(false, isTogglingSoundOn);

        }
        else
        {
            volume_SFX = volume;

            ToggleSwitchSFXVolume(true, isTogglingSoundOn);
        }

        

        audioMixer.SetFloat(GlobalString.AudioMixer.SFX, Mathf.Log10(volume_SFX) * 20);

    }
    #endregion


// Testing to Combine all Toggle func
    private void ToggleVolume(GlobalString.AudioMixer audioMixerName ,bool isOn, bool isTogglingSoundOn)
    {
        float length = 0f;
        float newVolume = 0f;

        if (isOn)
        {    
            newVolume = volume_Last_Master;

            if(isTogglingSoundOn)
            length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);                

        }
        else
        {                        
            newVolume = GlobalData.Audio.AudioMixerMinVolume;

            if(isTogglingSoundOn)
            length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);                
            
        }    

        switch(audioMixerName.ToString())
        {
            case GlobalString.AudioMixer.Master:
                {
                    volume_Master = newVolume;

                    masterVolumeToggle.isOn = isOn;

                    masterVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
                    masterVolumeSliderFill.SetActive(isOn);

                    SwapImage(isOn, masterVolumeToggleOnImage, masterVolumeToggleOffImage);

                    StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.Master, volume_Master, length));
                }
                break;
            case GlobalString.AudioMixer.BGM:
                {
                    
                }
                break;
            case GlobalString.AudioMixer.SFX:
                {
                    
                }
                break;

        }

        //
        if (isOn)
        {    
            volume_Master = volume_Last_Master;

            if(isTogglingSoundOn)
            length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.On);                

        }
        else
        {                        
            volume_Master = GlobalData.Audio.AudioMixerMinVolume;

            if(isTogglingSoundOn)
            length = SoundManager.Instance.PlaySFXOneShot(SoundAsset.SFXGroup.UI, SoundAsset.SFXUIName.Off);                
            
        }    

        //

        masterVolumeToggle.isOn = isOn;

        masterVolumeSliderHandleImage.GetComponent<Image>().enabled = isOn;
        masterVolumeSliderFill.SetActive(isOn);

        SwapImage(isOn, masterVolumeToggleOnImage, masterVolumeToggleOffImage);

        StartCoroutine(DelayVolumeChange(GlobalString.AudioMixer.Master, volume_Master, length));


    }
//
    
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
        

        SetMasterVolume(saveData.settingSoundData.masterVolume, isTogglingSoundOn: false);
        ToggleSwitchMasterVolume(saveData.settingSoundData.isMasterVolumeOn,isTogglingSoundOn: false);
        
        masterVolumeSlider.value = saveData.settingSoundData.masterVolumeSliderValue;
        
        
    }

}
