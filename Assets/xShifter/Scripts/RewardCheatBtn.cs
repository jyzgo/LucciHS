using EasyUI.Dialogs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCheatBtn :BtnExtension
{
    protected override void OnPressed()
    {
        ConfirmDialogUI.Instance
.SetTitle("Ads")
.SetMessage("Do you want to play ads to unlock this?")
.SetButtonsColor(DialogButtonColor.Yellow)
.SetFadeDuration(.6f)
.OnPositiveButtonClicked(() =>   AdsMgr.current.ShowRewardAds(CallBack))
.Show();

          }
    public string msg;
    protected void CallBack()
    {

        TimeLineMgr.current.PlayingTimeLine(msg);
    }

}
