//using Facebook.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using ThinkingAnalytics;
//using Umeng;
using UnityEngine;


public enum AdsFrom
{
    Admob,
    Unity,
    Facebook,
    Lion,
    Max
}


public class AnalyzeMgr : MonoBehaviour
{
    public static AnalyzeMgr current;
    const string APP_KEY = "5f9fb55b1c520d30739e2399";

    const string RESTART_CLICKED = "restart_clicked";
    internal void OnRestartClicked()
    {
        var dict = GetAutoAddKey(RESTART_CLICKED);
        ThinkingAnalyticsAPI.Track(RESTART_CLICKED, dict);
        SendEvent(RESTART_CLICKED);
    }

#if GOOGLE_PLAY
    const string CHANNEL_ID = "googleplay";
#else
    const string CHANNEL_ID = "china_test";
#endif

    static string groupId = "";
    const string GROUP_KEY = "group_key";
    const string GROUP_A = "group_a";
    const string GROUP_B = "group_b";
    
    void Awake()
    {
        groupId = PlayerPrefs.GetString(GROUP_KEY, "");
        if(groupId.Equals(""))
        {
            int r = UnityEngine.Random.Range(0, 1000);
            if (r % 2 == 0)
            {
                groupId = GROUP_A;
            }
            else
            {
                groupId = GROUP_B;
            }
            PlayerPrefs.SetString(GROUP_KEY, groupId);
        }
        _IPAddress = GetLocalIPAddress();
        _deviceUid = SystemInfo.deviceUniqueIdentifier;
        _gameVersion = Application.version;
        _deviceType = SystemInfo.deviceModel;
        string lastTimeStr = PlayerPrefs.GetString(LAST_LOGOUT_TIME_KEY, "");

        if (lastTimeStr.Equals(""))
        {
            _lastLogoutTime = DateTime.UtcNow;
        }
        else
        {
            _lastLogoutTime = DateTime.Parse(lastTimeStr);
        }

#if UNITY_IOS
        ios = 1;
#endif
        current = this;
    }


    public void OnGetExtraCoinTaped()
    {
        var dict = GetAutoAddKey(TAP_GET_EXTRA_BTN);
        ThinkingAnalyticsAPI.Track(TAP_GET_EXTRA_BTN, dict);
        SendEvent(TAP_GET_EXTRA_BTN);
    }

    static string _IPAddress = "";
    static string _deviceUid = "";
    static string _gameVersion = "";
    static string _deviceType = "";
    static int ios = 0;
    void Start()
    {
#if UNITY_EDITOR
        //return;
#endif
        //Umeng.GA.StartWithAppKeyAndChannelId(APP_KEY, CHANNEL_ID);
        //GA.ProfileSignIn(_deviceUid);
        //ThinkingAnalyticsAPI.EnableTracking(false);
        AdjustLiveOn.current.InitAdjust();
        OnInstall();
        OnGameStart();
    }

    const string WHICH_LEVEL_LOSE = "which_level_lose";


    const string ON_AVARTAT_PUCHASED = "on_avartar_purchased";
    internal void OnAvartarPurchased(int rowNumber)
    {
        var dict = GetAutoAddKey(ON_AVARTAT_PUCHASED);
        dict.Add("avatarID", rowNumber);
        ThinkingAnalyticsAPI.Track(ON_AVARTAT_PUCHASED, dict);
        SendEvent(ON_AVARTAT_PUCHASED);
    }

    const string LEVEL_LOSE = "level_lose";
    const string LEVEL_WIN = "level_win";
    const string LEVEL_START = "level_start";
    const string GAME_START = "game_start";


    const string INTER_REQUEST = "inter_request";
    const string INTER_SHOWED = "inter_showed";
    const string INTER_LOADED = "inter_loaded";
    const string INTER_FAILDED = "inter_failed";
    const string INTER_FAILDED_SHOW = "inter_failed_show";
    const string INTER_WHEEL = "inter_wheel";

    const string INTER_CLICKED = "inter_clicked";

