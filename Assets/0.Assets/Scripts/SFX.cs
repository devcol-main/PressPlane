using System.Collections;
using Ricimi;
using UnityEngine;
using UnityEngine.Audio;

public class SFX : MonoBehaviour
{
    public AudioMixer audioMixer;  
    
    //
    private SoundAsset soundAsset;  

    [SerializeField] private AudioSource continuousAudioSource;

    float volume = 0f;
    float pitch = 0f;

    void Start()
    {
        soundAsset = FindAnyObjectByType<SoundAsset>();    

    }

    public void SetupContinuousAudioSource(SoundAsset.SFXGroup sfxGroup)
    {
        string sfxGroupName="";

        switch(sfxGroup)
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
        string groupPathName = "Master/SFX/"+sfxGroupName;
         

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
    }

    // Player

    public void SetupContinuousSFXFly()
    {
        continuousAudioSource.loop = true;
        continuousAudioSource.volume = 0f;
        continuousAudioSource.pitch = 0f;
        
        foreach(var sfx in soundAsset.sfxPlayerAudioClipArray)
        {
            if(SoundAsset.SFXPlayerName.Fly == sfx.soundName)
            {
                continuousAudioSource.clip = sfx.audioClip;
                continuousAudioSource.Play();
            }
        }

        
    }


    public void ContinuousSFXFly(float power)
    {    
        const float maxVol = 0.3f;
        const float minVol = 0.1f;
        const float maxPitch = 1f;
        const float minPitch = 0.75f;

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

}
