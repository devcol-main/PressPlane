
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

    void Awake()
    {
        saveFolderPath = Application.persistentDataPath + saveFolderName;
    }


    void Start()
    {
        saveLoadManager = FindAnyObjectByType<SaveLoadManager>();

        
    }


    public void Save(string fileName, SaveDataCollection saveDataCollection)
    {

        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }

        savePath = Path.Combine(saveFolderPath, fileName);

        Debug.Log("!!!Save path: " + savePath);

        try
        {
            File.WriteAllText(savePath, saveDataCollection.ToJson());
            Debug.Log($"Save Data {fileName} to {savePath} complete");

            //Debug.Log("Save Done");

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

        try
        {
            json = File.ReadAllText(savePath);

            saveDataCollection.LoadFromJson(json);
            saveLoadManager.LoadFromSaveData(saveDataCollection);

            Debug.Log($"Load Data {fileName} from {savePath} successfully");

        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to read from {savePath} with exception {e}");
            File.WriteAllText(savePath, saveDataCollection.ToJson());

            json = "";

            saveDataCollection.LoadFromJson(json);
            saveLoadManager.InitialLoad(saveDataCollection);

        }


    }

    public void DeleteSavedData(string fileName)
    {
        
        savePath = Path.Combine(saveFolderPath, fileName);

        try
        {
            File.Delete(savePath);
            Debug.Log($"Data {fileName} from {savePath} deleted successfully");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to delete {fileName} from {savePath} with exception {e}");
        }
        /*
        if(File.Exists(savePath))
        {
            File.Delete(savePath);
        }
        else
        {
            Debug.Log("Failed Deleting, file not exist");
        }
        */

    }


}

