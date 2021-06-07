
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds.Api;
//using AudienceNetwork;
using EasyUI.Dialogs;
//using GoogleMobileAdsMediationTestSuite.Api;

//using LionStudios;
//using LionStudios.Ads;
//using Facebook.Unity;

public class AdsMgr :BaseDelayMono
{

    public static AdsMgr current;
    //public GoogleMobileAds.Api.RewardedAd admobRewardedAd;
    //public LionStudios.Ads.RewardedAd _lionRewardAd;
    //public LionStudios.Ads.ShowAdRequest _lionShowAdRequest;

    //public static FBAdsMgr _fbAdsMgr;
    public static MaxAdsMgr _maxAdsMgr;

    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        //_fbAdsMgr = gameObject.AddComponent<FBAdsMgr>();
        _maxAdsMgr = gameObject.AddComponent<MaxAdsMgr>();
        //MobileAds.Initialize(initStatus =>
        //{
        //    RequestAdmobInter();
        //    RequestAdmobReward();
        //});


        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
            // AppLovin SDK is initialized, start loading ads
            //MaxSdk.ShowMediationDebugger();
        };
        MaxSdk.SetSdkKey("k2upCW6APxH-_BKb4-RhCFa5P-7DIvcU69czdb45Hv2v4FTV1viTw7mAV6P6-SMgAkcDyAA47sola-hn6TACWa");
        MaxSdk.InitializeSdk();

        //----Lion SDK----
        //MaxSdk.SetSdkKey("sCw0m-SM8caA9GeJVue8L9l8kKZhoI7pwcRr9IDuiKXarXcpFJFMAzLZvrN2X9gZB6vUUl7aZbSrZIDSAEuttM");
        //MaxSdk.InitializeSdk();
        //----Lion SDK--end

        //FB.Init();
        _maxAdsMgr.InitializeInterstitialAds();
        _maxAdsMgr.InitializeRewardedAds();

    }

    void InitApplovin()
    {


    }


    const string ADMO_TEST_AND_REWARD = "ca-app-pub-3940256099942544/5224354917";

    const string ADMOB_AND_REWARD_ID = "ca-app-pub-6770182166257156/6986962054";
    const string ADMOB_IOS_REWARD_ID = "";
    const string ADMOB_AND_INTER_ID = "ca-app-pub-6770182166257156/7954249744";
    const string ADMOB_IOS_INTER_ID = "";

    //public GoogleMobileAds.Api.InterstitialAd ADMOBinterstitial;

    public bool hasRewardReady()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }

        if(_maxAdsMgr !=null && _maxAdsMgr.IsRewardReady())
        {
            return true;
        }

        return false;

        //if (LionStudios.Ads.RewardedAd.IsAdReady)
        //{
        //    return true;
        //}

        //if (_fbAdsMgr != null && _fbAdsMgr.isRewardLoaded())
        //{
        //    return true;
        //}


        if (isAdmobRewardReady())
        {
            return true;
        }

        return false;
    }

    public void ShowMediationTestSuite()
    {
//#if !ADS_ON
//    return;
//#endif
//        //MediationTestSuite.Show();
    }

    const string INTER = "INTER";
    const string REWARD = "REWARD";
    private void DelayRequestAdmobInterstitial()
    {
        DelayCallFunc(INTER, 5f, RequestAdmobInter);
    }

    void DelayRequestLionInter()
    {
        DelayCallFunc(INTER, 5f, RequestLionInter);
    }


    //ShowAdRequest _showLionInterRequest;


    void RequestLionInter()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }

        if (AnalyzeMgr.current != null)
        {
            AnalyzeMgr.current.OnInterRequest(AdsFrom.Lion);
        }

        // Create callbacks for interstitial ads
        //_showLionInterRequest = new ShowAdRequest();
        //_showLionInterRequest.OnDisplayed += OnLionInterShow;// adUnitID => LionDebug.Log("Displayed Interstitial Ad :: Ad Unit ID = " + adUnitID);
        //_showLionInterRequest.OnClicked += OnLionInterClicked;// adUnitID => LionDebug.Log("Clicked Interstitial Ad :: Ad Unit ID = " + adUnitID);
        //_showLionInterRequest.OnHidden += OnLionInterClosed;////adUnitID => LionDebug.Log("Closed Interstitial Ad :: Ad Unit ID = " + adUnitID);
        //_showLionInterRequest.OnFailedToDisplay += OnLionInterFailed; //(adUnitID, error) => LionDebug.LogError("Failed To Display Interstitial Video :: Error = " + error + " :: Ad Unit ID = " + adUnitID);
    }

    private void OnLionInterFailed(string arg1, int arg2)
    {
        AnalyzeMgr.current.OnInterFailed(AdsFrom.Lion, arg1+ "___" + arg2.ToString());
        DelayRequestAdmobInterstitial();
    }

    private void OnLionInterClosed(string obj)
    {
        AnalyzeMgr.current.OnInterClosed(AdsFrom.Lion);
        RequestLionInter();
    }

    private void OnLionInterClicked(string obj)
    {
        AnalyzeMgr.current.OnInterClicked(AdsFrom.Lion);
    }

    private void OnLionInterShow(string obj)
    {
        AnalyzeMgr.current.OnInterShowed(AdsFrom.Lion);
    }

    void RequestAdmobInter()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }

        if (AnalyzeMgr.current != null)
        {
            AnalyzeMgr.current.OnInterRequest(AdsFrom.Admob);
        }

