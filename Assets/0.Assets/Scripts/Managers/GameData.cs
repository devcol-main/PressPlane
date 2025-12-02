using UnityEngine;

public class GameData : MonoBehaviour
{
    public struct IngameData
    {
        public int HighScore;
        public int CurrentScore;
    }

/*
    public struct SettingData
    {
        // volumes
        public bool isMasterVolumeOn;
        public float masterVolume;

    }

*/

    //public IngameData ingameData;
    //public SettingData settingData;

    public static GameData Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            //Destroy(transform.root.gameObject);
            //Destroy(this.gameObject);

        }
        else
        {

            Instance = this;
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(transform.root.gameObject);

        }
    }

}
