using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class BtnExtension : MonoBehaviour
{
    protected Button _button;
    protected void InitInAwake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PressedAction);
    }

    private void Awake()
    {
        InitInAwake();
    }
    protected bool ShowBtn = true;

    protected virtual void PressedAction()
    {
        if (lastPressTime  + pressInterval < Time.time )
        {
            lastPressTime = Time.time;
            OnPressed();
        }
    }

    [HideInInspector]
    public float pressInterval = 0.5f;
    [HideInInspector]
    public float lastPressTime = 0f;


    public Vector3 GlowPosOffest = Vector3.zero;
    public Vector3 GlowScale = Vector3.one;


    protected abstract void OnPressed();
 
}
