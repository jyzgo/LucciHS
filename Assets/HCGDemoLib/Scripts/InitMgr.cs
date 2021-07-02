using System.Collections;
using System.Collections.Generic;
using MonsterLove.StateMachine;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum InitGameState
{
    Ready,
    Playing,
    Win,
    Lose
}

public class SentenceData
{
    public List<string> wordsList = new List<string>();
    public string Sentence = "";
}

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
                var gb = Instantiate(init);
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
        if(_current != null)
        {
            Destroy(gameObject);
            return;
        }
        _current = this;
        DontDestroyOnLoad(gameObject);
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

    StateMachine<InitGameState> _fsm;
    private void Start()
    {
        _fsm = StateMachine<InitGameState>.Initialize(this, InitGameState.Ready);
    }

    List<SentenceData> _datas = new List<SentenceData>();
    void Ready_Enter()
    {
        print("Ready Game Ready");
        ClearAllBoard();
        _datas.Clear();
        LoadConf();
    }

    void LoadConf()
    {
        print("load conf");
        _lvConf = PareseObj(gameConfEnumsFileEnum.levelConf);
    }

    JObject _lvConf;
    public static JObject PareseObj(gameConfEnumsFileEnum curEnum)
    {
        var fileName = curEnum.ToString();
        TextAsset lvCostData = Resources.Load<TextAsset>("Config/" + fileName);
        var jObj = JObject.Parse(lvCostData.text);
        return jObj;
    }

    void TestAdd()
    {
        var d = new SentenceData();
        d.wordsList.Add("cat");
        d.wordsList.Add("fart");
        d.wordsList.Add("v");
        d.wordsList.Add("puke");
        d.Sentence = "My cat farted so violently I puked.";
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
    public Text levelText;
    public Transform linesPanel;
    public Transform emojiPanel;
    public Transform selectPanel;

    public void LoadLevel(int index)
    {
        if (index > MAX_LEVEL_INDEX)
        {
            index = UnityEngine.Random.Range(2, MAX_LEVEL_INDEX - 1);
        }
        //----
        ClearAllBoard();
        //----
        //Debug.Log("index load " + index);
        //if (!Application.CanStreamedLevelBeLoaded(index))
        //{
        //    index = 1;
        //}
        //PlayerPrefs.SetInt(UNLOCK_PREF + index.ToString(), 1);
        //ClearSceneData.LoadLevelByIndex(index);
        
    }

    void ClearAllBoard()
    {
        foreach(Transform l in linesPanel)
        {
            l.gameObject.SetActive(false);
        }

        foreach(Transform e in emojiPanel)
        {
            e.gameObject.SetActive(false);
        }

        foreach(Transform e in selectPanel)
        {
            e.gameObject.SetActive(false);
        }
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
    public GameObject playRoot;

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
