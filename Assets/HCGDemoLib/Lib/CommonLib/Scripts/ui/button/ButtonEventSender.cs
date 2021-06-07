using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonEventSender : MonoBehaviour
{
    protected HashSet<ButtonEventListener> _listeners = new HashSet<ButtonEventListener>();
    protected Button _button;
    protected void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnPressed);
    }


    protected abstract int GetButtonKey();
    private void OnPressed()
    {
        int buttonKey = GetButtonKey();
        foreach (var listener in _listeners)
        {
            listener.ButtonPressed(buttonKey);
        }
    }

    public void RegisteListener(ButtonEventListener listener)
    {
        _listeners.Add(listener);

    }


}
