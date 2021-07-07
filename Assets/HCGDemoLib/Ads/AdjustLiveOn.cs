using com.adjust.sdk;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ThinkingAnalytics;
using UnityEngine;
using UnityEngine.Networking;

public class AdjustLiveOn : MonoBehaviour
{
    public static AdjustLiveOn current;
    private void Awake()
    {
        current = this;
    }
    const string ADJUST_TOKEN = "3ikf9zixfsqo";

    public void InitAdjust()
    {
        // import this package into the project:
        // https://github.com/adjust/unity_sdk/releases

#if UNITY_IOS
        /* Mandatory - set your iOS app token here */
        InitAdjust(token);
#elif UNITY_ANDROID
        /* Mandatory - set your Android app token here */
        InitAdjust(ADJUST_TOKEN);
#endif


    }


    private void InitAdjust(string adjustAppToken)
    {
        var adjustConfig = new AdjustConfig(
            adjustAppToken,
            AdjustEnvironment.Production, // AdjustEnvironment.Sandbox to test in dashboard
            true
        );
        adjustConfig.setLogLevel(AdjustLogLevel.Info); // AdjustLogLevel.Suppress to disable logs
        adjustConfig.setSendInBackground(true);
        new GameObject("Adjust").AddComponent<Adjust>(); // do not remove or rename

        Adjust.addSessionCallbackParameter("foo", "bar"); // if requested to set session-level parameters
        adjustConfig.setAttributionChangedDelegate(onAttributionChanged);

        //adjustConfig.setAttributionChangedDelegate((adjustAttribution) => {
        //  Debug.LogFormat("Adjust Attribution Callback: ", adjustAttribution.trackerName);
        //});

        Adjust.start(adjustConfig);

    }

    private void onAttributionChanged(AdjustAttribution obj)
    {
        string trackerToken = obj.trackerToken;
        string trackerName = obj.trackerName;
        string network = obj.network;
        string campaign = obj.campaign;
        string adgroup = obj.adgroup;
        string creative = obj.creative;
        string clickLabel = obj.clickLabel;
        string adid = obj.adid;
        string your_distinct_id = ThinkingAnalyticsAPI.GetDistinctId();
        var costType = obj.costType;
        var costAmount = obj.costAmount;
        var costCurrency = obj.costCurrency;
        //string thinkBaseUrl = "http://15.222.195.63/";
        //string thinkAppid = "a9c5337e3d264ecf98dc6b784e8a5869";
        string final = $"https://bi.rtxgames.fun/attribution/callback/adjust/a9c5337e3d264ecf98dc6b784e8a5869?ta_distinct_id={your_distinct_id}&" +
            $"network_name={network}&" +
            $"campaign_name={campaign}" +
            $"&adgroup_name={adgroup}" +
            $"&creative_name={creative}" +
            $"&trackerToken={trackerToken}" +
            $"&trackerName = {trackerName}" +
            $"&clickLabel = {clickLabel}" +
            $"&adid={adid}"
           ;
        //ThinkingAnalyticsAPI.UserSetOnce
        print("[AdJust ] + final " + final);
        var finalDict = new Dictionary<string, object>
        {
            { "adjust_distinct_id",your_distinct_id},
            { "adjust_network_name",network },
            { "adjust_compaign_name",campaign},
            { "adjust_adgroup_name",adgroup},
            { "adjust_creative_name",creative},
            { "adjust_clickLabel_name",clickLabel},
            { "adjust_adid_name",adid},
            { "adjust_trackerName",trackerName},
            { "adjust_trackerToken",trackerToken},
            { "adjust_costType",costType},
            { "adjust_costAmount",costAmount.ToString()},
            { "adjust_costCurrency",costCurrency}
            };

        ThinkingAnalyticsAPI.UserSet(finalDict);
        Send(final);
    }

    void TestUserSetOnce()
    {
        var ne = new Dictionary<string, object> { { "test_adjust", "adjustOnce" } };
        var distincId = ThinkingAnalyticsAPI.GetDistinctId();
        print("dist tinct id " + distincId);
        ThinkingAnalyticsAPI.UserSetOnce(ne);
    }
    
    public void TestAdjust()
    {
        string tt = "testJust";
        string test = $"https://bi.rtxgames.fun/attribution/callback/adjust/a9c5337e3d264ecf98dc6b784e8a5869?ta_distinct_id=3f97f863c215988b8fabbed7ee8db486___4___56&network_name=joenetwork3&campaign_name=joecampaign&adgroup_name=joeadgrouptest&creative_name=joecreative2&testAdjust={tt}&testFromhttps=httpsb";
        Send(test);
        test = $"http://15.222.195.63/attribution/callback/adjust/a9c5337e3d264ecf98dc6b784e8a5869?ta_distinct_id=3f97f863c215988b8fabbed7ee8db486___4___58&network_name=joenetwork4&campaign_name=joecampaign&adgroup_name=joeadgrouptest&creative_name=joecreative2&testAdjust={tt}&testFromCode=code";
        print("test " + test);
        Send(test);

    }

    const string APPID = "a9c5337e3d264ecf98dc6b784e8a5869";

    void Send(string URL)
    {
        StartCoroutine(GetRequest(URL));
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
        //request.Method = "POST";
        //request.ContentType = "text/plain";
        //request.ReadWriteTimeout = 30 * 1000;
        //request.Timeout = 30 * 1000;
        //Dictionary<string, object> param = new Dictionary<string, object>();
        //Stream requestStream = null;
        //HttpWebResponse response = null;
        //Stream responseStream = null;
        //try
        //{
        //    using (requestStream = request.GetRequestStream())
        //    {
        //        response = (HttpWebResponse)request.GetResponse();
        //        responseStream = response.GetResponseStream();
        //        var responseResult = new StreamReader(responseStream).ReadToEnd();
        //        if (responseResult != null)
        //        {
        //            print("adjust result " + responseResult);
        //        }

        //    }
        //}
        //catch (WebException ex)
        //{
        //}
        //finally
        //{
        //    if (requestStream != null)
        //    {
        //        requestStream.Close();
        //    }
        //    if (responseStream != null)
        //    {
        //        responseStream.Close();
        //    }
        //    if (response != null)
        //    {
        //        response.Close();
        //    }
        //    if (request != null)
        //    {
        //        request.Abort();
        //    }
        //}

    }

    IEnumerator GetRequest(string uri)
    {
        WWWForm form = new WWWForm();
        form.AddField("testFiled", "testData");

        using (UnityWebRequest www = UnityWebRequest.Get(uri))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}
