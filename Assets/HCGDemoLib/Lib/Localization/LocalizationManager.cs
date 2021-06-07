using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTUnity;
using UnityEngine.U2D;
using MTUnity.Utils;
using UnityEditor;
using UnityEngine.UI;
using System;

public class LocalizationManager: Singleton<LocalizationManager>
{

    const string STR_LOCALIZATION_PREFIX = "localization/UI/";
    const string STR_LOCALIZATION_TEXT =   "localization/commontext/";
    const string STR_TELLOUT_TEXT = "localization/solitaireTellout/";
    Dictionary<string, MTJSONObject> currentTextsDict;
    private SpriteAtlas _spriteAtlas;
    public HashSet<LocalBase> baseSet = new HashSet<LocalBase>();
    public SpriteAtlas GetAtlas { 
            get{
                return _spriteAtlas;
            }
         }

    private SystemLanguage _currentLang;
    public SystemLanguage CurrentLang
    {
        get { return _currentLang; }
        set {
            UpdateLang(value);
        }
    }

    public void AddListener(LocalBase local)
    {
        baseSet.Add(local);
    }

    public void RemoveListener(LocalBase local)
    {
        baseSet.Remove(local);
    }

    public void ChangeLang(SystemLanguage lang)
    {
        UpdateLang(lang);
        foreach (var localBase in baseSet)
        {
            localBase.UpdateLocale();
        }
        
    }

    public Sprite GetSprite(string fileName)
    {
        //Debug.Log("name" + fileName);
        return _spriteAtlas.GetSprite(fileName);
    }
    
    private void Awake()
    {

        UpdateLang(Application.systemLanguage);
    }


    readonly string[] MONTH_KEYS = new string[] {
    "January_",
    "February_",
    "March_",
    "April_",
    "May_",
    "June_",
    "July_",
    "August_",
    "September_",
    "October_",
    "November_",
    "December_"
    };

    public string[] WEEK_KEYS = new string[]
    {
    "Monday_",
    "Tuesday_",
    "Wednesday_",
    "Thursday_",
    "Friday_",
    "Saturday_",
    "Sunday_"
    };
    public string GetMonthDayYear(int dayStamp)
    {
        DateTime curDateTime = new DateTime(1970, 1, 1) + new TimeSpan(dayStamp, 0, 0, 0);
        return GetMonthDayYear(curDateTime);
    }

    public string GetMonthDayYear(DateTime curDateTime)
    {
        int month = curDateTime.Month;
        int day = curDateTime.Day;
        int year = curDateTime.Year;
        string monthLocal = GetLocalStringByKey(MONTH_KEYS[month - 1]);
        string final = monthLocal + " " + day.ToString() + "," + year.ToString();
        return final;
    }
    public string GetShortMonthDayYear(int dayStamp)
    {
        DateTime curDateTime = new DateTime(1970, 1, 1) + new TimeSpan(dayStamp, 0, 0, 0);
        return GetShortMonthDayYear(curDateTime);
    }

    public string GetShortMonthDayYear(DateTime curDateTime)
    {
        int month = curDateTime.Month;
        int day = curDateTime.Day;
        int year = curDateTime.Year;
        string monthLocal = GetLocalStringByKey(MONTH_KEYS[month - 1]);
        monthLocal = monthLocal.Substring(0, 3);
        string final = monthLocal + " " + day.ToString() + "," + year.ToString();
        return final;
    }



    /// <summary>
    /// 1 ~ 12
    /// </summary>
    /// <param name="index"> 1~12  </param>
    /// <returns></returns>
    public string GetMonthByIndex(int index)
    {
        return GetLocalStringByKey(MONTH_KEYS[index - 1]);
    }


    public string GetMonth(int dayStamp)
    {
        DateTime curDataTime = new DateTime(1970, 1, 1) + new TimeSpan(dayStamp, 0, 0, 0);
        int month = curDataTime.Month;
        return GetLocalStringByKey(MONTH_KEYS[month - 1]);
    }

    public string GetWeekOf(int dayStamp)
    {
        string final = GetLocalStringByKey("WeekOf_");
        string date = GetMonthDayYear(dayStamp);
        return final.Replace("[DATE]", date);
    }

    public string GetContentWithDate(DateTime date,string key)
    {
        string final = GetLocalStringByKey(key);
        string dateStr = GetMonthDayYear(date);
        return final.Replace("[DATE]", dateStr);

    }

