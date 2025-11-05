
using UnityEngine;

using System.IO;
using System;

//using static SaveDataCollection;
//

public class LocalSaveLoad : MonoBehaviour
{
    //[SerializeField]
    private string saveFolderName = "/Save/";

    private string saveFolderPath;
    private string savePath;

    private SaveLoadManager saveLoadManager;

    private void Awake()
    {
        saveLoadManager = FindAnyObjectByType<SaveLoadManager>();

        saveFolderPath = Application.persistentDataPath + saveFolderName;
    }


    public void Save(string fileName, SaveDataCollection saveDataCollection)
    {

        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }

        savePath = Path.Combine(saveFolderPath, fileName);

        print("!!!Save path: " + savePath);

        try
        {
            File.WriteAllText(savePath, saveDataCollection.ToJson());
            print("Save Done");

        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to write to {savePath} with exception {e}");

        }

    }

    public void Load(string fileName, SaveDataCollection saveDataCollection)
    {
        savePath = Path.Combine(saveFolderPath, fileName);

        string json;

        /*

        if (File.Exists(savePath))
        {
            json = File.ReadAllText(savePath);

        }
        else
        {

            print("from load Else");

            // original            
            File.WriteAllText(savePath, saveDataCollection.ToJson());
            json = "";
        }
        saveDataCollection.LoadFromJson(json);
        saveSystem.LoadFromSaveData(saveDataCollection);
        */

        try
        {
            json = File.ReadAllText(savePath);

            saveDataCollection.LoadFromJson(json);
            saveLoadManager.LoadFromSaveData(saveDataCollection);


        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {savePath} with exception {e}");
            File.WriteAllText(savePath, saveDataCollection.ToJson());

            json = "";

            saveDataCollection.LoadFromJson(json);

            saveLoadManager.InitialLoad(saveDataCollection);

        }


    }

    public void DeleteSavedData(string fileName)
    {
        
        savePath = Path.Combine(saveFolderPath, fileName);

        if(File.Exists(savePath))
        {
            File.Delete(savePath);
        }
        else
        {
            Debug.Log("Failed Deleting, file not exist");
        }


    }


}

