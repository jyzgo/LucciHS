using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeContoller : MonoBehaviour
{
    public float fadeTime = 1f;
    public float fadeDelay= 0f;
    public float showDelay= 0f;
    public float fadeOpacity = 0f;
    

    public GameObject[] fadeRoots;
    public HashSet<GraphicData> _graphicSet = new HashSet<GraphicData>();


    private void Start()
    {
      
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        //Your Function You Want to Call
        foreach (var gb in fadeRoots)
        {
            var arr = gb.GetComponentsInChildren<Graphic>();
            AddToGraphicDict(arr);
        }

    }

    void AddToGraphicDict(IEnumerable<Graphic> graphics)
    {
        foreach (var g in graphics)
        {
            var data = new GraphicData {
                originalOpacity = g.color.a,
                originalActive = g.gameObject.activeSelf,
                graphic = g

            };
            _graphicSet.Add(data);
        }
    }

    protected void SetGraphicsActive()
    {
        foreach (var data in _graphicSet)
        {
            var origincolor = data.graphic.color;
            origincolor.a -= data.originalOpacity;
            data.graphic.color = origincolor;
        }
    }

    protected void SetGrphicsDisActive()
    {
        foreach (var data in _graphicSet)
        {
            var origincolor = data.graphic.color;
            origincolor.a -= data.originalOpacity;

        }
    }

   IEnumerator ShowGraphic(GraphicData data)
    {
        Color color = data.graphic.color;
        float start = color.a;
        float end = data.originalOpacity;

        yield return new WaitForSeconds(showDelay);
        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeTime;
            color.a = Mathf.Lerp(start, end, normalizedTime);
            data.graphic.color = color;
            yield return null;
        }
        color.a = end;
        data.graphic.color = color;
    }

   

    void StopGrphicEnumerator()
    {
        foreach(var showIe in _showIEnumerators)
        {
            StopCoroutine(showIe);
        }
        _showIEnumerators.Clear();
        foreach(var hideIE in _hideIEnumerators)
        {
            StopCoroutine(hideIE);
        }
        _hideIEnumerators.Clear();
    }

    HashSet<IEnumerator> _showIEnumerators = new HashSet<IEnumerator>();
    public float FadeIn()
    {
        StopGrphicEnumerator();
        foreach(var data in _graphicSet)
        {
            var showIE = ShowGraphic(data);
            _showIEnumerators.Add(showIE);
            StartCoroutine(showIE);
        }
        return showDelay + fadeTime;
    }

    HashSet<IEnumerator> _hideIEnumerators = new HashSet<IEnumerator>();
    IEnumerator HideGraphic(GraphicData data)
    {
        //yield return new WaitForSeconds(fadeDelay);
        //data.originalActive = data.graphic.gameObject.activeSelf;
        //while (color.a > fadeOpacity)
        //{
        //    color.a -= fadeSpeed * Time.deltaTime;
        //    data.graphic.color = color;
        //    yield return null;
        //}

        yield return new WaitForSeconds(fadeDelay);
        Color color = data.graphic.color;
        data.originalOpacity = color.a;
        float start = color.a;
        float end = fadeOpacity;
        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeTime;
            color.a = Mathf.Lerp(start, end, normalizedTime);
            data.graphic.color = color;
            yield return null;
        }
        color.a = end;
        data.graphic.color = color;



    }

    public float FadeOut()
    {
        StopGrphicEnumerator(); 
        foreach (var data in _graphicSet)
        {
            var hideIe = HideGraphic(data);
            _hideIEnumerators.Add(hideIe);
            StartCoroutine(hideIe);
        }
        return fadeDelay + fadeTime;
    }


}
