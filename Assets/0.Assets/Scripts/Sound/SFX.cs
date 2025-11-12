using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Ricimi;
using UnityEngine;
using UnityEngine.Audio;

public class SFX : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    private SoundAsset soundAsset;

    private AudioSource continuousAudioSource;

    private List<AudioSource> audioSourceList = new List<AudioSource>();
    float volume = 0f;
    float pitch = 0f;

    void Awake()
    {
    }

    void OnEnable()
    {
        soundAsset = FindAnyObjectByType<SoundAsset>();

    }

    public void SetupContinuousAudioSource(GameObject gameObject, SoundAsset.SFXGroup sfxGroup)
    {
        string sfxGroupName = "";

        switch (gameObject.tag)
        {
            case TagName.Enemy:
                sfxGroupName = "ENEMY";
                break;
            case TagName.Obstacle:
                sfxGroupName = "OBSTACLE";
                break;

            case TagName.Player:
                sfxGroupName = "PLAYER";
                break;

            //
            case TagName.UI:
            default:
                sfxGroupName = "UI";
                break;
        }

        Debug.Log("SetupContinuousAudioSource");

        switch (sfxGroup)
        {
            case SoundAsset.SFXGroup.UI:
                sfxGroupName = "UI";
                break;
            case SoundAsset.SFXGroup.PLAYER:
                sfxGroupName = "PLAYER";
                break;
            case SoundAsset.SFXGroup.ENEMY:
                sfxGroupName = "ENEMY";
                break;
            case SoundAsset.SFXGroup.OBSTACLE:
                sfxGroupName = "OBSTACLE";
                break;
        }

        print("gameObject.tag: " + gameObject.tag + " | " + sfxGroupName);

        // string groupPathName = "Master/SFX/" + sfxGroupName;
        string groupPathName = "Master/" + GlobalString.AudioMixer.SFX + "/" + sfxGroupName;
      
        continuousAudioSource = gameObject.GetComponent<AudioSource>();

        if (null == continuousAudioSource)
        {
            continuousAudioSource = gameObject.AddComponent<AudioSource>();
        }

        // continuousAudioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master/SFX/")[0];
        continuousAudioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(groupPathName)[0];
        continuousAudioSource.playOnAwake = false;
        continuousAudioSource.loop = false;
        continuousAudioSource.volume = 0.5f;
        continuousAudioSource.pitch = 1f;
        continuousAudioSource.panStereo = 0;

        //controls the blend between a fully 2D sound and a fully 3D sound.
        // fully 2d -> 0 / fully 3d -> 1
        continuousAudioSource.spatialBlend = 0f;

        audioSourceList.Add(continuousAudioSource);


    }

    // Player
    public void SetupContinuousSFXFly()
    {
        continuousAudioSource.loop = true;
        continuousAudioSource.volume = 0f;
        continuousAudioSource.pitch = 0f;
          
        foreach (var sfx in soundAsset.sfxPlayerAudioClipArray)
        {
            if (SoundAsset.SFXPlayerName.Fly == sfx.soundName)
            {
                continuousAudioSource.clip = sfx.audioClip;
                continuousAudioSource.Play();
            }
        }
    
        /*
        
        foreach (var audios in audioSourceList)
        {
            if(audios.gameObject.name == gameObject.name)    
                print("in setup audios.gameObject.name: " + audios.gameObject.name);
        }

        //
        Debug.Log("SetupContinuousSFXFly");

        continuousAudioSource.loop = true;
        continuousAudioSource.volume = 0f;
        continuousAudioSource.pitch = 0f;

  
        foreach (var sfx in soundAsset.sfxPlayerAudioClipArray)
        {
            Debug.Log("sfx.soundName: " + sfx.soundName);

            if (SoundAsset.SFXPlayerName.Fly == sfx.soundName)
            {

                continuousAudioSource.clip = sfx.audioClip;
                continuousAudioSource.Play();
            }
        }
        */

    }

    public void ContinuousSFXFly(bool isOn, float power = GlobalData.Player.SoundPower)
    {
        const float maxVol = 0.3f;
        const float minVol = 0.05f;
        const float maxPitch = 1f;
        const float minPitch = 0.75f;

        if(false == isOn)
        {
            power *= -1;
        }

        //StartCoroutine(FadeFlySound(power));
        //volume += power * Time.deltaTime;
        volume += power;
        // Mathf.Clamp -> set min max value
        volume = Mathf.Clamp(volume, minVol, maxVol);


        //pitch += power * Time.deltaTime;
        pitch += power;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        /*
        Debug.Log("Current power: " + power);
        Debug.Log("Current pitch: " + pitch);
        Debug.Log("Current volume: " + volume);
        */
        continuousAudioSource.volume = volume;
        continuousAudioSource.pitch = pitch;
    }


    //
    
    private void SFXOneShot(out GameObject soundGameObject, out AudioSource audioSource)
    {       

        soundGameObject = new GameObject();
        soundGameObject.transform.SetParent(this.transform);

        audioSource = soundGameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = 0.8f;
        audioSource.pitch = 1f;
        audioSource.panStereo = 0;
        audioSource.spatialBlend = 0f;

        //audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(GlobalString.AudioMixer.UI)[0];
        
    }

