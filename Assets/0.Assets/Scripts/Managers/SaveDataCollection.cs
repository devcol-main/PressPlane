
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveDataCollection 
{

    [System.Serializable]
    public struct GameSystemTimeData
    {
        public int initialPlayedTime;
        public int recentPlayedTime;

        //public int userLastInDateTime;
    }

    [System.Serializable]
    public struct SettingSoundData
    {
        public bool isMasterVolumeOn;
        public bool isBGMVolumeOn;
        public bool isSFXVolumeOn;

        public float masterVolume;
        public float bgmVolume;
        public float sfxVolume;

    }

    public GameSystemTimeData gameSystemTimeData = new GameSystemTimeData();
    public SettingSoundData settingSoundData = new SettingSoundData();
    //

    //=======================================================

    [System.Serializable]
    public struct NormalSceneData
    {
        public int HighScore;
    }

    public NormalSceneData normalSceneData = new NormalSceneData();



    //=======================================================
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
        
    }

    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }

}


public interface ISaveable
{
    void PopulateSaveData(SaveDataCollection saveData);
    void LoadFromSaveData(SaveDataCollection saveData);

}