    private void UpdateLang(SystemLanguage lang)
    {
        _currentLang = lang;
        
        _spriteAtlas = Resources.Load<SpriteAtlas>(STR_LOCALIZATION_PREFIX + _currentLang.ToString());
        if (_spriteAtlas == null)
        {
            Debug.Log( _currentLang + " language not exist, change Language to English " );
            _currentLang = SystemLanguage.English;
            _spriteAtlas = Resources.Load<SpriteAtlas>(STR_LOCALIZATION_PREFIX + _currentLang.ToString());
        }

        if (_spriteAtlas == null)
        {
            Debug.Log("SpriteAltas not exist" + STR_LOCALIZATION_PREFIX + _currentLang.ToString());
        }
        
        var currentAsset = Resources.Load<TextAsset>(STR_LOCALIZATION_TEXT + _currentLang.ToString() + "_local");
        var tellAsset =  Resources.Load<TextAsset>(STR_TELLOUT_TEXT+ _currentLang.ToString() + "_local");
 
        currentTextsDict = MTJSON.Deserialize(currentAsset.ToString()).dict;
       if(tellAsset != null)
        {
            var tellDict = MTJSON.Deserialize(tellAsset.ToString()).dict;
            foreach (var p in tellDict )
            {
                if (currentTextsDict.ContainsKey(p.Key))
                {
                    currentTextsDict[p.Key] = p.Value;
                }
            }
        }

        

    }



    public string GetLocalStringByKey(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return "";
        }
        MTJSONObject value;
        if (currentTextsDict.TryGetValue(key, out value))
        {
            return value.s;
        }
       
        Debug.LogError("Localization Key not found " + key);
        return "";
    }
    public string GetCurrentISOName()
    {
        return GetISONameByLanguage(_currentLang);
    }

    public string GetISONameByLanguage(SystemLanguage lang)
    {
        switch (lang)
        {
        case SystemLanguage.Arabic:
            return "ar";
        case SystemLanguage.ChineseSimplified:
            return "zh";
        case SystemLanguage.ChineseTraditional:
            return "zt";
        case SystemLanguage.Danish:
            return "da";
        case SystemLanguage.Dutch:
            return "nl";
        case SystemLanguage.English:
            return "en";
        case SystemLanguage.Finnish:
            return "fi";
        case SystemLanguage.French:
            return "fr";
        case SystemLanguage.German:
            return "de";
        case SystemLanguage.Greek:
            return "el";
        case SystemLanguage.Hebrew:
            return "he";
        case SystemLanguage.Indonesian:
            return "id";
        case SystemLanguage.Italian:
            return "it";
        case SystemLanguage.Japanese:
            return "ja";
        case SystemLanguage.Korean:
            return "ko";
        case SystemLanguage.Norwegian:
            return "nb";
        case SystemLanguage.Polish:
            return "pl";
        case SystemLanguage.Portuguese:
            return "pt";
        case SystemLanguage.Russian:
            return "ru";
        case SystemLanguage.Spanish:
            return "es";
        case SystemLanguage.Swedish:
            return "sv";
        case SystemLanguage.Turkish:
            return "tr";
        default:
            return "en";
        }
    }

