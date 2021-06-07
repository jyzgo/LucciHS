using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonEventListener : MonoBehaviour
{
    ButtonEventSender[] _btnSenders;
    protected void Awake()
    {
        _btnSenders = GetComponentsInChildren<ButtonEventSender>();
        foreach (var sender in _btnSenders)
        {
            sender.RegisteListener(this);
        }
    }

    public abstract void ButtonPressed(int key);

}
