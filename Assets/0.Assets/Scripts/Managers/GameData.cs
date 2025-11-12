using UnityEngine;

public class GameData : MonoBehaviour, ISaveable
{
    public struct IngameData
    {
        public int HighScore;
        public int CurrentScore;
    }

    public static GameData Instance;

    public IngameData ingameData;

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
    }

    public void LoadFromSaveData(SaveDataCollection saveDataCollection)
    {
        Debug.Log("Game data-LoadFromSaveData");
        ingameData.HighScore = saveDataCollection.normalSceneData.HighScore;
    }
}
