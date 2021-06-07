using ActLib;
using MTUnity.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class IncreasedText : MonoBehaviour
{
    [Header("延迟多久执行")]
    public float delay = 0;
    [Header("每隔多久增加一次")]
    public float interval = 0;

    [Header("起始数值")]
    public float startNum = 0f;
    [Header("每次增加多少")]
    public float increaseEach = 0f;
    [Header("增加到多少")]
    public float endNum = 100f;



    [Header("数字前缀1 例如'X100' X就是前缀")]
    public string prex1;
    [Header("数字前缀2")]
    public string prex2;
    [Header("后缀1")]
    public string end1;
    [Header("后缀2")]
    public string end2;

    Text curText;
    // Start is called before the first frame update
    void Start()
    {
        curText = GetComponent<Text>();
        if (increaseEach > 0)
        {
            StartCoroutine(StartIncCount());
        }else
        {
            StartCoroutine(StartDecCount());
        }
    }

    IEnumerator StartIncCount()
    {
        yield return new WaitForSeconds(delay);
       
        while(startNum < endNum)
        {
            startNum += increaseEach;
            yield return new WaitForSeconds(interval);
            TextProcess();
        }

    }

    IEnumerator StartDecCount()
    {
        yield return new WaitForSeconds(delay);

        while (startNum > endNum)
        {
            startNum += increaseEach;
            yield return new WaitForSeconds(interval);
            TextProcess();
        }

    }


    public virtual void TextProcess()
    {
        curText.text = prex1 + prex2 + ((int)startNum).ToString() + end1 + end2;
    }

}
