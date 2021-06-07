using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  NotifyEnableController :NotifyActiveController
{
    public GameObject[] activeGameobjects;
    public float hideDelay = 0f;
    public float showDelay = 0f;
    protected override void Hide()
    {
        StartCoroutine(DelayHide());
    }
    protected IEnumerator DelayHide()
    {
        yield return new WaitForSeconds(hideDelay);
        SetGameObjectsActive(false);
    }

    protected override void Show()
    {
        StartCoroutine(DelayShow());
        
    }

    protected void SetGameObjectsActive(bool isOn)
    {
        IsOn = isOn;
        foreach (var activeGameobject in activeGameobjects)
        {
            activeGameobject.SetActive(isOn);
        }

    }
    protected IEnumerator DelayShow()
    {
        yield return new WaitForSeconds(showDelay);
        SetGameObjectsActive(true);
 
    }

  

}
