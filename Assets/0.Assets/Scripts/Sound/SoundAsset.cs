using UnityEngine;

public class SoundAsset : MonoBehaviour
{
    // AMBIENT, MUSIC
    public enum BGM
    {
        NONE = 0,
        MENU = 1,
        NORMAL = 2,

    }

    public enum SFX
    {
        UI = 0,
        PLAYER = 1,
        ENEMY = 2,
        OBSTACLE = 3,
        
    }

    public enum SFXName
    {
        UI_ON,
    }
    // =====

    [System.Serializable]
    public class BgmSoundAudioClip
    {
        public BGM bgmName;
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
        private SFX sfxGroup = SFX.UI;
        public SFXName sfxName;
        public AudioClip audioClip;

    }

    [System.Serializable]
    public class SFXPlayerAudioClip
    {        
        private SFX sfxGroup = SFX.PLAYER;
        public SFXName sfxName;
        public AudioClip audioClip;

    }



    public BgmSoundAudioClip[] bgmSoundAudioClipArray;
    public SFXUIAudioClip[] sfxUIAudioClipArray;

    public SFXPlayerAudioClip[] sfxPlayerAudioClipArray;

    
}
