using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// at SaveLoadManager
public class Timer : MonoBehaviour, ISaveable
{
	// ref URL
	private const string url = "www.google.com";

	[Header("Debug Display")]
	[SerializeField][ReadOnly] private int currentTime;

	[SerializeField][ReadOnly] private int initialPlayedTime;
	[SerializeField][ReadOnly] private int lastPlayedTime;
	private int timePassedSinceLastPlayed;
	private int totalPlayedTime;
	private int longestPlayedTime;

	// normal Scene
	private int normalSceneTotalPlayedTime;
	private int normalSceneLastPlayedTime;

	void Start()
	{
		StartCoroutine(AwakeTimer());
	}

	# region Only Once
	public void InitalTimer()
	{
		StartCoroutine(InitialTimer());
	}

	IEnumerator InitialTimer()
	{
		UnityWebRequest request = new UnityWebRequest();
		using (request = UnityWebRequest.Get(url))
		{
			yield return request.SendWebRequest();
			BaseTimerMethod(request);
		}

		Initiallize();
		
	}

	internal void Initiallize()
	{
		initialPlayedTime = currentTime;

		//
		timePassedSinceLastPlayed = 0;
		totalPlayedTime = 0;
		longestPlayedTime = 0;

		// normal Scene
		normalSceneTotalPlayedTime = 0;
		normalSceneLastPlayedTime = 0;
	}

	#endregion

	IEnumerator AwakeTimer()
	{
		UnityWebRequest request = new UnityWebRequest();
		using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            BaseTimerMethod(request);
        }
    }

    internal void BaseTimerMethod(UnityWebRequest request)
    {
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string date = request.GetResponseHeader("date");

            DateTime dateTime = DateTime.Parse(date).ToUniversalTime();
            TimeSpan timeStamp = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);

            currentTime = (int)timeStamp.TotalSeconds;    

			Debug.Log("dateTime (UniversalTime) : " + dateTime);			    

        }
    }


    public void PopulateSaveData(SaveDataCollection saveDataCollection)
	{
		//Debug.Log("PopulateSaveData : " + gameObject.name);
		saveDataCollection.timeData.initialPlayedTime = initialPlayedTime;
		saveDataCollection.timeData.lastPlayedTime = currentTime;
	}

	public void LoadFromSaveData(SaveDataCollection saveDataCollection)
	{
		//Debug.Log("LoadFromSaveData : " + gameObject.name);

		initialPlayedTime = saveDataCollection.timeData.initialPlayedTime;
		lastPlayedTime = saveDataCollection.timeData.lastPlayedTime;
	}
}
