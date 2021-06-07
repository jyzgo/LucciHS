using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMsgBtn :BtnExtension
{
    public string msg;
    protected override void OnPressed()
    {

        TimeLineMgr.current.PlayingTimeLine(msg);
    }

}
