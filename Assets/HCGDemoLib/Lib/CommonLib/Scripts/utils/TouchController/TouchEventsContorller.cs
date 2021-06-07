using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchEventsContorller :BeTouchContoller
{
    protected override void OnTouched(Vector3 pos)
    {
        actions.Invoke();
        
    }
    public UnityEvent actions;
}
