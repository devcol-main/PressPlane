using UnityEngine;


[RequireComponent(typeof(LocalSaveLoad))]
//[RequireComponent(typeof(GoogleCloudSaveSystem))]
public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;

    private LocalSaveLoad localSaveLoad;
    //private GoogleCloudSaveSystem googleCloudSaveSystem;
    
    //
    private UISoundController uiSoundController;
    private Score score;
    private Timer timer;

    //
    //private string fileName = "SaveData.text";
    private string fileName = "SaveData.json";

    public bool IsOnline { get; set;}
    
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

#if UNITY_ANDROID
        if(GPGSManager.Instance.IsAuthenticated)
        {
            IsOnline = true;
        }
        else
        {
            IsOnline = false;
        }

#endif        
    }

    public void Referencing()
    {
        localSaveLoad = FindFirstObjectByType<LocalSaveLoad>();

        uiSoundController = FindFirstObjectByType<UISoundController>();
        score = FindFirstObjectByType<Score>();
        timer = FindFirstObjectByType<Timer>();
    }

    public void Save()
    {
        SaveDataCollection saveDataCollection = new SaveDataCollection();
        PopulateSaveData(saveDataCollection);

#if UNITY_EDITOR
        Debug.Log("Save Local");
        localSaveLoad.Save(fileName, saveDataCollection);

#endif

#if UNITY_ANDROID
        if(IsOnline)
        {
            
            GPGSManager.Instance.Save(fileName,saveDataCollection);
        }
        else
        {
            Debug.LogWarning("Saving Local!");
            localSaveLoad.Save(fileName, saveDataCollection);
        }
#endif
    }


    public void Load()
    {
        SaveDataCollection saveDataCollection = new SaveDataCollection();

        Debug.Log("Load START from " + gameObject.name);

#if UNITY_EDITOR

        Debug.Log("Load Local");
        localSaveLoad.Load(fileName, saveDataCollection);
#endif

#if UNITY_ANDROID
        if(IsOnline)
        {
            
            GPGSManager.Instance.Load(fileName,saveDataCollection);
        }
        else
        {
            Debug.LogWarning("Loading Local!");
            localSaveLoad.Load(fileName, saveDataCollection);
        }
#endif

    }

    public void Delete()
    {
        

#if UNITY_EDITOR
        Debug.Log("Deleting " + fileName + " from local");
        localSaveLoad.DeleteSavedData(fileName);
#endif

#if UNITY_ANDROID
    if(IsOnline)
        {
            Debug.Log("Deleting " + fileName + " from Online");
            GPGSManager.Instance.DeleteSavedGame(fileName);
        }
        else
        {
            Debug.LogWarning("Deleting " + fileName + " from local");
            localSaveLoad.DeleteSavedData(fileName);
        }
#endif


        
    }

    public void Delete(string deletingFileName)
    {
        Debug.Log("Delete from " + this.gameObject.name);

        localSaveLoad.DeleteSavedData(deletingFileName);

        GPGSManager.Instance.DeleteSavedGame(deletingFileName);
    }




    public void PopulateSaveData(SaveDataCollection saveDataCollection)
    {

        //Debug.Log("PopulateSaveDatas from" + this.gameObject.name);

        uiSoundController.PopulateSaveData(saveDataCollection);
        score.PopulateSaveData(saveDataCollection);
        timer.PopulateSaveData(saveDataCollection);




    }

    public void LoadFromSaveData(SaveDataCollection saveDataCollection)
    {
        //Debug.Log("LoadFromSaveData from" + this.gameObject.name);

        uiSoundController.LoadFromSaveData(saveDataCollection);
        score.LoadFromSaveData(saveDataCollection);
        timer.LoadFromSaveData(saveDataCollection);
    }

    // firstime & after reset
    public void InitialLoad(SaveDataCollection saveDataCollection)
    {
        Debug.Log("InitialLoad");

        //timer.SetInitialTime();
  
        
    }

    #if UNITY_EDITOR
#endif

#if UNITY_ANDROID
#endif



}
