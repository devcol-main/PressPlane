using System;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

using GoogleMobileAds.Common; 
using UnityEngine.Events;// AppState


//[AddComponentMenu("GoogleMobileAds/Samples/AdController")]
public class Admob : MonoBehaviour
{

#if DEBUG
    [SerializeField] private bool isTestMode = true;

# else

    [SerializeField] private bool isTestMode = false;

#endif


    [SerializeField] private bool isBannerAdsStarOnAwake = true;

    [Header ("Ads Events For Game")]
    //public UnityAction interRewardEvent;
    public UnityAction rewardedEndEvent;


#if UNITY_ANDROID
    private string appOpenTestId                 = "ca-app-pub-3940256099942544/9257395921";
    private string adpativeBannerTestId          = "ca-app-pub-3940256099942544/9214589741";
    private string bannerTestId                  = "ca-app-pub-3940256099942544/6300978111";
    private string interstitialTestId            = "ca-app-pub-3940256099942544/1033173712";
    private string rewardedTestId                = "ca-app-pub-3940256099942544/5224354917";
    private string rewardedInterstitialTestId    = "ca-app-pub-3940256099942544/5354046379";
    private string nativeOverlayTestId           = "ca-app-pub-3940256099942544/2247696110";
    private string nativeVideoTestId             = "ca-app-pub-3940256099942544/1044960115";



#elif UNITY_IPHONE
    private string appOpenTestId                 = " ca-app-pub-3940256099942544/5575463023";
    private string bannerTestID                  = "ca-app-pub-3940256099942544/2934735716";
    private string interstitialTestID            = "ca-app-pub-3940256099942544/4411468910";
    private string rewardedTestId                = "ca-app-pub-3940256099942544/1712485313";
    private string nativeOverlayTestId           = "ca-app-pub-3940256099942544/3986624511";

    private string rewardedInterstitialTestId    = "ca-app-pub-3940256099942544/6978759866";


#else
    private string appOpenTestId                = "unused";

    private string bannerTestId                 = "unused";
    private string interstitialTestId           = "unused";
    private string rewardedTestId               = "unused";

    private string nativeOverlayTestId          = "unused";

    private string rewardedInterstitialTestId   = "unused";

#endif
    private string appOpenId                    = "?";
    
    private string bannerId                     = "?";
    private string interstitialId               = "?";
    private string rewardedId                   = "ca-app-pub-4892249986723209/7036956642";
    private string nativeOverlayId              = "?";
    private string rewardedInterstitialId       = "?";

    // ca-app-pub-4892249986723209~2929178912
    
    
    // re: ca-app-pub-4892249986723209/7710538784
    

    
    private List<string> testDeviceIdsList = new List<string>
    {
        // "YOUR_TEST_DEVICE_ID" // Replace with your actual device ID from the console log
        // Real Phone
        "4272fe0a-b514-4d82-a732-10ca1ad1abe1", // s23 fe | Android | Shake
        
        // AVD
        "d2bb0218-fa9e-4bcc-ae0d-1abd02aaf388" // AVD Medium Phone Test 35 API | Android | Shake

    };

    private void Awake()
    {
        
        // Use the AppStateEventNotifier to listen to application open/close events.
        // This is used to launch the loaded ad when we open the app.
        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
    }

    private void OnDestroy()
    {
        // Always unlisten to events when complete.
        AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
    }

    private void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        //MobileAds.Initialize(initStatus => { });

        MobileAds.Initialize(initStatus =>
        {
            // Combine all later
            /*
            LoadAppOpenAd();

            if(isBannerAdsStarOnAwake)
                LoadBannerAd();

            //
            LoadAdaptiveBannerAd();

            LoadInterstitialAd();

            //
            LoadRewardedAd();
            //RewardedEndEvent

            //
            LoadNativeOverlayAd();

            //interRewardEvent += () => GiveInterReward();
            rewardedEndEvent += () => GiveRewardedReward();
            */

            LoadRewardedAd();
            //rewardedEndEvent += () => GiveRewardedReward();


        });

