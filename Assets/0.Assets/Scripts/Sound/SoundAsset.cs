using UnityEngine;

public class SoundAsset : MonoBehaviour
{
    public enum BGM
    {
        NONE = 0,
        MENU = 1,
        NORMAL = 2,

    }

    [System.Serializable]
    public class BgmSoundAudioClip
    {
        public BGM bgmName;
        public AudioClip audioClip;

        [Range(0f, 1f)]
        public float volume = 1f;

        [Range(0.1f, 3f)]
        public float pitch;
    }

    public BgmSoundAudioClip[] bgmSoundAudioClipArray;

}
