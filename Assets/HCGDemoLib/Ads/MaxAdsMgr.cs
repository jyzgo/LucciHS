using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxAdsMgr :MonoBehaviour
{
    string interAdsID = "8bc504b5516e85cf";
    string rewardAdsID = "5293ea71600b9fd4";
    int interRetray;
    private void Awake()
    {
        
    }

    public void InitializeInterstitialAds()
    {
        // Attach callback
        //MaxSdkCallbacks.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
        //MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
        //MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
        //MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;

        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent+= OnInterstitialFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent+= InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent+= OnInterstitialDismissedEvent;

        // Load the first interstitial
        LoadInterstitial();
    }


    private void LoadInterstitial()
    {
        AnalyzeMgr.current.OnInterRequest(AdsFrom.Max);
        MaxSdk.LoadInterstitial(interAdsID);
    }

    private void OnInterstitialLoadedEvent(string adUnitId ,MaxSdkBase.AdInfo arg2)
    {
        // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(adUnitId) will now return 'true'
        // Reset retry attempt
        AnalyzeMgr.current.OnInterLoaded(AdsFrom.Max);
        interRetray = 0;
    }

    private void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo err)
    {
        var errorCode = err.Message;
        print("[AppLovin Max] OnInterAdFailed errorCode  " + errorCode);
        // Interstitial ad failed to load 
        // We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds)

        interRetray++;
        double retryDelay = Math.Pow(2, Math.Min(6, interRetray));

        AnalyzeMgr.current.OnInterFailed(AdsFrom.Max,errorCode.ToString());
        Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void InterstitialFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo err, MaxSdkBase.AdInfo adInfo)
    {
        var errorCode = err.Message;
        // Interstitial ad failed to display. We recommend loading the next ad
        AnalyzeMgr.current.OnInterFailedShow(AdsFrom.Max, errorCode.ToString());
        LoadInterstitial();
    }

    private void OnInterstitialDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad
        AnalyzeMgr.current.OnInterClosed(AdsFrom.Max);
        LoadInterstitial();
    }
    public void ShowInter()
    {
        print("ShowInter");
        if (MaxSdk.IsInterstitialReady(interAdsID))
        {
            AnalyzeMgr.current.OnInterShowed(AdsFrom.Max);
            MaxSdk.ShowInterstitial(interAdsID);
        }
    }

    public bool IsInterReady()
    {
        return MaxSdk.IsInterstitialReady(interAdsID);
    }

    public bool IsRewardReady()
    {
        return MaxSdk.IsRewardedAdReady(rewardAdsID);
    }



    int rewardAttemp;

    public void InitializeRewardedAds()
    {
        // Attach callback
        //MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
        //MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
        //MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
        //MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        //MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClickedEvent;
        //MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
        //MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        // Load the first rewarded ad
        LoadRewardedAd();
    }

    private void OnRewardedAdRevenuePaidEvent(string arg1, MaxSdkBase.AdInfo arg2)
    {
    }

    private void OnRewardedAdLoadFailedEvent(string arg1, MaxSdkBase.ErrorInfo arg2)
    {
        rewardAttemp++;
        double retryDelay = Math.Pow(2, Math.Min(6, rewardAttemp));
        AnalyzeMgr.current.OnRewardFailedLoad(AdsFrom.Max,arg2.ToString());
        Invoke("LoadRewardedAd", (float)retryDelay);
    }

    private void LoadRewardedAd()
    {
        AnalyzeMgr.current.OnRewardRequest(AdsFrom.Max);
        MaxSdk.LoadRewardedAd(rewardAdsID);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId,MaxSdkBase.AdInfo adInfo)
    {
        AnalyzeMgr.current.OnRewardLoaded(AdsFrom.Max);
        // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(adUnitId) will now return 'true'

        // Reset retry attempt
        rewardAttemp = 0;
    }

    private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to load 
        // We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds)

        print("[Applovin Max] OnRewardAdFailed " + errorCode);
        rewardAttemp++;
        double retryDelay = Math.Pow(2, Math.Min(6, rewardAttemp));

        AnalyzeMgr.current.OnRewardFailedLoad(AdsFrom.Max,errorCode.ToString());
        Invoke("LoadRewardedAd", (float)retryDelay);
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorCode, MaxSdkBase.AdInfo adInfo)
    {
        AnalyzeMgr.current.OnRewardFailedShow(AdsFrom.Max, errorCode.ToString());
        // Rewarded ad failed to display. We recommend loading the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId,MaxSdkBase.AdInfo adInfo) {
        AnalyzeMgr.current.OnRewardShowed(AdsFrom.Max);
    }

    private void OnRewardedAdClickedEvent(string adUnitId,MaxSdkBase.AdInfo adInfo) { 
    
        AnalyzeMgr.current.OnRewardClicked(AdsFrom.Max);
    }

    private void OnRewardedAdHiddenEvent(string adUnitId,MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        AnalyzeMgr.current.OnRewardSkiped(AdsFrom.Max);
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward,MaxSdkBase.AdInfo adInfo)
    {
        AnalyzeMgr.current.OnRewardFinished(AdsFrom.Max);
        // Rewarded ad was displayed and user should receive the reward
        if(_onRewardFinished != null)
        {
            _onRewardFinished();
        }
    }

    Action _onRewardFinished;

    public void ShowReward(Action onRewardFinished)
    {
        AnalyzeMgr.current.OnRewardBeforeShow(AdsFrom.Max);
        _onRewardFinished = onRewardFinished;
        if (MaxSdk.IsRewardedAdReady(rewardAdsID))
        {
            MaxSdk.ShowRewardedAd(rewardAdsID);
        }
    }
}
