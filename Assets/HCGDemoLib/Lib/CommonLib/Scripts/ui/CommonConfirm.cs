using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CommonConfirm : MonoBehaviour {

    public Canvas _curCanvas;

    public UnityEvent Yes;
    public UnityEvent No;

    public string content;
    public string title;

    public Text Content;
    public Text Title;


    public LocalText yesLocalKey;
    public LocalText noLocalKey;


    public BaseUIAction _slide;

    public bool isSetContent = true;
    private void Awake()
    {
        //_curCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        //_curCanvas.worldCamera = Camera.main;
        //_curCanvas.sortingLayerName = "upperUI";
        if (isSetContent)
        {
            SetContent(content);
            SetTitle(title);
        }

    }

    public void SetContent(string str)
    {
        if (Content != null)
        {
            content = str;
            Content.gameObject.SetActive(true);
            Content.text = content;
        }
    }


    public void SetTitle(string st,float fontSize=50f)
    {
        if (Title != null)
        {
            Title.gameObject.SetActive(true);
            title = st;
            Title.text = title;
        }
    }


    public void DisableTitleAndContent()
    {
        Title.gameObject.SetActive(false);
        Content.gameObject.SetActive(false);
        Yes = null;
        No = null;

    }

    public void YesCall()
    {
        if (Yes != null)
        {
            Yes.Invoke();
        }
        CloseWindow();
    }

    public void ShowWindowWithAnim()
    {
        gameObject.transform.SetAsLastSibling();
        if (_slide != null)
        {
            _slide.Show();
        }
    }
    

    IEnumerator DisableWindow(float t)
    {
        yield return new WaitForSeconds(t);

        gameObject.SetActive(false);
        DisableTitleAndContent();

    }

    void CloseWindow()
    {
        if (_slide != null)
        {
            var t = _slide.Hide();
            StartCoroutine(DisableWindow(t));
        }
        else
        {
            StartCoroutine(DisableWindow(0));
        }
    }

    public void Nocall()
    {
        if (No != null)
        {
            No.Invoke();
        }
        CloseWindow();
    }

    internal void SetYesBtnKey(string yesContent)
    {
        yesLocalKey.localizationKey = yesContent;
    }

    internal void SetNoBtnNoKey(string noContent)
    {
        noLocalKey.localizationKey = noContent;
    }

    internal void UpdateLocalKeys()
    {
        yesLocalKey.UpdateLocale();
        noLocalKey.UpdateLocale();
    }
}
