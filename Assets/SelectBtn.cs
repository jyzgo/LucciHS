using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectBtn : MonoBehaviour,IPointerDownHandler
{
    Toggle tog;
    private void Awake()
    {
        tog = GetComponent<Toggle>();
    }

    bool isTouched = false;
    public void ResetSelectBtn()
    {
        isTouched = false;
        tog.interactable = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isTouched)
        {
            InitMgr.current.BePressed(btnContent.text);
        }
        tog.interactable = false;
        isTouched = true;
        
    }

    public Text btnContent;

    public BaseEventData OnSelect { get; private set; }
    public PointerEventData OnClicked { get; private set; }
}
