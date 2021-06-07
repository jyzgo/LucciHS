using NotifyManagerNS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NotifyActiveController : MonoBehaviour,INotifyListener
{

    public bool IsOn = true;
    protected string activeEventName = "";
    protected string inactiveEventName = "";



    /// <summary>
    /// Called in start
    /// </summary>
    protected abstract void Init();

    protected void Start()
    {
        Init();
        if (activeEventName.Length > 0)
        {
            NotifyManager.AddListener(activeEventName, this);
        }

        if (inactiveEventName.Length > 0)
        {
            NotifyManager.AddListener(inactiveEventName, this);
        }
    }


    public void OnNotifying(string eventName, params object[] data)
    {

        if (eventName.Equals(activeEventName))
        {
            Show();
        }
        else if (eventName.Equals(inactiveEventName))
        {
            Hide();
        }

    }

    protected abstract void Show();
    protected abstract void Hide();


    protected void OnDestroy()
    {
        if (activeEventName.Length > 0)
        {
            NotifyManager.RemoveListener(activeEventName, this);
        }
        if (inactiveEventName.Length > 0)
        {
            NotifyManager.RemoveListener(inactiveEventName, this);
        }
    }


}
