using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UISoundController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
   

    [Header("Master Volume")]
    [SerializeField] private Toggle masterVolumeToggle;
    [SerializeField] private Slider masterVolumeSlider;

    //[SerializeField] private 
    [SerializeField] private GameObject masterVolumeSliderHandleImage;
    [SerializeField] private GameObject masterVolumeSliderFill;

    [Header("BGM")]
    [SerializeField] private Toggle bgmToggle;
    [SerializeField] private Slider bgmSlider;

    [Header("SFX")]
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Slider sfxSlider;

    //
    private bool isMasterVolumeOn = true;
    private bool isBGMVolumeOn = true;
    private bool isSFXVolumeOn = true;

    //
    private float volume_Master;
    private float volume_Last_Master;
    private float volume_BGM;
    private float volume_SFX;


    private void Start()
    {
        Debug.Log("UISoundController");
        masterVolumeSlider.onValueChanged.AddListener(MasterVolumeChanged);
        masterVolumeToggle.onValueChanged.AddListener(ToggleSwitchMasterVolume);

    
        
        
        // bgmSlider.onValueChanged.AddListener(BGMVolumeChanged);
        // sfxSlider.onValueChanged.AddListener(SFXVolumeChanged);

        

        // Set / Load value
        //masterVolumeSlider.value = 0.65f;
        

    }

    #region ToggleSwitchMasterVolume
    public void ToggleSwitchMasterVolume(bool isOn)
    {
        Debug.Log("ToggleSwitchMasterVolume");
        
        masterVolumeToggle.isOn = isOn;
        isMasterVolumeOn = isOn;

        if(!isOn)
        {
            volume_Master = GlobalData.Audio.AudioMixerMinVolume;
            masterVolumeSliderHandleImage.GetComponent<Image>().enabled = false;
            masterVolumeSliderFill.SetActive(false);
        }        
        else
        {
            volume_Master = volume_Last_Master;
            masterVolumeSliderHandleImage.GetComponent<Image>().enabled = true;
            masterVolumeSliderFill.SetActive(true);

        }
        

        audioMixer.SetFloat(GlobalString.AudioMixer.Master, Mathf.Log10(volume_Master) * 20);


        // SoundManager.Instance.PlaySFXUI(SoundAsset.SFXUI.UI_OFF);

        // float legnth = SoundManager.Instance.SFXLength(SoundAsset.SFXUI.UI_OFF);

        // Invoke("masterVolumeToggleOff", legnth);
        
        
        // Invoke("UI_OnSound", 0.1f);

    }



    public void MasterVolumeChanged(float volume)
    {             
        Debug.Log("MasterVolumeChanged");

        SetMasterVolume(volume);
    }

    // Master
    private void SetMasterVolume(float volume)
    {          
        if(0 == volume)
        {
            volume_Master = GlobalData.Audio.AudioMixerMinVolume;
            //masterVolumeToggleOff();
            ToggleSwitchMasterVolume(false);             

        }
        else
        {
            volume_Master = volume;
            

            ToggleSwitchMasterVolume(true);     
        }

        volume_Last_Master = volume;

        //volume_Last_Master = volume_Master;

        audioMixer.SetFloat(GlobalString.AudioMixer.Master, Mathf.Log10(volume_Master) * 20);        

        //volume_Master = volume;
        //Debug.Log("masterVolumeSlider.value: " + masterVolumeSlider.value );
    }

    //private void 

    #endregion


    // private void UI_OnSound()
    // {
    //     SoundManager.Instance.PlaySFXUI(SoundAsset.SFXUI.UI_ON);
    // }

    // private void UI_OffSound()
    // {
    //     SoundManager.Instance.PlaySFXUI(SoundAsset.SFXUI.UI_OFF);
    // }


    // #region BGM
    // //-----------
    // // BGM
    // public void SetBGMVolume(float volume)
    // {
    //     audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    //     volume_BGM = volume;
    // }


    // public void ToggleSwitchBGMVolume()
    // {
    //     if (isBGMVolumeOn)
    //     {
    //         bgmToggle.isOn = false;
    //         isBGMVolumeOn = false;
    //         audioMixer.SetFloat("BGM", Mathf.Log10(minVolume) * 20);

    //         SoundManager.Instance.PlaySFXUI(SoundAsset.SFXUI.UI_OFF);


    //     }
    //     else
    //     {
    //         bgmToggle.isOn = true;
    //         isBGMVolumeOn = true;
    //         audioMixer.SetFloat("BGM", Mathf.Log10(volume_BGM) * 20);

    //         SoundManager.Instance.PlaySFXUI(SoundAsset.SFXUI.UI_ON);


    //     }
    // }
    // private void BGMVolumeChanged(float volume)
    // {
    //     if (!isBGMVolumeOn)
    //     {
    //         ToggleSwitchBGMVolume();
    //     }
    // }

    // #endregion

    // #region SFX
    // //-----------
    // //SFX
    // public void SetSFXVolume(float volume)
    // {
    //     audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    //     volume_SFX = volume;
    // }


    // public void ToggleSwitchSFXVolume()
    // {
    //     if (isSFXVolumeOn)
    //     {
    //         SoundManager.Instance.PlaySFXUI(SoundAsset.SFXUI.UI_OFF);


    //         Invoke("SFXVolumeToggleOff", SoundManager.Instance.SFXLength(SoundAsset.SFXUI.UI_OFF));

    //         /*
    //         sfxToggle.isOn = false;
    //         isSFXVolumeOn = false;
    //         audioMixer.SetFloat("SFX", Mathf.Log10(minVolume) * 20);
    //         */

    //     }
    //     else
    //     {
    //         sfxToggle.isOn = true;
    //         isSFXVolumeOn = true;
    //         audioMixer.SetFloat("SFX", Mathf.Log10(volume_SFX) * 20);

    //         Invoke("UI_OnSound", 0.1f);

    //     }
    // }

    // private void SFXVolumeToggleOff()
    // {
    //     sfxToggle.isOn = false;
    //     isSFXVolumeOn = false;
    //     audioMixer.SetFloat("SFX", Mathf.Log10(minVolume) * 20);
    // }



    // private void SFXVolumeChanged(float volume)
    // {
    //     if (!isSFXVolumeOn)
    //     {
    //         ToggleSwitchSFXVolume();
    //     }
    // }

    // #endregion


}
