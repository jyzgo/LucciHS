using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopupMgr : MonoBehaviour
{

    static PopupMgr _current;
    public static PopupMgr current
    {
        get
        {
            if (m_ShuttingDown)
            {
                return null;
            }
            if (_current == null)
            {
                var init = Resources.Load("PopupMgr");
                var gb = Instantiate(init) as GameObject;
                DontDestroyOnLoad(gb);
                _current = gb.GetComponent<PopupMgr>();
            }
            return _current;
        }
    }

    private void Awake()
    {
        _curCanvas = GetComponent<Canvas>();
        //_curCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        //_curCanvas.worldCamera = Camera.main;
    }

    Canvas _curCanvas;

    static bool m_ShuttingDown = false;
    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }


    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }

    CommonConfirm _confirm;
    public string ConfirmPrefabName;
    void ActiveConfirm()
    {
        if(_confirm == null)
        {
            var confirmPrefab = Resources.Load(ConfirmPrefabName) as GameObject;
            var confirmGB = GameObject.Instantiate<GameObject>(confirmPrefab);
            _confirm = confirmGB.GetComponent<CommonConfirm>();
            _confirm.transform.SetAsFirstSibling();
            //_confirm.transform.parent = _current.transform;
        }
        //_current.transform.SetAsLastSibling();
    }

    public void ShowMsgPopUpWithTitle(string title,string msg, UnityEvent confirmAct)
    {
        ActiveConfirm();
        _confirm.DisableTitleAndContent();
        _confirm.SetContent(msg);
        _confirm.SetTitle(title);
        _confirm.Yes = confirmAct;
        _confirm.gameObject.SetActive(true);
        _confirm.ShowWindowWithAnim();
    }

    public void ShowMsgPopUp(string msg, UnityEvent confirmAct)
    {
        ActiveConfirm();
        _confirm.DisableTitleAndContent();
        _confirm.SetContent(msg);
        _confirm.Yes = confirmAct;
        _confirm.gameObject.SetActive(true);
        _confirm.ShowWindowWithAnim();
    }

    public void ShowYesNoPop(string msg, UnityEvent yes, UnityEvent noFunc)
    {
        ActiveConfirm();
        _confirm.DisableTitleAndContent();
        _confirm.SetContent(msg);
        _confirm.Yes = yes;
        _confirm.gameObject.SetActive(true);
        _confirm.ShowWindowWithAnim();
    }

    public void ShowYesNoPopWithTitle(string title, string msg, UnityEvent yesFunc, UnityEvent NoFunc)
    {
        ActiveConfirm();
        _confirm.DisableTitleAndContent();
        _confirm.SetTitle(title);
        _confirm.SetContent(msg);
        _confirm.Yes = yesFunc;
        _confirm.No = NoFunc;
        _confirm.gameObject.SetActive(true);
        _confirm.ShowWindowWithAnim();

    }
}
