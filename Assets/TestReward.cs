using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReward :BtnExtension
{
    protected override void OnPressed()
    {
        AdsMgr.current.ShowRewardAds(CallBack);
    }

    public void CallBack()
    {
        print("test");
        InitMgr.current.buyEmojiRoot.SetActive(false);
    }
}
