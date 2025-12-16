using UnityEngine;
using Unity.Services.Authentication;
using System;
using System.Threading.Tasks;
using Unity.Services.Core;
using UnityEngine.SocialPlatforms;




#if UNITY_ANDROID || UNITY_EDITOR
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi.Events;

#endif

public class GPGSManager : MonoBehaviour
{

#if UNITY_ANDROID //|| UNITY_EDITOR
    public static GPGSManager Instance { get; private set; }

    /*
    // if wants to use GPGSManager without MonoBehaviour
    private static GPGSManager instance = new GPGSManager();
    public static GPGSManager Instance => instance;
    */

    PlayGamesPlatform PGP => PlayGamesPlatform.Instance;
    //ISavedGameClient SavedGameClient => PlayGamesPlatform.Instance.SavedGame;
    //IEventsClient Events => PlayGamesPlatform.Instance.Events;

    private string googlePlayGamesToken;
    
    public bool IsAuthenticated {get; private set;}
    private async void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(transform.root.gameObject);

            //

            // for debug
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();


            LoginGooglePlayGames();
        }


    }

    #region Google Login

    public void LoginGooglePlayGames()
    {
        PGP.Authenticate((status) =>
        {
            if (status == SignInStatus.Success)
            {
                Debug.Log("Login with GooglePlayGames Successful");

                PGP.RequestServerSideAccess(forceRefreshToken: true, code =>
                {
                    Debug.Log("Authorization code: " + code);
                    googlePlayGamesToken = code;

                });

                //
                IsAuthenticated = true;


            }
            else
            {
                Debug.Log($"GooglePlayGames login failed, : {status}");

                //
                IsAuthenticated = false;
            }
        });
    }

    // when login google play games with intention
    public void StartSignInWithGooglePlayGames()
    {
        if (!PGP.IsAuthenticated())
        {
            Debug.LogWarning("Not yet authenticated with GooglePlayGames / Attempting Login Again...");
            LoginGooglePlayGames();
            return;
        }

        SignInOrLinkWithGooglePlayGames();
    }

    private async void SignInOrLinkWithGooglePlayGames()
    {
        if (string.IsNullOrEmpty(googlePlayGamesToken))
        {
            Debug.LogWarning("Authorization code is null or empty");
            return;
        }

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            // new player
            await SignInWithGooglePlayGamesAsync(googlePlayGamesToken);
        }
        else
        {
            // existing player
            await LinkWithGooglePlayGamesAsync(googlePlayGamesToken);
        }
    }

    // new player
    private async Task SignInWithGooglePlayGamesAsync(string authCode)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);
            Debug.Log("Signin Success");
        }
        catch (AuthenticationException e)
        {

            Debug.LogException(e);
        }
        catch (RequestFailedException e)
        {
            Debug.LogException(e);
        }
    }

    // existing player
    private async Task LinkWithGooglePlayGamesAsync(string authCode)
    {
        try
        {
            await AuthenticationService.Instance.LinkWithGooglePlayGamesAsync(authCode);
            Debug.Log("Link Success");
        }
        catch (AuthenticationException e) when (e.ErrorCode == AuthenticationErrorCodes.AccountAlreadyLinked)
        {
            Debug.LogWarning("This user is already linked with another account. Log in Instead");
        }
        catch (AuthenticationException e)
        {
            Debug.LogException(e);
        }
        catch (RequestFailedException e)
        {
            Debug.LogException(e);
        }
    }
    #endregion


    #region Achievements
    public void ShowAchievements()
    {
        PGP.ShowAchievementsUI();

        // Social is obsolete
        //Social.ShowAchievementsUI();
    }

    public void UnlockAchievement(string gpgsid)
    {
        //PGP.UnlockAchievement(gpgsid, (bool success) => { });
        CheckAchievement(gpgsid);

    }

    public void CheckScoreAchievement(int currentScore)
    {
        if(10 == currentScore)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_reach_10);    
        }
        else if(20 == currentScore)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_reach_20);    
        }
        else if(30 == currentScore)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_reach_30);    
        }
        else if(40 == currentScore)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_reach_40);    
        }
        else if(50 == currentScore)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_reach_50);    
        }
        else if(100 == currentScore)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_reach_100);    
        }
    }
    public void CheckDeathAchievement(int numDeath)
    {
        if(10 == numDeath)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_gameover_10);    
        }
        else if(20 == numDeath)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_gameover_20);    
        }
        else if(50 == numDeath)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_gameover_50);    
        }
        else if(100 == numDeath)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_gameover_100);    
        }
    }

    internal void CheckAchievement(string gpgsid)
    {
        PGP.LoadAchievements(achievements =>
        {
            foreach (var achieve in achievements)
            {
                if(gpgsid == achieve.id)
                {
                    if(achieve.completed)
                    {
                        return;
                    }
                    else
                    {
                        PGP.UnlockAchievement(gpgsid, (bool success) => { });
                    }
                }
            }
        });
    }


    public void IncrementAchievement(string gpgsid, int increaseAmount)
    {
        PGP.IncrementAchievement(gpgsid, increaseAmount, (bool success) => { });
    }

    /*
 According to the expected behavior of Social.ReportProgress,
a value of 0.0f means the achievement is revealed
and a progress of 100.0f means the achievement is unlocked.

  To reveal an achievement that was previously hidden without unlocking it,
call Social.ReportProgress with a value of 0.0f.

 */
    public void RevealAchievement(string gpgsid)
    {
        PGP.ReportProgress(gpgsid, 0f, (bool success) => { });
    }

    #endregion

    #region Leaderboard
    public void ShowLeaderboard()
    {
        PGP.ShowLeaderboardUI();
    }

    public void AddHighScoreToLeaderboard(int highScore, string gpgsid = GPGSIds.leaderboard_leaderboard)
    {
        PGP.ReportScore(highScore, gpgsid, (bool success) => { });

    }
    #endregion

    #region Event
    public void IncrementEvent(string gpgsid, uint increaseAmount)
    {
        PGP.Events.IncrementEvent(gpgsid, increaseAmount);
    }
    #endregion

    #region Save & Load

    // Open Saved Game

    // ===== With SaveDataCollection =====
    public void Save(string fileName, SaveDataCollection saveDataCollection)
    {
        OpenDataFile(isSaving: true, fileName, saveDataCollection.ToJson());
    }

    public void Load(string fileName, SaveDataCollection saveDataCollection)
    {
        OpenDataFile(isSaving: false, fileName, saveDataCollection.ToJson());
    }

    // ===== With String =====
    public void Save(string fileName, string saveData)
    {
        OpenDataFile(isSaving: true, fileName, saveData);
    }

    //
    public void Load(string fileName)
    {
        OpenDataFile(isSaving: false, fileName, "");
    }


    private void OpenDataFile(bool isSaving, string fileName, string saveData, Action<bool> onCloudSaved = null, Action<bool, string> onCloudLoaded = null)
    {
        Debug.Log($"OpenDataFile, isSaving: {isSaving} | {fileName} , {saveData}");

        DataSource dataSource = DataSource.ReadCacheOrNetwork;
        ConflictResolutionStrategy conflictResolutionStrategy = ConflictResolutionStrategy.UseLastKnownGood;

        if (isSaving)
        {
            SavingDataFile(fileName, saveData, onCloudSaved, dataSource, conflictResolutionStrategy);
        }
        //loading
        else
        {
            LoadingDataFile(fileName, onCloudLoaded, dataSource, conflictResolutionStrategy);
        }
    }
    internal void SavingDataFile(string fileName, string saveData, Action<bool> onCloudSaved, DataSource dataSource, ConflictResolutionStrategy conflictResolutionStrategy)
    {
        PGP.SavedGame.OpenWithAutomaticConflictResolution(fileName, dataSource,
                    conflictResolutionStrategy, (status, game) =>
                    {
                        //Writting Save Files
                        if (status == SavedGameRequestStatus.Success)
                        {
                            Debug.Log("SavedGameRequestStatus.Success");

                            var update = new SavedGameMetadataUpdate.Builder().Build();
                            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(saveData);
                            PGP.SavedGame.CommitUpdate(game, update, bytes, (status2, game2) =>
                            {
                                onCloudSaved?.Invoke(status2 == SavedGameRequestStatus.Success);
                            });

                        }
                        else
                        {
                            Debug.LogWarning("SavedGameRequestStatus Failed");
                            // handle error
                            onCloudSaved?.Invoke(false);
                        }
                    });
    }

    internal void LoadingDataFile(string fileName, Action<bool, string> onCloudLoaded, DataSource dataSource, ConflictResolutionStrategy conflictResolutionStrategy)
    {
        PGP.SavedGame.OpenWithAutomaticConflictResolution(fileName, dataSource,
                    conflictResolutionStrategy, (status, game) =>
                    {
                        if (status == SavedGameRequestStatus.Success)
                        {
                            PGP.SavedGame.ReadBinaryData(game, (status2, loadedData) =>
                            {
                                string data = System.Text.Encoding.UTF8.GetString(loadedData);
                                onCloudLoaded?.Invoke(true, data);

                                //saveDataCollection.LoadFromJson(data);
                                //SaveLoadManager.Instance.LoadFromSaveData(saveDataCollection);

                                Debug.Log($"Load Data {fileName} from cloud successfully");


                            });

                        }
                        else if (status == SavedGameRequestStatus.AuthenticationError)
                        {
                            onCloudLoaded?.Invoke(false, "AuthenticationError");

                        }
                        else if (status == SavedGameRequestStatus.BadInputError)
                        {
                            onCloudLoaded?.Invoke(false, "BadInputError");

                        }
                        else if (status == SavedGameRequestStatus.InternalError)
                        {
                            onCloudLoaded?.Invoke(false, "InternalError");

                        }
                        else if (status == SavedGameRequestStatus.TimeoutError)
                        {
                            onCloudLoaded?.Invoke(false, "TimeoutError");

                        }
                    });
    }

    
    
    /*

    private void OpenSavedGame(string fileName)
    {
        // OpenWithAutomaticConflictResolution | OpenWithManualConflictResolution

        PGP.SavedGame.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLastKnownGood, OnSavedGameOpenedToSave);

    }
    internal void OnSavedGameOpenedToSave(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            byte[] myData = System.Text.Encoding.ASCII.GetBytes(num.ToString());

            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
            builder = builder.WithUpdatedDescription("Saved game at " + DateTime.Now);

            SavedGameMetadataUpdate updatedMetadata = builder.Build();

            //
            PGP.SavedGame.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);

        }
        else
        {
            // handle error
        }
    }
    internal void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            Debug.Log("Success to save to the cloud");
        }
        else
        {
            // handle error
            Debug.Log("Failed to save to the cloud");
        }
    }

    internal void OnSavedGameOpenedToLoad(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
        }
        else
        {
            // handle error
        }
    }
    */



    // Delete
    public void DeleteSavedGame(string fileName)
    {
        //SavedGameClient.OpenWithAutomaticConflictResolution(saveFile, DataSource.ReadCacheOrNetwork,   ConflictResolutionStrategy.UseLastKnownGood, DeleteSavedGame); 
        PGP.SavedGame.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork,
        ConflictResolutionStrategy.UseLastKnownGood, DeleteSavedGame);
    }
    internal void DeleteSavedGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("SavedGameRequestStatus.Success");
            PGP.SavedGame.Delete(game);
            /*
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.Delete(game);
            */

        }
        else
        {
            // handle error
            Debug.LogWarning("SavedGameRequestStatus failed");
        }

    }

    // show Saved Files
    public void ShowSavedFiles()
    {
        Debug.Log("ShowSavedFiles");

        uint maxNumToDisplay = 5;
        bool allowCreateNew = false;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelected);
    }

    internal void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {
            // handle selected game save
            Load(game.Filename);            
        }
        else
        {
            // handle cancel or error
        }
    }
    // ==
    #endregion

#endif

}