    const string REWARD_REQUEST = "reward_request";
    const string REWARD_LOADED = "reward_loaded";
    const string REWARD_SHOWED = "reward_showed";
    const string REWARD_FAILED_LOAD = "reward_failed";
    const string REWARD_FAILED_SHOW = "reward_failed_show";
    const string REWARD_SKIPED = "reward_skiped";
    const string REWARD_CLICKED = "reward_clicked";
    const string REWARD_CLOSED = "reward_closed";
    const string REWARD_FINISHED = "reward_finished";


    #region keys
    const string DISTINCT_ID_KEY = "#distinct_id";
    const string TIME_KEY = "#time";
    const string IP_ADDRESS_KEY = "ip_address";
    const string DEVICE_TYPE_KEY = "device";
    const string PLAYER_NAME_KEY = "playername";
    const string LAST_TIME_LOGIN_KEY = "last_login_time";
    const string LAST_LOGOUT_TIME_KEY = "last_logout_time";
    const string REG_TIME_KEY = "reg_time";
    const string LEVEL_KEY = "level";
    const string ONLINE_TIME_KEY = "onlinetime";
    const string IOS_KEY = "ios";
    #endregion

    const string TAP_GET_EXTRA_BTN = "get_extra_btn_taped";

    const string INSTALL_KEY = "install";
    public  static void OnInstall()
    {
#if UNITY_EDITOR
        return;
#endif
        bool firstInstall = PlayerPrefs.GetInt(INSTALL_KEY, 0) == 0;
        if (firstInstall)
        {
            PlayerPrefs.SetInt(INSTALL_KEY, 1);
            var dict = GetBaseDict();
            dict.Add("reg_time", DateTime.UtcNow);
            ThinkingAnalyticsAPI.Track(INSTALL_KEY, dict);
            ThinkingAnalyticsAPI.UserSetOnce(dict);
            SendEvent(INSTALL_KEY);
        }
    }



    const string level_Box_opened = "Level_box_open";
    internal void OnLevelBoxOpened(string itemId, int itemValue,int count)
    {
        var dict = GetAutoAddKey(level_Box_opened);
        dict.Add("itemID", itemId);
        dict.Add("itemValue", itemValue);
        dict.Add("boxCount", count);
        ThinkingAnalyticsAPI.Track(level_Box_opened, dict);
    }


    const string SPIN_CLICK = "Spin_click";
    public void SpinClick()
    {
        var dict = GetAutoAddKey(SPIN_CLICK);
       
        ThinkingAnalyticsAPI.Track(SPIN_CLICK, dict);
    }

    const string SPIN5_CLICK = "Spin5_click";
    public void Spin5Click()
    {
        var dict = GetAutoAddKey(SPIN5_CLICK);
        ThinkingAnalyticsAPI.Track(SPIN5_CLICK, dict);
    }

    const string SPIN5_FAILED = "Spin5_click_failed";

    public void Spin5ClickFailed()
    {
        var dict = GetAutoAddKey(SPIN5_FAILED);
        ThinkingAnalyticsAPI.Track(SPIN5_FAILED, dict);
    }

    const string GET_ONE_REFILL = "Get_one_refill";
    public void OnGetOneRefill()
    {
        JustCount(GET_ONE_REFILL);
    }
    const string GET_ONE_REFILL_REWARD = "Get_one_refill_reward";
    public void OnGetOneRefillReward()
    {
        JustCount(GET_ONE_REFILL_REWARD);
    }

    const string TRY_ITEM_VIA_REWARD = "try_get_item_via_reward";
    public void OnTryItemViaReward(string itemID, string itemValue,string seqTime)
    {
        var dict = GetAutoAddKey(TRY_ITEM_VIA_REWARD);
        dict.Add("itemID", itemID);
        dict.Add("itemValue",itemValue);
        dict.Add("seqTime",seqTime);
        ThinkingAnalyticsAPI.Track(TRY_ITEM_VIA_REWARD, dict);
    }

