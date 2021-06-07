using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Toggle))]
public abstract class ToggleExtension: MonoBehaviour
{
    protected Toggle _toggle; 
    protected void InitInAwake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(OnToggleTouched);
    }

    protected void Awake()
    {
        InitInAwake();
    }

    protected abstract void OnToggleTouched(bool b);
}