#if UNITY_EDITOR
    static    Dictionary<string, MTJSONObject> _editorDict;
    public static string GetEditorText(string key)
    {
        if (_editorDict == null)
        {
            var currentAsset = Resources.Load<TextAsset>(STR_LOCALIZATION_TEXT + "English_local");
            _editorDict = MTJSON.Deserialize(currentAsset.ToString()).dict;
        }
        return _editorDict[key].s;
    }


    private SpriteAtlas _editorspriteAtlas;
    public Sprite GetEditorSprite(string key)
    {
        if (_editorspriteAtlas == null)
        {
            _editorspriteAtlas = Resources.Load<SpriteAtlas>(STR_LOCALIZATION_PREFIX +"English");
        }
        Debug.Log("get key " + key);
        return _editorspriteAtlas.GetSprite(key);
    }

    IEnumerator testLang;
    void InstAutoTestLang()
    {
        testLang = nextLang();
        StartCoroutine(testLang);
    }

    void InstopTestLang()
    {
        if (testLang != null)
        {
            StopCoroutine(testLang);
        }
    }
    IEnumerator nextLang()
    {
        foreach (var l in langSet)
        {
            ChangeLang(l);
            yield return new WaitForSeconds(1f);
        }
    }

    //[MenuItem("TestLang/AutoTestLang")]
    //static void AutoTestLang()
    //{
    //    LocalizationManager.Instance.InstAutoTestLang();
        
    //}
    //[MenuItem("Clean/Clean all Local Image")]
    //public static void CleanLocalImage()
    //{
    //    var localImages = GameObject.FindObjectsOfType<LocalImage>();
    //    foreach (var localImage in localImages)
    //    {
    //        localImage.GetComponent<Image>().sprite = null;
    //    }
    //}

    //[MenuItem("TestLang/Stop Auto test lang")]
    //static void StopTestLang()
    //{
    //    LocalizationManager.Instance.InstopTestLang();
    //}

    //[MenuItem("TestLang/Arabic")]
    //static void ChnangToArabic()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Arabic);
    //}
    //[MenuItem("TestLang/ChineseSimplified")]
    //static void ChnangToChineseSimplified()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.ChineseSimplified);
    //}
    //[MenuItem("TestLang/ChineseTraditional")]
    //static void ChnangToChineseTraditional()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.ChineseTraditional);
    //}
    //[MenuItem("TestLang/Danish")]
    //static void ChnangToDanish()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Danish);
    //}
    //[MenuItem("TestLang/Dutch")]
    //static void ChnangToDutch()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Dutch);
    //}
    //[MenuItem("TestLang/English")]
    //static void ChnangToEnglish()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.English);
    //}
    //[MenuItem("TestLang/Finnish")]
    //static void ChnangToFinnish()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Finnish);
    //}
    //[MenuItem("TestLang/French")]
    //static void ChnangToFrench()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.French);
    //}
    //[MenuItem("TestLang/German")]
    //static void ChnangToGerman()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.German);
    //}
    //[MenuItem("TestLang/Greek")]
    //static void ChnangToGreek()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Greek);
    //}
    //[MenuItem("TestLang/Hebrew")]
    //static void ChnangToHebrew()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Hebrew);
    //}
    //[MenuItem("TestLang/Indonesian")]
    //static void ChnangToIndonesian()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Indonesian);
    //}
    //[MenuItem("TestLang/Italian")]
    //static void ChnangToItalian()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Italian);
    //}
    //[MenuItem("TestLang/Japanese")]
    //static void ChnangToJapanese()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Japanese);
    //}
    //[MenuItem("TestLang/Korean")]
    //static void ChnangToKorean()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Korean);
    //}
    //[MenuItem("TestLang/Norwegian")]
    //static void ChnangToNorwegian()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Norwegian);
    //}
    //[MenuItem("TestLang/Polish")]
    //static void ChnangToPolish()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Polish);
    //}
    //[MenuItem("TestLang/Portuguese")]
    //static void ChnangToPortuguese()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Portuguese);
    //}
    //[MenuItem("TestLang/Russian")]
    //static void ChnangToRussian()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Russian);
    //}
    //[MenuItem("TestLang/Spanish")]
    //static void ChnangToSpanish()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Spanish);
    //}
    //[MenuItem("TestLang/Swedish")]
    //static void ChnangToSwedish()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Swedish);
    //}
    //[MenuItem("TestLang/Turkish")]
    //static void ChnangToTurkish()
    //{
    //    LocalizationManager.Instance.ChangeLang(SystemLanguage.Turkish);
    //}


    HashSet<SystemLanguage> langSet = new HashSet<SystemLanguage>() {
                 SystemLanguage.Arabic,
                                SystemLanguage.ChineseSimplified,
                                SystemLanguage.ChineseTraditional,
                                SystemLanguage.Danish,
                                SystemLanguage.Dutch,
                                SystemLanguage.English,
                                SystemLanguage.Finnish,
                                SystemLanguage.French,
                                SystemLanguage.German,
                                SystemLanguage.Greek,
                                SystemLanguage.Hebrew,
                                SystemLanguage.Indonesian,
                                SystemLanguage.Italian,
                                SystemLanguage.Japanese,
                                SystemLanguage.Korean,
                                SystemLanguage.Norwegian,
                                SystemLanguage.Polish,
                                SystemLanguage.Portuguese,
                                SystemLanguage.Russian,
                                SystemLanguage.Spanish,
                                SystemLanguage.Swedish ,
             SystemLanguage.Turkish
};

#endif
}
