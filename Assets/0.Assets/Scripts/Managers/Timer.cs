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

	private int tempInitialPlayedTime;

	void Start()
	{
		
		//StartCoroutine(AwakeTimerCoroutine());
		
	}

	public void SetInitialTime()
	{	
		//StartCoroutine(InitialTimerCoroutine());
	}



	// Debug InitialTimerCoroutine
	// // it exit at yield return request.SendWebRequest();
	IEnumerator InitialTimerCoroutine()
	{
		UnityWebRequest request = new UnityWebRequest();
		Debug.Log("InitialTimerCoroutine");
		using (request = UnityWebRequest.Get(url))
		{			
			yield return request.SendWebRequest();			

			Debug.Log("yield return request.SendWebRequest();	");
			if (request.result == UnityWebRequest.Result.Success)
			{
				string date = request.GetResponseHeader("date");

				DateTime dateTime = DateTime.Parse(date).ToUniversalTime();
				TimeSpan timeStamp = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);

				initialPlayedTime = (int)timeStamp.TotalSeconds;
				//currentTime = (int)timeStamp.TotalSeconds;
				Debug.Log("InitialTimer dateTime (UniversalTime) : " + dateTime);

			}
			else
			{
				Debug.Log(request.error);

			}

		}

	}

	// private void Initiallize()
	// {
	// 	//initialPlayedTime = currentTime;
	// 	Debug.Log("Initiallize from timer: " + initialPlayedTime);

	// 	//
	// 	timePassedSinceLastPlayed = 0;
	// 	totalPlayedTime = 0;
	// 	longestPlayedTime = 0;

	// 	// normal Scene
	// 	normalSceneTotalPlayedTime = 0;
	// 	normalSceneLastPlayedTime = 0;
	// }


	IEnumerator AwakeTimerCoroutine()
	{
		Debug.Log("AwakeTimerCoroutine");
		UnityWebRequest request = new UnityWebRequest();
		using (request = UnityWebRequest.Get(url))
		{
			yield return request.SendWebRequest();
			BaseTimerMethod(request);

			Debug.Log("AwakeTimerCoroutine : " + currentTime);
		}

		StartCoroutine(StartTimerCoroutine());
	}
	IEnumerator StartTimerCoroutine()
	{
		UnityWebRequest request = new UnityWebRequest();
		while (true)
		{
			using (request = UnityWebRequest.Get(url))
			{
				yield return request.SendWebRequest();
				yield return new WaitForSeconds(1f);
				BaseTimerMethod(request);

			}
		}
	}

	private void BaseTimerMethod(UnityWebRequest request)
	{
		//Debug.Log("BaseTimerMethod");
		if (request.result == UnityWebRequest.Result.Success)
		{
			string date = request.GetResponseHeader("date");

			DateTime dateTime = DateTime.Parse(date).ToUniversalTime();
			TimeSpan timeStamp = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);

			currentTime = (int)timeStamp.TotalSeconds;

			//Debug.Log("dateTime (UniversalTime) : " + dateTime);
		}
		else
		{
			Debug.Log(request.error);
		}
	}


	public void PopulateSaveData(SaveDataCollection saveDataCollection)
	{
		//Debug.Log("PopulateSaveData : " + gameObject.name);
		//saveDataCollection.timeData.initialPlayedTime = initialPlayedTime;
		//saveDataCollection.timeData.lastPlayedTime = currentTime;
	}

	public void LoadFromSaveData(SaveDataCollection saveDataCollection)
	{
		//Debug.Log("LoadFromSaveData : " + gameObject.name);

		//initialPlayedTime = saveDataCollection.timeData.initialPlayedTime;
		//lastPlayedTime = saveDataCollection.timeData.lastPlayedTime;
	}
}