        ConfigureTestDevices();

    }

    private void ConfigureTestDevices()
    {
    

        RequestConfiguration requestConfiguration = new RequestConfiguration
        {
            TestDeviceIds = testDeviceIdsList
        };

        // RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
        //     .SetTestDeviceIds(testDeviceIds)
        //     .build();

        MobileAds.SetRequestConfiguration(requestConfiguration);
        
        Debug.Log("AdMob configured with test devices.");
    }


    private void OnAppStateChanged(AppState state)
    {
        Debug.Log("App State changed to : " + state);

        // if the app is Foregrounded and the ad is available, show it.
        if (state == AppState.Foreground)
        {
            // ShowAppOpenAd();

            if (IsAdAvailable)
            {
                ShowAppOpenAd();
            }


        }
    }

    // Preload & Since ads expire after an hour, you should clear this cache and reload with new ads every hour.


    /*
     Tip: You can use ad load calls to build up a cache of preloaded ads
    before you intend to show them,
    so that ads can be shown with zero latency when needed.

    Since ads expire after an hour,
    you should clear this cache and reload with new ads every hour.
     */

    // ==========================


    #region AppOpen Ads

    /*
     The preferred way to use app open ads on cold starts
    is to use a loading screen to load your game or app assets,
    and to only show the ad from the loading screen.
    If your app has completed loading
    and has sent the user to the main content of your app,
    do not show the ad.
     */

    /*
    - If you have a loading screen under the app open ad
    and your loading screen completes loading before the ad is dismissed,
    dismiss your loading screen in the
    OnAdDidDismissFullScreenContent event handler.


    - On the iOS platform,
    AppStateEventNotifier instantiates an AppStateEventClient GameObject.
    This GameObject is required for events to fire so don't destroy it.
    Events stop firing if the GameObject is destroyed.
     */

    private AppOpenAd _appOpenAd;

    // change name
    private DateTime _expireTime;

    public bool IsAdAvailable
    {
        get
        {
            return _appOpenAd != null;

            /*
            return _appOpenAd != null
                   && _appOpenAd.IsLoaded()
                   && DateTime.Now < _expireTime;
            */
        }
    }

    private void LoadAppOpenAd()
    {
        // Clean up the old ad before loading a new one.
        if (_appOpenAd != null)
        {
            _appOpenAd.Destroy();
            _appOpenAd = null;
        }

        Debug.Log("Loading the app open ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        string appOpenAdUnitId = isTestMode ? appOpenTestId : appOpenId;


        // send the request to load the ad.
        AppOpenAd.Load(appOpenAdUnitId, adRequest,
            (AppOpenAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
                {
                    Debug.LogError("app open ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("App open ad loaded with response : "
                          + ad.GetResponseInfo());

                // App open ads can be preloaded for up to 4 hours.
                //_expireTime = DateTime.Now + TimeSpan.FromHours(4);

                _appOpenAd = ad;
                AppOpenEventHandlers(_appOpenAd);
                AppOpenReloadHandler(_appOpenAd);
            });
    }

    private void AppOpenEventHandlers(AppOpenAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("App open ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("App open ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("App open ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("App open ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("App open ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    private void AppOpenReloadHandler(AppOpenAd ad)
    {
    // Raised when the ad closed full screen content.
      ad.OnAdFullScreenContentClosed += () =>
    {
            Debug.Log("App open ad full screen content closed.");
        //
        _appOpenAd.Destroy();


        // Reload the ad so that we can show another as soon as possible.
        LoadAppOpenAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadAppOpenAd();
        };
    }

    public void ShowAppOpenAd()
    {
        if (_appOpenAd != null && _appOpenAd.CanShowAd())
        {
            Debug.Log("Showing app open ad.");
            _appOpenAd.Show();
        }
        else
        {
            Debug.LogError("App open ad is not ready yet.");
        }
    }

    #endregion


    // ==========================


    #region Banner Ads


    BannerView _bannerView;

    /// Creates a 320x50 banner view at top of the screen.
    private void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyAd();
        }

        // Create a 320x50 banner at top of the screen
        //_bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Top);

        /*
        int width = Screen.width;

        Debug.Log("width: " + width);

        AdSize adSize = new AdSize(320, 100);

        _bannerView = new BannerView(_adUnitId, adSize, AdPosition.Top);
        */

          
        string bannerAdUnitId = isTestMode ? bannerTestId : bannerId;

        Debug.Log("adunitId: " + bannerAdUnitId);

        _bannerView = new BannerView(bannerAdUnitId
            , AdSize.Banner, AdPosition.Top);

        
    }

    /*
        Size in dp (WxH)	     Description	        Availability	                     AdSize constant
        320x50	                 Standard Banner	    Phones and Tablets	                 BANNER
        320x100	                 Large Banner	        Phones and Tablets	                 LARGE_BANNER
        300x250	                 IAB Medium Rectangle	Phones and Tablets	                 MEDIUM_RECTANGLE
        468x60	                 IAB Full-Size Banner	Tablets	                             FULL_BANNER
        728x90	                 IAB Leaderboard	    Tablets	                             LEADERBOARD
        Provided width x         Adaptive height	    Adaptive banner	Phones and Tablets	 N/A
        Screen width x 32|50|90	Smart banner	        Phones and Tablets	                 SMART_BANNER
     
     */

    /// Creates the banner view and loads a banner ad.
    private void LoadBannerAd()
    {
        // create an instance of a banner view first.
        if (_bannerView == null)
        {
            CreateBannerView();
        }

        //
        ListenToAdEvents();

        //

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);


    }

    public void HideBannerAd()
    {
        if(null != _bannerView)
            _bannerView.Hide();
    }

    public void UnHideBannderAd()
    {
        if(null == _bannerView)
        {
            LoadBannerAd();
        }
        else
            _bannerView.Show();
    }

    //
    BannerView _adaptiveBannerView;
    private void LoadAdaptiveBannerAd()
    {
        CreateAdaptiveBannerView();

        //ListenToAdEvents();

        var adRequest = new AdRequest();
        _adaptiveBannerView.LoadAd(adRequest);

        

        
    }

    private void CreateAdaptiveBannerView()
    {
                // Get the device safe width in density-independent pixels.
        int deviceWidth = MobileAds.Utils.GetDeviceSafeWidth();

        // Define the anchored adaptive ad size.
        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(deviceWidth);

        // Create an anchored adaptive banner view.
        _adaptiveBannerView = new BannerView(adpativeBannerTestId, adaptiveSize, AdPosition.Bottom);

    }

    /// listen to events the banner view may raise.
    private void ListenToAdEvents()
    {
        // Raised when an ad is loaded into the banner view.
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _bannerView.GetResponseInfo());
        };

        // Raised when an ad fails to load into the banner view.
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }


    /// Destroys the banner view.
    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    #endregion

    // ==========================


    #region Interstitial Ads


    private InterstitialAd _interstitialAd;

    /// Loads the interstitial ad.
    private void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();


        string interstitialAdUnitId = isTestMode ? interstitialTestId : interstitialId;


        // send the request to load the ad.
        InterstitialAd.Load(interstitialAdUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;


                InterstitialEventHandlers(_interstitialAd);
                InterstitialReloadHandler(_interstitialAd);

            });
    }

    /// Shows the interstitial ad.
    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            LoadInterstitialAd();
        }
    }

    private void InterstitialEventHandlers(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }


    private void InterstitialReloadHandler(InterstitialAd interstitialAd)
    {
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
    {
        Debug.Log("= Interstitial Ad full screen content closed. =");
        // Reward here if you wants to

        //
        Debug.Log("_interstitialAd.Destroy.");

        _interstitialAd.Destroy();


        // Reload the ad so that we can show another as soon as possible.
        LoadInterstitialAd();
        };



        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
    }
    #endregion


    // ==========================

    #region Rewarded Ads

    private RewardedAd _rewardedAd;

    /// Loads the rewarded ad.
    private void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        string rewardedAdUnitId = isTestMode ? rewardedTestId : rewardedId;


        // send the request to load the ad.
        RewardedAd.Load(rewardedAdUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;

                RewardedEventHandlers(_rewardedAd);
                RewardedReloadHandler(_rewardedAd);

            });
    }
    /*
    Warning: Attempting to load a new ad from the ad request completion block
    when an ad failed to load is strongly discouraged.
    If you must load an ad from the ad request completion block,
    limit ad load retries to avoid continuous failed ad
    requests in situations such as limited network connectivity.

     */


    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));

                Debug.Log("Rewardad DP done giving reward to user");
                
                // if want extra steps
                // if(null != rewardedEndEvent)
                //     rewardedEndEvent.Invoke();
                _rewardedAd.OnAdFullScreenContentClosed += GiveRewardedReward;
                //

            });
        }
        //
        else
        {
            Debug.Log("ShowRewardedAd failed");

            Debug.Log("at ShowRewardedAd Load start");
            LoadRewardedAd();
        }
    }
    public void GiveRewardedReward()
    {
        Debug.Log("!!!!! GiveRewardedReward!!!!!");

        GameManager.Instance.OnSpineWheel();
    }


    private void RewardedEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("closed. at RewardedEventHandlers");

            Debug.Log("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    private void RewardedReloadHandler(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("closed. at RewardedReloadHandler");


            Debug.Log("Rewarded Ad full screen content closed.");

            //
            _rewardedAd.Destroy();

            // Reload the ad so that we can show another as soon as possible.
            Debug.Log("RE- LoadRewardedAd.");

            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);

            //_rewardedAd.Destroy();

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
    }

    #endregion


    // ==========================
    #region Native Overlay Ads
    private NativeOverlayAd _nativeOverlayAd;

    //
    public GameObject AdLoadedStatus;
    public RectTransform AdPlacmentTarget;


    public NativeAdOptions NativeOverlayOption = new NativeAdOptions
    {
        AdChoicesPlacement = AdChoicesPlacement.TopRightCorner,
        MediaAspectRatio = MediaAspectRatio.Any,
    };

    public NativeTemplateStyle NativeOverlayStyle = new NativeTemplateStyle
    {
        TemplateId = NativeTemplateId.Medium,
    };


    //
    private void LoadNativeOverlayAd()
    {
        // Clean up the old ad before loading a new one.
        if (_nativeOverlayAd != null)
        {
            DestroyAd();
        }

        Debug.Log("Loading native overlay ad.");

        // Create a request used to load the ad.
        var adRequest = new AdRequest();

        string nativeOverlayAdUnitId = isTestMode ? nativeOverlayTestId : nativeOverlayId;
        
        NativeOverlayAd.Load(nativeOverlayAdUnitId, adRequest, NativeOverlayOption,
                (NativeOverlayAd ad, LoadAdError error) =>
                {
                    // If the operation failed with a reason.
                    if (error != null)
                    {
                        Debug.LogError("Native Overlay ad failed to load an ad with error : " + error);
                        return;
                    }
                    // If the operation failed for unknown reasons.
                    // This is an unexpected error, please report this bug if it happens.
                    if (ad == null)
                    {
                        Debug.LogError("Unexpected error: Native Overlay ad load event fired with " +
                        " null ad and null error.");
                        return;
                    }

                    // The operation completed successfully.
                    Debug.Log("Native Overlay ad loaded with response : " + ad.GetResponseInfo());
                    _nativeOverlayAd = ad;

                    // Register to ad events to extend functionality.
                    NativeOverlayEventHandlers(_nativeOverlayAd);

                    // Inform the UI that the ad is ready.
                    AdLoadedStatus?.SetActive(true);
                });


    }

    private void NativeOverlayEventHandlers(NativeOverlayAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Native Overlay ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Native Overlay ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Native Overlay ad was clicked.");
        };
        // Raised when the ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Native Overlay ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Native Overlay ad full screen content closed.");
        };
    }


    private void RenderNativeOverlayAd()
    {
        if (_nativeOverlayAd != null)
        {
            Debug.Log("Rendering Native Overlay ad.");

            // Renders a native overlay ad at the default size
            // and anchored to the bottom of the screne.
            _nativeOverlayAd.RenderTemplate(NativeOverlayStyle, AdPosition.Bottom);
        }
    }

    public void ShowNativeOverlayAd()
    {
        if (_nativeOverlayAd != null)
        {
            Debug.Log("Showing Native Overlay ad.");
            _nativeOverlayAd.Show();
        }
    }

    public void HideNativeOverlayAd()
    {
        if (_nativeOverlayAd != null)
        {
            Debug.Log("Hiding Native Overlay ad.");
            _nativeOverlayAd.Hide();
        }
    }

    public void DestroyNativeOverlayAd()
    {
        if (_nativeOverlayAd != null)
        {
            Debug.Log("Destroying native overlay ad.");
            _nativeOverlayAd.Destroy();
            _nativeOverlayAd = null;
        }

        // Inform the UI that the ad is not ready.
        AdLoadedStatus?.SetActive(false);
    }

    public void LogResponseInfo()
    {
        if (_nativeOverlayAd != null)
        {
            var responseInfo = _nativeOverlayAd.GetResponseInfo();
            if (responseInfo != null)
            {
                Debug.Log(responseInfo);
            }
        }
    }

    #endregion

    // ==========================


    // as of 2025 May it's in beta
    #region RewardedInterstitial Ads
    #endregion
}