//#if UNITY_ANDROID
//        string adUnitId = ADMOB_AND_INTER_ID;
//#elif UNITY_IPHONE
//        string adUnitId = AND_INTER_ID;
//#else
//        string adUnitId = "unexpected_platform";
//#endif
        //if (ADMOBinterstitial != null)
        //{
        //    ADMOBinterstitial.Destroy();
        //}

        //this.ADMOBinterstitial = new GoogleMobileAds.Api.InterstitialAd(adUnitId);
        //// Called when an ad request has successfully loaded.
        //this.ADMOBinterstitial.OnAdLoaded += HandleOnAdLoaded;
        //// Called when an ad request failed to load.
        //this.ADMOBinterstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        //// Called when an ad is shown.
        //this.ADMOBinterstitial.OnAdOpening += HandleOnInterAdOpened;
        //// Called when the ad is closed.
        //this.ADMOBinterstitial.OnAdClosed += HandleOnAdClosed;
        //// Called when the ad click caused the user to leave the application.
        //this.ADMOBinterstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        //// Initialize an InterstitialAd.
        //AdRequest request = new AdRequest.Builder().Build();
        //// Load the interstitial with the request.
        //this.ADMOBinterstitial.LoadAd(request);


    }

    private void HandleOnAdLeavingApplication(object sender, EventArgs e)
    {
        AnalyzeMgr.current.OnInterClicked(AdsFrom.Admob);
    }

    private void HandleOnAdClosed(object sender, EventArgs e)
    {
        AnalyzeMgr.current.OnInterClosed(AdsFrom.Admob);
        RequestAdmobInter();

    }

    private void HandleOnInterAdOpened(object sender, EventArgs e)
    {
        AnalyzeMgr.current.OnInterShowed(AdsFrom.Admob);
    }

    //private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    //{
    //    AnalyzeMgr.current.OnInterFailed(AdsFrom.Admob, e.Message);
    //    DelayRequestAdmobInterstitial();
    //}

    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
    }

    public bool isFBRewardReady()
    {
        return false;
        //return _fbAdsMgr.isRewardLoaded();
    }

    public bool isFBInterReady()
    {
        return false;
        //return _fbAdsMgr.isInterLoaded;
    }

    public bool isAdmobInterReady()
    {
        //if (ADMOBinterstitial == null)
        //{
        //    return false;
        //}
        //return this.ADMOBinterstitial.IsLoaded();
        return false;

    }

    public bool isAdmobRewardReady()
    {
        //if (admobRewardedAd == null)
        //{
        //    return false;
        //}
        //return admobRewardedAd.IsLoaded();
        return false;
    }



    Action _onRewardFinished;
    internal bool ShowRewardAds(Action onRewardFinished)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        try
        {
            if(_maxAdsMgr != null && _maxAdsMgr.IsRewardReady())
            {
                _maxAdsMgr.ShowReward(onRewardFinished);
                lastShowInterTime = Time.time;
                return true;
            }
            //if (LionStudios.Ads.RewardedAd.IsAdReady)
            //{
            //    LionStudios.Ads.RewardedAd.Show(_lionShowAdRequest);
            //    return;
            //}
            //if (_fbAdsMgr != null && _fbAdsMgr.isRewardLoaded())
            //{
            //    _fbAdsMgr.ShowRewardedVideo(onRewardFinished);
            //    lastShowInterTime = Time.time;
            //    return;
            //}

            //if (_unityAdsMgr != null && _unityAdsMgr.isRewardedReady())
            //{
            //    _unityAdsMgr.ShowRewardedVideo(onRewardFinished);
            //    lastShowInterTime = Time.time;
            //    return true;
            //}

            _onRewardFinished = null;
            //if (admobRewardedAd.IsLoaded())
            //{
            //    _onRewardFinished = onRewardFinished;
            //    AnalyzeMgr.current.OnRewardBeforeShow(AdsFrom.Admob);
            //    admobRewardedAd.Show();
            //    lastShowInterTime = Time.time;
            //    return;
            //}
        }
        catch
        {
            AnalyzeMgr.current.OnRewardError();
        }
        finally
        {
        }

        return false;

    }
    int showInterTime = 1;
    public void ShowInterEvery3Times()
    {
        print("Show interr");
        if (showInterTime % 3 == 0)
        {
            ShowInter();
        }
        showInterTime++;
    }

    float lastShowInterTime = 0f;
    public void ShowInterEveryMin()
    {
        if (lastShowInterTime + 3f < Time.time)
        {
            lastShowInterTime = Time.time;
            ShowInter();
        }
    }

    float lastShowAdsTime = 0;
    const float ADS_INTERVAL = 30f;
    public bool IsInterReady()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false; 

        }
        if (_maxAdsMgr != null && _maxAdsMgr.IsInterReady())
        {
            return true;
        }
        return false;
    }

    public void ShowInter()
    {
        print("Show inter");
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;

        }
        if(_maxAdsMgr !=null && _maxAdsMgr.IsInterReady())
        {
            _maxAdsMgr.ShowInter();
            return;
        }
        //LionStudios.Ads.Interstitial.Show(_showLionInterRequest);
