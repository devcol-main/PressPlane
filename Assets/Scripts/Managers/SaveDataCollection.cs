
using System.Collections.Generic;
using UnityEngine;
//using System;

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
    public GameSystemTimeData gameSystemTimeData = new GameSystemTimeData();

    //

    
    /*
    [System.Serializable]
    public struct EnergyRefillTimedata
    {
        public int timeFullFillRequireTotal;
        
        
        public int timeTotal;
        public int timePoint;
        public int timePassed;

        public bool isEnergyRefillRunning;
        
    }

    public EnergyRefillTimedata energyRefillTimedata = new EnergyRefillTimedata();


    //
    [System.Serializable]
    public struct InventoryData
    {
        public int blockCollected;
        public int diamondCollected;

        public int energyCollected;
        public bool maxEnergyStatus;
    }

    public InventoryData inventoryData = new InventoryData();

    //
    [System.Serializable]
    public struct StageData
    {
        //
        public MainStageIndex mainStageIndex;
        public int subStageNum;

        public bool isStageOpen;

        public int stageStar;
        public int bestStarRecord;

    }

    public List<StageData> mainStageDatasList = new List<StageData>();

    public List<StageData> subStageDatasList = new List<StageData>();

    */
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