    const string GOT_ITEM_VIA_REWARD = "got_item_via_reward";
    public void OnGotItemViaReward(string itemID,string itemValue,string seqTime)
    {
        var key = GOT_ITEM_VIA_REWARD;
        var dict = GetAutoAddKey(key);
        dict.Add("itemID", itemID);
        dict.Add("itemValue", itemValue);
        dict.Add("seqTime", seqTime);
        ThinkingAnalyticsAPI.Track(key, dict);

    }

    const string FAILED_GET_ITEM_VIA_REWARD = "failed_get_item_via_reward";
    public void OnFailedGetItemViaReward(string itemID, string itemValue,string seqTime)
    {
        var key = FAILED_GET_ITEM_VIA_REWARD;
        var dict = GetAutoAddKey(key);
        dict.Add("itemID", itemID);
        dict.Add("itemValue", itemValue);
        dict.Add("seqTime", seqTime);
        ThinkingAnalyticsAPI.Track(key, dict);
    }

    public void JustCount(string str)
    {
        var dict = GetAutoAddKey(str);
        ThinkingAnalyticsAPI.Track(str, dict);
    }

    const string GET_3_REFILL = "Get_3_refill_first";
    public void OnGet3Refill()
    {
        JustCount(GET_3_REFILL);
    }
    const string GET_3_REFILL_REWARD = "Get_3_refill_reward_second";
    public void OnGet3RefillReward()
    {
        JustCount(GET_3_REFILL_REWARD);
    }

    const string GET_SPIN_BY_TIME = "get_spin_by_time";
    public void OnGetSpinByTime()
    {
        JustCount(GET_SPIN_BY_TIME);
    }
    const string WHEN_SPIN_0 = "WHEN_SPIN_0";
    public void WhenSpin0()
    {
        JustCount(WHEN_SPIN_0);
    }
    const string WHEN_REFILL_0 = "WHEN_REFILL_TOO_LOW";
    public void WhenRefiil0()
    {
        JustCount(WHEN_REFILL_0);
    }


    int current_level = -1;
    public static void OnGameStart()
    {
#if UNITY_EDITOR
        return;
#endif
        var dict = GetAutoAddKey(GAME_START);
        dict.Add("game_start_time", DateTime.UtcNow);
        ThinkingAnalyticsAPI.Track(GAME_START, dict);
        ThinkingAnalyticsAPI.UserSet(dict);
        SendEvent(GAME_START);
    }

    const string LOGIN_KEY = "login";
    public void OnLogin()
    {
#if UNITY_EDITOR
        return;
#endif
        string lastlogin = PlayerPrefs.GetString(LAST_TIME_LOGIN_KEY, "");
        if (lastlogin.Equals(""))
        {
            loginDateTime = DateTime.Parse(lastlogin);
        }
        else
        {
            loginDateTime = DateTime.UtcNow;
        }
        var curlevel = InitMgr.current.GetCurrentLevelIndex();
        var dict = GetAutoAddKey(LOGIN_KEY);
        dict.Add("level", curlevel);
        dict.Add(LAST_TIME_LOGIN_KEY, loginDateTime);
        PlayerPrefs.SetString(LAST_TIME_LOGIN_KEY, DateTime.UtcNow.ToString());
        ThinkingAnalyticsAPI.Track(LOGIN_KEY, dict);
        ThinkingAnalyticsAPI.UserSet(dict);
    }
    DateTime loginDateTime;

    DateTime _lastLogoutTime;
    const string LOGOUT_KEY = "logout";
    public void OnLogout()
    {
        var dict = GetAutoAddKey(LOGOUT_KEY);
        dict.Add(LAST_LOGOUT_TIME_KEY, _lastLogoutTime);
        dict.Add(ONLINE_TIME_KEY, _onlineTime);
        ThinkingAnalyticsAPI.Track(LOGOUT_KEY, dict);
        ThinkingAnalyticsAPI.UserAdd(LOGOUT_KEY, dict);
        ThinkingAnalyticsAPI.UserSet(dict);
        SendEvent(LOGOUT_KEY);
    }

