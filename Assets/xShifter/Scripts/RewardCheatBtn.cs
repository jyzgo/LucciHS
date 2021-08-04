using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCheatBtn :BtnExtension
{
    protected override void OnPressed()
    {
        AdsMgr.current.ShowRewardAds(DoNothing);
    }
    void DoNothing()
    {
    }

}