// testing

// public void PlaySFXOneShot()
//     {        
//         GameObject soundGameObject;   
//         AudioSource audioSource;

//         SFXOneShot(out soundGameObject, out audioSource);

//         //SFXOneShot(ref soundGameObject, ref audioSource);

//         Debug.Log("after SFXOneShot: " + soundGameObject);

//         //Debug.Log("PlaySFXOneShot: "+ sfxUIName);

//         string objectName = "PlaySFXOneShot";   

//         foreach (var sfx in soundAsset.sfxUIAudioClipArray)
//         {
//             if(SoundAsset.SFXUIName.Off == sfx.soundName)
//             {
//                 //audioSource.clip = sfx.audioClip;
//                 Debug.Log("PlaySFXOneShot Played: " + sfx.audioClip);
//                 soundGameObject.name = objectName + "_" + sfx.soundName.ToString();

//                 audioSource.PlayOneShot(sfx.audioClip);
//                 //Destroy(soundGameObject, sfx.audioClip.length);

                
//             }
//         }

//     }

//

    public float PlaySFXOneShot(SoundAsset.SFXGroup sfxGroup, SoundAsset.SFXUIName sfxName)
    {     
        //Debug.Log("sfxGroup.ToString: " +sfxGroup.ToString());

        GameObject soundGameObject;   
        AudioSource audioSource;

        SFXOneShot(out soundGameObject, out audioSource);

        string objectName = "PlaySFXOneShot";

        float length = 0f;

        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(sfxGroup.ToString())[0];        

        
        foreach (var sfx in soundAsset.sfxUIAudioClipArray)
        {
            if(sfxName == sfx.soundName)
            {
                //audioSource.clip = sfx.audioClip;
                Debug.Log("PlaySFXOneShot Played: " + sfx.audioClip);
                soundGameObject.name = objectName + "_" + sfx.soundName.ToString();

                audioSource.PlayOneShot(sfx.audioClip);

                length = sfx.audioClip.length;

                Destroy(soundGameObject, sfx.audioClip.length);
                
            }
        }

        return length;

    }


    public float PlaySFXOneShot(SoundAsset.SFXGroup sfxGroup, SoundAsset.SFXPlayerName sfxName)
    {     

        GameObject soundGameObject;   
        AudioSource audioSource;

        SFXOneShot(out soundGameObject, out audioSource);

        string objectName = "PlaySFXOneShot";

        float length = 0f;

        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(sfxGroup.ToString())[0];        

        
        foreach (var sfx in soundAsset.sfxPlayerAudioClipArray)
        {
            if(sfxName == sfx.soundName)
            {
                //audioSource.clip = sfx.audioClip;
                Debug.Log("PlaySFXOneShot Played: " + sfx.audioClip);
                soundGameObject.name = objectName + "_" + sfx.soundName.ToString();

                audioSource.PlayOneShot(sfx.audioClip);
                length = sfx.audioClip.length;
                Destroy(soundGameObject, sfx.audioClip.length);
                
            }
        }

        return length;

    }
}