    public void OnInterRequest(AdsFrom f)
    {
        var dict = GetAutoAddKey(INTER_REQUEST);
        var from = f.ToString();
        dict.Add("from", from);
        ThinkingAnalyticsAPI.Track(INTER_REQUEST, dict);
        SendEvent(INTER_REQUEST);
    }

    internal void OnInterLoaded(AdsFrom f)
    {
        var dict = GetAutoAddKey(INTER_LOADED);
        var from = f.ToString();
        dict.Add("from", from);
        ThinkingAnalyticsAPI.Track(INTER_LOADED, dict);
        SendEvent(INTER_LOADED);
    }


    public void OnInterShowed(AdsFrom f)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(INTER_SHOWED);
        dict.Add("from", from);
        dict.Add("level", InitMgr.current.GetCurrentLevelIndex());
        ThinkingAnalyticsAPI.Track(INTER_SHOWED, dict);
        SendEvent(INTER_SHOWED, from);
    }

    public void OnInterFailed(AdsFrom f, string error)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(INTER_FAILDED);
        dict.Add("from", from);
        dict.Add("adsError", error);
        dict.Add("level", InitMgr.current.GetCurrentLevelIndex());
        ThinkingAnalyticsAPI.Track(INTER_FAILDED, dict);
        SendEvent(INTER_FAILDED, error);
    }

    public void OnInterFailedShow(AdsFrom f, string error)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(INTER_FAILDED_SHOW);
        dict.Add("from", from);
        dict.Add("adsError", error);
        dict.Add("level", InitMgr.current.GetCurrentLevelIndex());
        ThinkingAnalyticsAPI.Track(INTER_FAILDED_SHOW, dict);
        SendEvent(INTER_FAILDED_SHOW, error);
    }

    const string INTER_CLOSED = "inter_closed";
    public void OnInterClosed(AdsFrom f)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(INTER_CLOSED);
        dict.Add("from", from);
        ThinkingAnalyticsAPI.Track(INTER_CLOSED, dict);
        SendEvent(INTER_CLOSED);

    }

    public void OnInterClicked(AdsFrom f)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(INTER_CLICKED);
        dict.Add("from", from);
        ThinkingAnalyticsAPI.Track(INTER_CLICKED, dict);
        SendEvent(INTER_CLICKED);

    }

    //const string REQUEST_ALL = "Request_all";
    //internal void OnRequestAll()
    //{
    //    var dict = GetAutoAddKey(REQUEST_ALL);
    //    ThinkingAnalyticsAPI.Track(REQUEST_ALL, dict);
    //    SendEvent(REQUEST_ALL);
    //}

    public void OnRewardRequest(AdsFrom f)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(REWARD_REQUEST);
        dict.Add("level", InitMgr.current.GetCurrentLevelIndex());
        ThinkingAnalyticsAPI.Track(REWARD_REQUEST, dict);
        SendEvent(REWARD_REQUEST);
    }

    internal void ShowInterWheel()
    {
        var dict = GetAutoAddKey(INTER_WHEEL);
        dict.Add("level", InitMgr.current.GetCurrentLevelIndex());
        ThinkingAnalyticsAPI.Track(INTER_WHEEL, dict);
        SendEvent(INTER_WHEEL);
    }

    const string REWARD_ERROR = "reward_error"; //when 
    public void OnRewardError()
    {
        var dict = GetAutoAddKey(REWARD_ERROR);
        dict.Add("level", InitMgr.current.GetCurrentLevelIndex());
        ThinkingAnalyticsAPI.Track(REWARD_ERROR, dict);
        SendEvent(REWARD_ERROR);

    }

    public void OnBeforeLoadReward(AdsFrom f)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(REWARD_LOADED);
        dict.Add("level", InitMgr.current.GetCurrentLevelIndex());
        dict.Add("from", from);
        ThinkingAnalyticsAPI.Track(REWARD_LOADED, dict);
        SendEvent(REWARD_LOADED);
    }

    public void OnRewardLoaded(AdsFrom f)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(REWARD_LOADED);
        dict.Add("level", InitMgr.current.GetCurrentLevelIndex());
        dict.Add("from", from);
        ThinkingAnalyticsAPI.Track(REWARD_LOADED, dict);
        SendEvent(REWARD_LOADED);
    }

    const string BEFORE_SHOW_REWARD = "BEFORE_SHOW_REWARD";
    public void OnRewardBeforeShow(AdsFrom f)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(BEFORE_SHOW_REWARD);
        dict.Add("from", from);
        ThinkingAnalyticsAPI.Track(BEFORE_SHOW_REWARD, dict);
        SendEvent(BEFORE_SHOW_REWARD);
    }

    public void OnRewardShowed(AdsFrom f)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(REWARD_SHOWED);
        dict.Add("from", from);
        ThinkingAnalyticsAPI.Track(REWARD_SHOWED, dict);
        SendEvent(REWARD_SHOWED);
    }

    public void OnRewardFinished(AdsFrom f)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(REWARD_FINISHED);
        dict.Add("from", from);
        ThinkingAnalyticsAPI.Track(REWARD_FINISHED, dict);
        SendEvent(REWARD_FINISHED);

    }

    public void OnRewardSkiped(AdsFrom f)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(REWARD_SKIPED);
        dict.Add("from", from);
        ThinkingAnalyticsAPI.Track(REWARD_SKIPED, dict);
        SendEvent(REWARD_SKIPED);
    }
    public void OnRewardFailedLoad(AdsFrom f, string error)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(REWARD_FAILED_LOAD);
        dict.Add("from", from);
        dict.Add("error", error);
        ThinkingAnalyticsAPI.Track(REWARD_FAILED_LOAD, dict);
        SendEvent(REWARD_FAILED_LOAD, error);
    }

    public void OnRewardFailedShow(AdsFrom f, string error)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(REWARD_FAILED_SHOW);
        dict.Add("from", from);
        dict.Add("error", error);
        ThinkingAnalyticsAPI.Track(REWARD_FAILED_SHOW, dict);
        SendEvent(REWARD_FAILED_SHOW, error);
    }


    public void OnRewardClicked(AdsFrom f)
    {
        var from = f.ToString();
        var dict = GetAutoAddKey(REWARD_CLICKED);
        dict.Add("from", from);
        ThinkingAnalyticsAPI.Track(REWARD_CLICKED, dict);
        SendEvent(REWARD_CLICKED);
    }

    public void OnLevelLose(int level, string btnName)
    { 
        print("lose " + level);
        var dict = GetAutoAddKey(LEVEL_LOSE);
        dict.Add("level", level);
        dict.Add("btnName", btnName);
        //FB.LogAppEvent(LEVEL_LOSE, null,dict);
        ThinkingAnalyticsAPI.Track(LEVEL_LOSE, dict);
    }

    public void OnLevelWon(int level, int gameTime)
    {
        print("won" + level);
        SendEvent(LEVEL_WIN, _deviceUid + "___" + level.ToString() + "___" + gameTime.ToString());
        var thinkDict = GetAutoAddKey(LEVEL_WIN);
        thinkDict.Add("level", level);
        ThinkingAnalyticsAPI.Track(LEVEL_WIN, thinkDict);
        ThinkingAnalyticsAPI.UserSet(thinkDict);
    }

    public void OnLevelStart(int level)
    {
        print("start " + level);
        current_level = level;
        SendEvent(LEVEL_START, SystemInfo.deviceUniqueIdentifier + "___" + level.ToString());
        var levelDict = GetAutoAddKey(LEVEL_START);
        levelDict.Add("level", level);
        ThinkingAnalyticsAPI.Track(LEVEL_START, levelDict);
        ThinkingAnalyticsAPI.UserSet(levelDict);
    }


    const string FIRST_NEXT_LEVEL = "firstNextLevel";
    public void OnFirstLevelTapped(int curLevelIndex)
    {
        var dict = GetAutoAddKey(FIRST_NEXT_LEVEL);
        dict.Add(FIRST_NEXT_LEVEL, curLevelIndex);
        ThinkingAnalyticsAPI.Track(FIRST_NEXT_LEVEL, dict);
        AnalyzeMgr.SendEvent(FIRST_NEXT_LEVEL, SystemInfo.deviceUniqueIdentifier + "___" + (curLevelIndex - 1).ToString());
    }

    const string FIRST_PLAY_LEVEL = "firstPlayLevel";
    public void OnFirstPlayNextLevel(int curLevelIndex)
    {
        var dict = GetAutoAddKey(FIRST_PLAY_LEVEL);
        dict.Add(FIRST_PLAY_LEVEL, curLevelIndex);
        ThinkingAnalyticsAPI.Track(FIRST_PLAY_LEVEL, dict);
        SendEvent(FIRST_PLAY_LEVEL, SystemInfo.deviceUniqueIdentifier + "___" + (curLevelIndex).ToString());
    }



    const string OPEN_SHOP = "open_shop";
    public void OnShopBtnTapped()
    {
        var shopDict = GetAutoAddKey(OPEN_SHOP);
        ThinkingAnalyticsAPI.Track(OPEN_SHOP, shopDict);
        SendEvent(OPEN_SHOP, SystemInfo.deviceUniqueIdentifier);
    }



    static void SendEvent(string eventId)
    {
        //GA.Event(eventId);
        //FB.LogAppEvent(eventId);
    }


    static void SendEvent(string eventId, string label)
    {
        //#if !UNITY_EDITOR
        //        GA.Event(eventId, label);
        //        var eventDir = new Dictionary<string, object>();
        //        eventDir["label"] = label;
        //        FB.LogAppEvent(eventId,null,eventDir);
        //#endif
    }

    const string GET_3_KEYS_FROM_REWARD = "get_3_keys_from_reward";
    internal void OnGetBoxKeysFromReward()
    {
        var shopDict = GetAutoAddKey(GET_3_KEYS_FROM_REWARD);
        ThinkingAnalyticsAPI.Track(GET_3_KEYS_FROM_REWARD, shopDict);
    }

    public static void SendEventWithThink(string eventId, string label)
    {
        SendEvent(eventId, label);
    }


    /// <summary>
    /// <summary>
    /// 自定义事件 — 计算事件数
    /// </summary>
    static void SendEvent(string eventId, Dictionary<string, string> attributes, int value)
    {
        //GA.Event(eventId, attributes, value);
        var eventDir = new Dictionary<string, object>();
        foreach (var p in attributes)
        {
            eventDir.Add(p.Key, p.Value);
        }
        //FB.LogAppEvent(eventId, value, eventDir);
    }


    public static Dictionary<string, object> GetBaseDict()
    {
        int curLv = -1;
        int playerHasRefill = -1;
        int playerHasSpin = -1;
        if (InitMgr.current != null)
        {
            curLv = InitMgr.current.GetCurrentLevelIndex();
        }
        return new Dictionary<string, object>() {
            { "account_id",_deviceUid},
            { "distinct_id",_deviceUid},
            { "event_time",DateTime.UtcNow},
            {"deviceid",_deviceUid },
            {"ip_address",_IPAddress},
            {"device",_deviceType},
            {"ios",ios},
            {"version",_gameVersion},
            {"curLevelBase", curLv},
            {
                "bundleid",Application.identifier
            },
            {"playerSpinBase",playerHasSpin },
            {"playerRefillBase",playerHasRefill },
            {GROUP_KEY,groupId}

        };
    }

    static Dictionary<string, object> GetAutoAddKey(string trackKey)
    {
        int openAppTimes = PlayerPrefs.GetInt(trackKey, 0);
        openAppTimes++;
        PlayerPrefs.SetInt(trackKey, openAppTimes);
        var dict = GetBaseDict();
        dict.Add(trackKey + "Count", openAppTimes);
        return dict;
    }

    const string CURRENT_ONLINE_TIME = "onlinetime";
    int _onlineTime = 0;
    void OnApplicationFocus(bool pauseStatus)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            return;
        }
        if (!pauseStatus)
        {
            PlayerPrefs.SetString(LAST_LOGOUT_TIME_KEY, DateTime.UtcNow.ToString());
            _lastLogoutTime = DateTime.UtcNow;
            _onlineTime = (_lastLogoutTime - loginDateTime).Seconds;
            PlayerPrefs.SetInt(CURRENT_ONLINE_TIME, _onlineTime);
            OnLogout();
        }
    }


    public static string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }

        return "";
    }

    const string REFILL_CLICKED = "refill_clicked";
    internal void OnRefillBtnClicked()
    {
        var shopDict = GetAutoAddKey(REFILL_CLICKED);
        ThinkingAnalyticsAPI.Track(REFILL_CLICKED, shopDict);
        SendEvent(REFILL_CLICKED, SystemInfo.deviceUniqueIdentifier);
    }

    const string GET_ITEM_BY_COLLECTION = "GET_ITEM_BY_COLLECTION";
    public void OnGetItemByCollection(string itemId, string itemValue)
    {
        var shopDict = GetAutoAddKey(GET_ITEM_BY_COLLECTION);
        ThinkingAnalyticsAPI.Track(GET_ITEM_BY_COLLECTION, shopDict);
        shopDict.Add("itemId", itemId);
        shopDict.Add("itemValue", itemValue);
        SendEvent(GET_ITEM_BY_COLLECTION, SystemInfo.deviceUniqueIdentifier);
    }

    const string  GET_REFILL_BTN = "GET_REFILL_BTN";
    internal void OnGetRefillBtnTapped()
    {
        var shopDict = GetAutoAddKey(GET_REFILL_BTN);
        ThinkingAnalyticsAPI.Track(GET_REFILL_BTN, shopDict);
        SendEvent(GET_REFILL_BTN, SystemInfo.deviceUniqueIdentifier);
    }


    const string ON_ADD_REFILL_FIINISHED = "ON_ADD_REFILL_FIINISHED";
    internal void OnAddRefilFinished(int v)
    {
        var shopDict = GetAutoAddKey(ON_ADD_REFILL_FIINISHED);
        shopDict.Add("addCount", v);
        ThinkingAnalyticsAPI.Track(ON_ADD_REFILL_FIINISHED, shopDict);
        SendEvent(ON_ADD_REFILL_FIINISHED, SystemInfo.deviceUniqueIdentifier);
    }

    const string ON_COIN_REWARD_FINISHED = "ON_COIN_REWARD_FINISHED";
    internal void onAddCoinRewardFinshied()
    {
        var shopDict = GetAutoAddKey(ON_COIN_REWARD_FINISHED);
        ThinkingAnalyticsAPI.Track(ON_COIN_REWARD_FINISHED, shopDict);
        SendEvent(ON_COIN_REWARD_FINISHED, SystemInfo.deviceUniqueIdentifier);
    }

    const string NOT_ENOUGH_REFILL_TAP_REWARD = "NOT_ENOUGH_REFILL_TAP_REWARD";
    internal void OnTapRefillButNotEnough()
    {
        var shopDict = GetAutoAddKey(NOT_ENOUGH_REFILL_TAP_REWARD);
        ThinkingAnalyticsAPI.Track(NOT_ENOUGH_REFILL_TAP_REWARD, shopDict);
        SendEvent(NOT_ENOUGH_REFILL_TAP_REWARD, SystemInfo.deviceUniqueIdentifier);
    }

    const string FREE_SPIN = "free_spin";

    public string GOLD_SPIN = "gold_spin";

    public string REWARD_SPIN = "reward_spin";

    public static bool IsGroupA()
    {
        return groupId.Equals(GROUP_A);
    }

}
