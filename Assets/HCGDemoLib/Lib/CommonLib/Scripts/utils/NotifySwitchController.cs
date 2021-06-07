using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NotifyManagerNS;


public class GraphicData
{
    public float originalOpacity;
    public bool originalActive;
    public Graphic graphic;
}
public abstract class NotifySwitchController : MonoBehaviour,INotifyListener
{
    public bool IsOn = true;
    public bool IsCtrlObjectActive = false;
    protected string activeEventName = "";
    protected string inactiveEventName = "";

    public float fadeSpeed = 0.1f;
    public float fadeDelay= 0f;
    public float showDelay= 0f;
    public float fadeOpacity = 0f;
    

    public Image[] images;
    public Text[] texts;


    public GameObject[] fadeRoots;
    public HashSet<GraphicData> _graphicSet = new HashSet<GraphicData>();


    /// <summary>
    /// SetEvents must called in awake
    /// </summary>
    protected abstract void SetEvents();
    

    // Start is called before the first frame update
    protected void Start()
    {
        if (activeEventName.Length > 0)
        {
            NotifyManager.AddListener(activeEventName, this);
        }

        if (inactiveEventName.Length > 0)
        {
            NotifyManager.AddListener(inactiveEventName, this);
        }

        foreach(var gb in fadeRoots)
        {
            var arr = gb.GetComponentsInChildren<Graphic>();
            AddToGraphicDict(arr);
        }
        AddToGraphicDict(images);
        AddToGraphicDict(texts);


        if (!IsOn)
        {
            //SetGrphicsDisActive(images);
            //SetGrphicsDisActive(texts);
            //SetGrphicsDisActive(rootGraphics);
            SetGrphicsDisActive();
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

    protected void Show()
    {
        IsOn = true;
        ShowGraphics();
    }
    protected void Hide()
    {
        IsOn = false;
        HideGraphic();
    }

    protected void SetGraphicsActive()
    {
        foreach (var data in _graphicSet)
        {
            var origincolor = data.graphic.color;
            origincolor.a -= data.originalOpacity;
            data.graphic.color = origincolor;
            if (IsCtrlObjectActive)
            {
                data.graphic.gameObject.SetActive(data.originalActive);
            }
        }
    }

    protected void SetGrphicsDisActive()
    {
        foreach (var data in _graphicSet)
        {
            var origincolor = data.graphic.color;
            origincolor.a -= data.originalOpacity;
            if (IsCtrlObjectActive)
            {
                data.graphic.gameObject.SetActive(false);
            }

        }
    }

   IEnumerator ShowGraphic(GraphicData data)
    {
        yield return new WaitForSeconds(showDelay);
        if (IsCtrlObjectActive)
        {
            data.graphic.gameObject.SetActive(data.originalActive);
        }
        Color color = data.graphic.color;
        while (color.a < data.originalOpacity)
        {
            color.a += fadeSpeed * Time.deltaTime;
            data.graphic.color = color;
                yield return null;
        }
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
    protected void ShowGraphics()
    {
        StopGrphicEnumerator();
        foreach(var data in _graphicSet)
        {
            var showIE = ShowGraphic(data);
            _showIEnumerators.Add(showIE);
            StartCoroutine(showIE);
        }
    }

    HashSet<IEnumerator> _hideIEnumerators = new HashSet<IEnumerator>();
    IEnumerator HideGraphic(GraphicData data)
    {
        yield return new WaitForSeconds(fadeDelay);
        Color color = data.graphic.color;
        data.originalOpacity = color.a;
        data.originalActive = data.graphic.gameObject.activeSelf;
        while (color.a > fadeOpacity)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            data.graphic.color = color;
            yield return null;
        }
        if (IsCtrlObjectActive)
        {
            data.graphic.gameObject.SetActive(false);
        }
    }

    protected void HideGraphic()
    {
        StopGrphicEnumerator(); 
        foreach (var data in _graphicSet)
        {
            var hideIe = HideGraphic(data);
            _hideIEnumerators.Add(hideIe);
            StartCoroutine(hideIe);
        }
    }

 

    public void OnNotifying(string eventName,params object[] data)
    {
        if (eventName.Equals(activeEventName))
        {
            Show();
        }
        else if (eventName.Equals(inactiveEventName))
        {
            Hide();
        }

    }

    protected void OnDestroy()
    {
        if (activeEventName.Length > 0)
        {
            NotifyManager.RemoveListener(activeEventName, this);
        }
        if (inactiveEventName.Length > 0)
        {
            NotifyManager.RemoveListener(inactiveEventName, this);
        }
    }


}
