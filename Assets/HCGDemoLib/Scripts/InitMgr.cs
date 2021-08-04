using System.Collections;
using System.Collections.Generic;
using MonsterLove.StateMachine;
using MTUnity.Utils;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum InitGameState
{
    Ready,
    SubInit,
    Playing,
    SubWin,
    Check,
    Win,
    Lose
}

public class SentenceData
{
    public List<string> wordsList = new List<string>();
    public string Sentence = "";
}

public class LevelConfData
{
    public int LevelIndex;
    public List<int> levelIDs = new List<int>();
    public string LevelDes = "";
}

public class WordConfData
{
    public List<string> words = new List<string>();
    public string answer = "";
    public List<int> icons = new List<int>();
    public List<string> unusedWords = new List<string>();
}

public class iconConfData
{
    public int iconRef = 0;
    public string msg = "";
    public string des = "";
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

    public const string CUR_MAX_LEVEL_KEY = "MAX_LEVEL_KEY";
    public const string CUR_LEVEL_KEY = "CUR_LEVEL_KEY";

    public GameObject buyEmojiRoot;
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
        LoadConf();
    }

    StateMachine<InitGameState> _fsm;
    private void Start()
    {
        _fsm = StateMachine<InitGameState>.Initialize(this, InitGameState.Ready);
    }

    List<SentenceData> _datas = new List<SentenceData>();
    IEnumerator Ready_Enter()
    {
        print("Ready Game Ready");
        _datas.Clear();
        yield return new WaitForEndOfFrame();
        CallPlay();

    }

    void LoadConf()
    {
        print("load conf");
        curLevelIndex = PlayerPrefs.GetInt(CUR_LEVEL_KEY, 1);
        curPlayerMaxIndex = PlayerPrefs.GetInt(CUR_MAX_LEVEL_KEY, 1); 
        LoadLevelConf();
        LoadWordConf();
        LoadIconConf();
    }

    List<LevelConfData> _lvDatas = new List<LevelConfData>();
    void LoadLevelConf()
    { 
        _lvConf = PareseObj(gameConfEnumsFileEnum.levelConf);
        for (int i = 1; i < _lvConf.Count + 1; i++)
        {
            var curLevelJson = _lvConf["lv" + i];
            var data = new LevelConfData();
            data.LevelIndex = 1;
            data.LevelDes = curLevelJson["des"].ToString();

            for (int ji = 0; ji < 3; ji++)
            {
                var myI = (int)curLevelJson["id" + ji.ToString()];
                data.levelIDs.Add(myI);
            }
            _lvDatas.Add(data);
            //print("id + " + d.ToString());

        }
        //print("lev " + _lvConf.ToString() + " " + _lvConf.Count);
    }

    List<WordConfData> _wordConfDatas = new List<WordConfData>();
    void LoadWordConf()
    {
        _wordConf = PareseObj(gameConfEnumsFileEnum.wordConf);
        for (int i = 0; i < _wordConf.Count; i++)
        {
            var wc = _wordConf["id" + i.ToString()];
            var size = (int)wc["size"];
            
            var data = new WordConfData();
            data.answer = wc["answer"].ToString();
            for (int j = 0; j < size; j++)
            {
                var curWord = wc["word" + j.ToString()].ToString();
                var curIcon = wc["icon" + j.ToString()];
                int id = 0;
                if(curIcon != null)
                {
                    id = (int)curIcon;
                }
                data.words.Add(curWord);
                data.icons.Add(id);
            }
            var uWords = wc["uwords"];
            foreach (var w in uWords)
            {
                data.unusedWords.Add(w.ToString());
            }
            //print("wwww " + wc.ToString());
            _wordConfDatas.Add(data);
        }
    }


    void LoadIconConf()
    {
        _iconConf = PareseObj(gameConfEnumsFileEnum.iconConf);
    }

    JObject _lvConf;
    JObject _wordConf;
    JObject _iconConf;
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

    public Text sentence;
    public Image[] emojis;
    public SelectBtn[] selectBtns;
    public void LoadLevel(int index)
    {
        //if (index > MAX_LEVEL_INDEX)
        //{
        //    index = UnityEngine.Random.Range(2, MAX_LEVEL_INDEX - 1);
        //}
        if(index > _lvDatas.Count -1)
        {
            index = UnityEngine.Random.Range(2, _lvDatas.Count - 1);
        }
        //----

        //----
        _curLevelConfData = _lvDatas[index];
        _wordIndex = 0;
        _fsm.ChangeState(InitGameState.SubInit);
        print("load level des " + _curLevelConfData.LevelDes);
        //levelText.text = "Level " + curLevelIndex.ToString();
        //Debug.Log("index load " + index);
        //if (!Application.CanStreamedLevelBeLoaded(index))
        //{
        //    index = 1;
        //}
        //PlayerPrefs.SetInt(UNLOCK_PREF + index.ToString(), 1);
        //ClearSceneData.LoadLevelByIndex(index);
        
    }
    int _wordIndex = 0;

    public SpriteRef _spriteRef;
    readonly Color GREY = new Color(77f / 255f, 77f / 255f, 77f / 255f);
    void SubInit_Enter()
    {
        ClearAllBoard();
        int wordId =  _curLevelConfData.levelIDs[_wordIndex];
        print("Sub init " + wordId);
        _curWordData = _wordConfDatas[wordId];
        _btnTouchIndex = 0;
        sentence.text = _curWordData.answer;
        sentence.gameObject.SetActive(false);
        sentence.color = GREY;

        print("ioccc " + _curWordData.icons.Count);
        for (int i = 0; i < _curWordData.icons.Count; i++)
        {
            var iconId = _curWordData.icons[i];
            emojis[i].gameObject.SetActive(true);
            emojis[i].sprite = _spriteRef.refs[iconId];

        }

        List<string> btnContens = new List<string>();
        btnContens.AddRange(_curWordData.words);
        for (int i = 0; i < 9 - _curWordData.words.Count; i++)
        {
            btnContens.Add(_curWordData.unusedWords[i]);

        }
        btnContens.RandomShuffle();
        for (int i = 0; i < selectBtns.Length; i++)
        {
            selectBtns[i].gameObject.SetActive(true);
            selectBtns[i].ResetSelectBtn();
            selectBtns[i].btnContent.text = btnContens[i];

        }
        print("answer is " + _curWordData.answer);
        _isWinThisTime = true;

    }

    class TouchData
    {
        public SelectBtn btn;
        public bool right;
    }

    List<TouchData> touchDataList = new List<TouchData>();
    
    int _btnTouchIndex = 0;
    bool _isWinThisTime = true;

    public void BePressed(string word)
    {
        print("_bb " + _btnTouchIndex + " word " + word + " word count " + _curWordData.words.Count);

        if (!_curWordData.words[_btnTouchIndex].Equals(word))
        {
            print("ttt " + _curWordData.words[_btnTouchIndex] + " rr " + word);
            _isWinThisTime = false;
        }

        _btnTouchIndex++;
        if (_btnTouchIndex >= _curWordData.words.Count)
        {
            _fsm.ChangeState(InitGameState.Check);
        }
        print("Down " + word);
    }

    void Check_Enter()
    {

        sentence.gameObject.SetActive(true);
        if(_isWinThisTime)
        {
            _fsm.ChangeState(InitGameState.Win);
        }else
        {
            _fsm.ChangeState(InitGameState.Lose);
        }
    }

    IEnumerator Win_Enter()
    {
        buyEmojiRoot.SetActive(true);
        AdsMgr.current.ShowInter();
        sentence.color = Color.green;
        yield return new WaitForSeconds(2f);
        curLevelIndex++;
        if(curLevelIndex >= curPlayerMaxIndex)
        {
            curPlayerMaxIndex = curLevelIndex + 1;
        }

        PlayerPrefs.SetInt(CUR_LEVEL_KEY, curLevelIndex);
        PlayerPrefs.SetInt(CUR_MAX_LEVEL_KEY,curPlayerMaxIndex);
        _fsm.ChangeState(InitGameState.Ready);
    }

    IEnumerator Lose_Enter()
    {
        sentence.color = Color.red;
        yield return new WaitForSeconds(2f);
        _fsm.ChangeState(InitGameState.SubInit);
    }

    LevelConfData _curLevelConfData;
    WordConfData _curWordData;
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

        AdsMgr.current.ShowInter();
        CallPlay();
        print("tt " + TimeLineMgr.current.timelineIndex);
        //retryBtn.gameObject.SetActive(isFirst);
        //rewardBackBtn.gameObject.SetActive(!isFirst);
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
