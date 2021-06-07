using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Tab2Layer : MonoBehaviour
{
    // Start is called before the first frame update

    public Image[] bottomTab;
    public Text[] bottomText;

    public Image[] frontTab;
    public Text[] frontText;


    public int _index = 0;
    public int _tabSize = 0;
    public GameObject[] views;

    public event UnityAction<int> onTabChanged;



    void Start()
    {
        UpdateTab();

    }

    void UpdateTab()
    {
        for (int i = 0; i < bottomTab.Length; i++)
        {
            bottomTab[i].enabled = (_index != i);
            bottomText[i].gameObject.SetActive(_index != i);
            frontTab[i].enabled = (_index == i);
            frontText[i].gameObject.SetActive((_index == i));

        }

        for (int i = 0; i < views.Length; i++)
        {
            views[i].SetActive(i == _index);
        }

        //for (int i = 0; i < texts.Length; i++)
        //{
        //    texts[i].color = i == _index ? Color.white : Color.gray;

        //}

    }

    public void BePressed(int index)
    {

        _index = index;
        UpdateTab();
        if (onTabChanged != null)
        {
            onTabChanged(index);
        }

    }
}
