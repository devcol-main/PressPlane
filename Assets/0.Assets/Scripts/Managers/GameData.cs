using UnityEngine;

public class GameData : MonoBehaviour, ISaveable
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

    public IngameData ingameData;
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

    public void PopulateSaveData(SaveDataCollection saveDataCollection)
    {
        Debug.Log("Game data-PopulateSaveData");
        saveDataCollection.normalSceneData.HighScore = ingameData.HighScore;

        //saveDataCollection.settingSoundData.isMasterVolumeOn = settingData.isMasterVolumeOn;
        //saveDataCollection.settingSoundData.masterVolume = settingData.masterVolume;
    }

    public void LoadFromSaveData(SaveDataCollection saveDataCollection)
    {
        Debug.Log("Game data-LoadFromSaveData");
        ingameData.HighScore = saveDataCollection.normalSceneData.HighScore;

        //settingData.isMasterVolumeOn = saveDataCollection.settingSoundData.isMasterVolumeOn;
        //settingData.masterVolume = saveDataCollection.settingSoundData.masterVolume;
    }
}
