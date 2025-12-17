
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveDataCollection 
{

    [System.Serializable]
    public struct TimeData
    {
        public int initialPlayedTime;
        public int totalPlayedTime;
        public int lastPlayedTime;
        public int timePassedSinceLastPlayed;

        //public int userLastInDateTime;
        //public int userLastOutDateTime;
        public int longestPlayedTime;

        // normal Scene
        public int normalSceneTotalPlayedTime;
        public int normalSceneLastPlayedTime;

    }

    [System.Serializable]
    public struct SettingSoundData
    {
        // Master 
        public bool isMasterVolumeOn;               

        public float masterVolume;
        public float masterVolumeSliderValue;

        // BGM

        public bool isBGMVolumeOn;
        public float bgmVolume;
        public float bgmVolumeSliderValue;

        // SFX

        public bool isSFXVolumeOn;
        public float sfxVolume;
        public float sfxVolumeSliderValue;

    }

    public TimeData timeData = new TimeData();
    public SettingSoundData settingSoundData = new SettingSoundData();
    //

    //=======================================================

    [System.Serializable]
    public struct NormalSceneData
    {
        public int numGamePlayed;
        public int numTotalEarnedScore;
        public int highScore;
        public int numDeath;
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