#if !ADS_ON
    return;
#endif
        //if (_fbAdsMgr != null && _fbAdsMgr.isInterLoaded)
        //{
        //    _fbAdsMgr.ShowInterstitial();
        //    return;
        //}

      
//#if !UNITY_EDITOR
//        if (this.ADMOBinterstitial.IsLoaded())
//        {
//            this.ADMOBinterstitial.Show();
//        }
//#endif
    }
    // Start is called before the first frame update

    //public void RequestAll()
    //{
    //    AnalyzeMgr.current.OnRequestAll();
    //    //print("request all");
    //    //if (_fbAdsMgr != null)
    //    //{
    //    //    _fbAdsMgr.RequestRewardedVideo();
    //    //}
    //    RequestAdmobReward();
    //    //RequestLionReward();
    //}

    public void RequestLionReward()
    {
        //_lionShowAdRequest = new ShowAdRequest();
        //_lionShowAdRequest.OnDisplayed += OnLionRewardDisplayed; //+= adUnitId => Debug.Log("Displayed Rewarded Ad :: Ad Unit ID = " + adUnitId);
        //_lionShowAdRequest.OnClicked += OnLionRewardClicked;//adUnitId => Debug.Log("Clicked Rewarded Ad :: Ad Unit ID = " + adUnitId);
        //_lionShowAdRequest.OnHidden += OnLionRewardHidden;//adUnitId => Debug.Log("Closed Rewarded Ad :: Ad Unit ID = " + adUnitId);
        //_lionShowAdRequest.OnFailedToDisplay += OnLionRewardLoadFaild; //(adUnitId, error) => Debug.LogError("Failed To Display Rewarded Ad ::Error = " + error + " :: Ad Unit ID = " + adUnitId);
        //_lionShowAdRequest.OnReceivedReward += OnLionReceivedReward; //(adUnitId, reward) => Debug.Log("Received Reward :: Reward = " + reward + " :: Ad Unit ID = " + adUnitId);
    }

    public void RequestAdmobReward()
    {
#if !ADS_ON
    return;
#endif
        
#if UNITY_ANDROID
        string adUnitId = ADMOB_AND_REWARD_ID;
#elif UNITY_IPHONE
            string adUnitId = IOS_REWARD_ID;
#else
            string adUnitId = "unexpected_platform";
#endif

        //this.admobRewardedAd = new GoogleMobileAds.Api.RewardedAd(adUnitId);

        //this.admobRewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        //this.admobRewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        //this.admobRewardedAd.OnAdClosed += HandleRewardedAdClosed;
        //this.admobRewardedAd.OnPaidEvent += OnPaidEvent;
        //admobRewardedAd.OnAdFailedToLoad += HandleAdFaildToLoad;

        //if (AnalyzeMgr.current != null)
        //{
        //    AnalyzeMgr.current.OnRewardRequest(AdsFrom.Admob);
        //}
        //// Create an empty ad request.
        //AdRequest request = new AdRequest.Builder().Build();
        //// Load the rewarded video ad with the request.
        //this.admobRewardedAd.LoadAd(request);
    }

    #region LionAds
    //private void OnLionReceivedReward(string arg1, MaxSdkBase.Reward arg2)
    //{

    //    if (_onRewardFinished != null)
    //    {
    //        _onRewardFinished();
    //    }
    //    else
    //    {
    //        InitMgr.current.BackToLastPoint();
    //    }
    //    AnalyzeMgr.current.OnRewardFinished(AdsFrom.Lion);
    //    RequestLionReward();
    //}

    //private void OnLionRewardLoadFaild(string arg1, int arg2)
    //{
    //    AnalyzeMgr.current.OnRewardFailedLoad(AdsFrom.Lion, arg1 + "___" + arg2.ToString());
    //    DelayCallFunc(REWARD, 5f, RequestLionReward);
    //    RewardFaildToLoad();
    //}

    //private void OnLionRewardHidden(string obj)
    //{
    //    //在用户点按“关闭”图标或使用“返回”按钮关闭激励广告时，系统会调用此方法。如果您的应用暂停了音频输出或游戏循环，则非常适合使用此方法恢复这些活动
    //    AnalyzeMgr.current.OnRewardSkiped(AdsFrom.Lion);
    //    UserSkipReward();
    //    RequestLionReward();
    //}

    //private void OnLionRewardClicked(string obj)
    //{
    //}

    //private void OnLionRewardDisplayed(string obj)
    //{
    //}

    #endregion

    //private void OnPaidEvent(object sender, AdValueEventArgs e)
    //{

    //}

    //private void HandleAdFaildToLoad(object sender, AdErrorEventArgs e)
    //{
    //    AnalyzeMgr.current.OnRewardFailedLoad(AdsFrom.Admob, e.Message);
    //    DelayCallFunc(REWARD, 5f, RequestAdmobReward);
    //    RewardFaildToLoad();
    //}

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        //在用户点按“关闭”图标或使用“返回”按钮关闭激励广告时，系统会调用此方法。如果您的应用暂停了音频输出或游戏循环，则非常适合使用此方法恢复这些活动
        AnalyzeMgr.current.OnRewardSkiped(AdsFrom.Admob);
        UserSkipReward();
        RequestAdmobReward();
    }

    //private void HandleUserEarnedReward(object sender, Reward e)
    //{
    //    if (_onRewardFinished != null)
    //    {
    //        _onRewardFinished();
    //    }
    //    else
    //    {
    //        InitMgr.current.BackToLastPoint();
    //    }
    //    AnalyzeMgr.current.OnRewardFinished(AdsFrom.Admob);
    //    RequestAdmobReward();
    //}

    public void RewardFaildToLoad()
    {
    }

    public void UserSkipReward()
    {
    }

    private void HandleRewardedAdLoaded(object sender, EventArgs e)
    {
        if (AnalyzeMgr.current != null)
        {
            AnalyzeMgr.current.OnRewardLoaded(AdsFrom.Admob);
        }
    }



}
//#endregion
