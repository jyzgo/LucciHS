using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseCall : BaseCallAction
{
    public string winOrLose = "";
    public MonoBehaviour be;
    public override void CallFunc()
    {
        if(winOrLose.Equals("win"))
        {
            be.SendMessage("OnWin");
        }else
        {
            be.SendMessage("OnLose");
        }
    }

}
