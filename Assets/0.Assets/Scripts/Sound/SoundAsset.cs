using System.ComponentModel;
using UnityEngine;

public class SoundAsset : MonoBehaviour
{
    // AMBIENT, MUSIC
    public enum BGM
    {
        
        MENU = 0,
        NORMAL = 1,
        ITEM = 2,


    }

    public enum SFXGroup
    {
        UI = 0,
        PLAYER = 1,
        ENEMY = 2,
        OBSTACLE = 3,
        
    }

    public enum SFXUIName
    {
        On, Off,
        Restart, MainMenu,
    }

    public enum SFXPlayerName
    {
        Fly,
        Damaged, Death,
    }


    // =====

    [System.Serializable]
    public class BgmSoundAudioClip
    {
        public BGM soundName;
        public AudioClip audioClip;

        /*

        [Range(0f, 1f)]
        public float volume = 1f;

        [Range(0.1f, 3f)]
        public float pitch;

        */
    }

    [System.Serializable]
    public class SFXUIAudioClip
    {        
        private SFXGroup sfxGroup = SFXGroup.UI;
        public SFXUIName soundName;
        public AudioClip audioClip;

    }

    [System.Serializable]
    public class SFXPlayerAudioClip
    {        
        
        private SFXGroup sfxGroup = SFXGroup.PLAYER;
        public SFXPlayerName soundName;
        public AudioClip audioClip;

    }



    public BgmSoundAudioClip[] bgmSoundAudioClipArray;
    public SFXUIAudioClip[] sfxUIAudioClipArray;
    public SFXPlayerAudioClip[] sfxPlayerAudioClipArray;

    
}
