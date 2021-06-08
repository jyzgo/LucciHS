using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitMgr : MonoBehaviour
{
    static InitMgr _current;

    public static InitMgr current
    {
        get
        {

            if (m_ShuttingDown)
            {
                return null;
            }
            if (_current == null)
            {
                var init = Resources.Load("InitMgr");
                var gb = Instantiate(init) as GameObject;
                _current = gb.GetComponent<InitMgr>();
            }
            return _current;
        }
    }
    public void InitCall()
    {

    }

    //public Button[] resetBtns;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //foreach(var b in resetBtns)
        //{
        //    b.onClick.AddListener(RestartLevel);
        //}
        foreach (var n in nextLevelBtn)
        {
            n.onClick.AddListener(playNextLevel);
        }

        foreach (var n in noThanksBtn)
        {
            n.onClick.AddListener(RestartLevel);
        }
        if (rewardBackBtn != null)
        {
            rewardBackBtn.onClick.AddListener(rewardBack);
        }
        if (retryBtn != null)
        {
            retryBtn.onClick.AddListener(rewardBackToLast);
        }
        DisableAllUI();
    }

    private void rewardBack()
    {
        DisableAllUI();
        AdsMgr.current.ShowRewardAds(rewardBackToLast);
    }

    void rewardBackToLast()
    {
        DisableAllUI();
        TimeLineMgr.current.BackToLastTimeLine();
        print("reward Back");
    }

    float playLevelTapTime = 0f;
    public const float INTERVALE_TAP = 1f;

    private void playNextLevel()
    {
        if (playLevelTapTime + INTERVALE_TAP > Time.time)
        {
            return;
        }

        curLevelIndex++;
        DisableAllUI();
        CallPlay();
    }



    int curPlayerMaxIndex = 1;
    int curLevelIndex = 1;

    float gameStartTime;
    private void CallPlay()
    {
        print("cur index " + curLevelIndex + " max " + curPlayerMaxIndex);

        playLevelTapTime = Time.time;
        if (curLevelIndex >= curPlayerMaxIndex)
        {
            //Debug.Log("play level  " + (curLevelIndex).ToString());
            if (AnalyzeMgr.current != null)
            {
                AnalyzeMgr.current.OnFirstPlayNextLevel(curLevelIndex);
            }
        }
        //HideWinCanvas();
        gameStartTime = Time.time;
        LoadLevel(curLevelIndex);
    }



    public int MAX_LEVEL_INDEX = 6;
    public const string UNLOCK_PREF = "UNLOCK";
    public void LoadLevel(int index)
    {
        if (index > MAX_LEVEL_INDEX)
        {
            index = UnityEngine.Random.Range(2, MAX_LEVEL_INDEX - 1);
        }
        //Debug.Log("index load " + index);
        if (!Application.CanStreamedLevelBeLoaded(index))
        {
            index = 1;
        }
        PlayerPrefs.SetInt(UNLOCK_PREF + index.ToString(), 1);
        ClearSceneData.LoadLevelByIndex(index);
        //OnPlayCanvas.gameObject.SetActive(true);

        //SceneManager.LoadScene("StayAlive_level_" + index.ToString("000"));
        //AdsMgr.current.RequestInterstitial();
        //AdsMgr.current.CreateAndLoadRewardedAd();
        //UpdateHpStatus();
    }

    static bool m_ShuttingDown = false;
    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }


    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }


    public GameObject winRoot;
    public GameObject loseRoot;

    public void DisableAllUI()
    {
        winRoot.SetActive(false);
        loseRoot.SetActive(false);
    }
    public void ToWin()
    {
        AnalyzeMgr.current.OnLevelWon(curLevelIndex, (int)(Time.time-TimeLineMgr.StartGameTime));
        AdsMgr.current.ShowInter();
        DisableAllUI();
        winRoot.SetActive(true);
    }

    public void ToLose()
    {
        AnalyzeMgr.current.OnLevelLose(curLevelIndex, "none");
        DisableAllUI();
        loseRoot.SetActive(true);
        bool isFirst = TimeLineMgr.current.timelineIndex == 1;

        print("tt " + TimeLineMgr.current.timelineIndex);
        retryBtn.gameObject.SetActive(isFirst);
        rewardBackBtn.gameObject.SetActive(!isFirst);
    }

    public void RestartLevel()
    {
        AdsMgr.current.ShowInter();
        DisableAllUI();
        ClearSceneData.LoadLevelByIndex(curLevelIndex);
    }

    internal int GetCurrentLevelIndex()
    {
        return curLevelIndex;
    }

    public Button[] nextLevelBtn;
    public Button[] noThanksBtn;
    public Button rewardBackBtn;
    public Button retryBtn;


}
