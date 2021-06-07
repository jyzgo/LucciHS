using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOpenPop : BtnExtension
{
    protected override void OnPressed()
    {
        PopupMgr.current.ShowMsgPopUp("This is msg tet", null);
    }

}
