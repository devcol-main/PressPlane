using UnityEngine;


[RequireComponent(typeof(LocalSaveLoad))]
//[RequireComponent(typeof(GoogleCloudSaveSystem))]
public class SaveLoadManager : MonoBehaviour
{

    private LocalSaveLoad localSaveLoad;
    //private GoogleCloudSaveSystem googleCloudSaveSystem;
    // 

    //
    private string fileName = "SaveData.text";
    public static SaveLoadManager Instance;
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

    //
    void OnEnable()
    {
        Referencing();
    }

    void Start()
    {


    }

    public void Referencing()
    {
        localSaveLoad = FindFirstObjectByType<LocalSaveLoad>();

    }

    public void Save()
    {
        SaveDataCollection saveDataCollection = new SaveDataCollection();
        PopulateSaveData(saveDataCollection);

        Debug.Log("Save Local");
        localSaveLoad.Save(fileName, saveDataCollection);

        // if (GameManager.Instance.OnOnline)
        // {
        //     // if Andoird & Online
        //     Debug.Log("Save online");
        //     googleCloudSaveSystem.Save(fileName, saveDataCollection);

        // }
        // else
        // {

        //     // if local!
        //     Debug.Log("Save Local");
        //     localSaveSystem.Save(fileName, saveDataCollection);

        // }
    }


    public void Load()
    {

        Debug.Log("Load START");

        SaveDataCollection saveDataCollection = new SaveDataCollection();

        Debug.Log("Load Local");
        localSaveLoad.Load(fileName, saveDataCollection);

        

        // if(GameManager.Instance.OnOnline)
        // {
        //     // if Andorid & Online
        //     Debug.Log("Load Online");
        //     googleCloudSaveSystem.Load(fileName, saveDataCollection);

        // }
        // else
        // {
        //     // if Local!
        //     Debug.Log("Load Local");
        //     localSaveSystem.Load(fileName, saveDataCollection);



        // }

    }

    // firstime & after reset
    public void InitialLoad(SaveDataCollection saveDataCollection)
    {
        Debug.Log("InitialLoad");

    }

    // =========

    public void PopulateSaveData(SaveDataCollection saveDataCollection)
    {

        Debug.Log("PopulateSaveData from SaveSystem");

        //// Game Setting
        // Timer
        //timer.PopulateSaveData(saveDataCollection);

        GameData.Instance.PopulateSaveData(saveDataCollection);

    }

    public void LoadFromSaveData(SaveDataCollection saveDataCollection)
    {
        /*
        highScore = saveDataCollection.highScore;
        //debugText.text = "Load Done";
        */

        Debug.Log("LoadFromSaveData");

        //// GameSetting
        // Timer
        //timer.LoadFromSaveData(saveDataCollection);

        GameData.Instance.LoadFromSaveData(saveDataCollection);

    }



}
