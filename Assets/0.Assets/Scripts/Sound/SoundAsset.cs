using System.ComponentModel;
using UnityEngine;

public class SoundAsset : MonoBehaviour
{
    // AMBIENT, MUSIC
    
    public enum BGM
    {        
        NONE = 0,
        MENU,
        NORMAL,
        ITEM,
    }


    public enum BGM_AMBIENT
    {        
        NONE = 0,
        
    }

    public enum BGMGroup
    {
        NONE, ALL, MUSIC, AMBIENT
    }

    //

    public enum SFXGroup
    {
        NONE,
        ALL,
        SFX,
        UI,
        INGAME,
        PLAYER,
        ENEMY,
        OBSTACLE,
        
    }

    public enum SFXUIName
    {
        On, Off,
        SelectHighPitch, SelectLowPitch,
        Deselect,
        Restart, MainMenu,
        StartGame,
        Result_Pre, Result_DoubleLife, Result_Life, Result_Nothing,
        
    }

    public enum SFXIngame
    {
        Score, ScoreLong, HighScore, GameOver,
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
    public class SFXIngameAudioClip
    {        
        private SFXGroup sfxGroup = SFXGroup.INGAME;
        public SFXIngame soundName;
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
    
    //
    public SFXUIAudioClip[] sfxUIAudioClipArray;
    public SFXIngameAudioClip[] sfxIngameAudioClipArray;
    public SFXPlayerAudioClip[] sfxPlayerAudioClipArray;

    
}
