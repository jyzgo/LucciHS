using MonsterLove.StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public enum TimelineGameStates
{
    Init,
    Playing,
    WaitForAnim,
    Anim,
    CheckWin,
    Win,
    Lose
}

public enum WinState
{
    None,
    Win,
    Lose,
}

[Serializable]
public class TimeLineData
{
    public string timelineKey;
    public PlayableDirector director;
    public PlayableDirector nextDir;
    public WinState winState;
}

public class TimeLineMgr : MonoBehaviour
{

    public TimeLineData[] _datas;
    StateMachine<TimelineGameStates> fsm;
    public Dictionary<string, TimeLineData> _playDir = new Dictionary<string, TimeLineData>();

    public static TimeLineMgr current;
    private void Awake()
    {
        current = this;
        fsm = StateMachine<TimelineGameStates>.Initialize(this, TimelineGameStates.Init);
        foreach(var v in _datas)
        {
            _playDir.Add(v.timelineKey, v);
        }
        timelineIndex = 0;
        StartGameTime = Time.time;
    }

    private void Start()
    {
        AnalyzeMgr.current.OnLevelStart(InitMgr.current.GetCurrentLevelIndex());
    }

    IEnumerator Init_Enter()
    {
        yield return new WaitForSeconds(0.2f);
        fsm.ChangeState(TimelineGameStates.Playing);
    }


    public int timelineIndex = 0;

    TimeLineData _curPlayData;
    public void PlayingTimeLine(string s)
    {
        if(!(fsm.State == TimelineGameStates.Playing))
        {
            print("is not playing " + s);
            return;
        }

        //AnalyzeMgr.current.
        print("play timeline " + s);
        TimeLineData data;
        if(_playDir.TryGetValue(s,out data))
        {
            _winState= data.winState;
            var dir = data.director;
            dir.Play();
            CurTimelineName = s;
            _dirDuration = (float)dir.duration;
            _curPlayData = data;
            timelineIndex++;
            fsm.ChangeState(TimelineGameStates.Anim);

        }
    }

    float _dirDuration = 0;
    WinState  _winState;

    const float MAX_WAIT_TIME = 20f;
    IEnumerator Anim_Enter()
    {
        print("anim enter");
        if(_winState == WinState.Win)
        {
            if (_dirDuration > MAX_WAIT_TIME)
            {
                _dirDuration = MAX_WAIT_TIME;
            }
        }
        yield return new WaitForSeconds(_dirDuration);
        if(_winState == WinState.Win)
        {
            fsm.ChangeState(TimelineGameStates.Win);

        }else if(_winState == WinState.Lose)
        {
            fsm.ChangeState(TimelineGameStates.Lose);
        }else
        {

            if (_curPlayData != null && _curPlayData.nextDir != null)
            {
                _curPlayData.nextDir.Play();
                _dirDuration = (float)_curPlayData.nextDir.duration;
                fsm.ChangeState(TimelineGameStates.Playing);
                _curPlayData = null;
            }
            else
            {
                fsm.ChangeState(TimelineGameStates.Playing);
            }
        }
    }

    void Playing_Enter()
    {
        print("playing");
    }


    void Lose_Enter()
    {
        print("Lose");
        InitMgr.current.ToLose();
    }

    void Win_Enter()
    {
        print("win");
        
        InitMgr.current.ToWin();
    }

    public static float StartGameTime = 0f;

    public static string CurTimelineName { get; internal set; }

    public void BackToLastTimeLine()
    {
        //if (_curIndex > 1)
        //{
            //_curIndex--;
            //btnNamesList.RemoveAt(btnNamesList.Count - 1);

            var dir = _curPlayData.director; // _curTimeLineData.Directors[_curIndex];
            dir.Play();
            dir.time = 0;
            dir.Stop();
            dir.Evaluate();
            fsm.ChangeState(TimelineGameStates.Playing);
        //}

    }
}
