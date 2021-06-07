using MTUnity.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SlideDir
{
    LeftToRight,
    RightToLeft,
    UpToDown,
    DownToUp,
    None
}

public enum SlidePOS
{
    Center,
    Reletive
}

[RequireComponent(typeof(RectTransform))]
public class SlideIn : BaseUIAction{

    // Use this for initialization
    RectTransform rect;
    float height;
    float width;
    Vector3 originPos;

    public SlideDir _slideDir;
    public SlidePOS _sidePos = SlidePOS.Reletive;
    public float ratio = 1f;

    public bool ShowOnEnable = true;

    public GameObject[] gameObjects;
    public Transform[] transforms;
    public UnityEvent enableAction;
    public UnityEvent disabelAction;


    bool isInit = false;
    private void Init()
    {
        if (isInit)
        {
            return;
        }
        isInit = true;
        rect = GetComponent<RectTransform>();

        height = rect.rect.height * ratio ;
        width = rect.rect.width * ratio;

        switch(_slideDir)
        {
            case SlideDir.DownToUp:
                width = 0f;
                break;
            case SlideDir.UpToDown:
                height *= -1 ;
                width = 0f;
                break;
            case SlideDir.RightToLeft:
                height = 0;
                width *= -1;
                break;
            case SlideDir.LeftToRight:
                height = 0;
                
                break;
        }


        originPos = rect.anchoredPosition;


    }
    void Awake()
    {
        Init(); 

    }

    void OnEnable () {
        if (ShowOnEnable)
        {
            Show();
        }
	}

    public bool resetOnDisable = true;
    private void OnDisable()
    {
        if (resetOnDisable)
        {
            rect.anchoredPosition = originPos;
        }
    }

    public const float MoveTime = 0.3f;
    const float RATE = 1.7f;
    public override  float Hide()
    {
        Init();
        if (disabelAction != null && gameObject.activeInHierarchy)
        {
            disabelAction.Invoke();
        }
        gameObject.StopAllActions();
        gameObject.ForceRunActions(new MTEaseInOut(new MTUIAnchorPositionChangeTo(MoveTime, originPos), RATE));
        foreach (var gb in gameObjects)
        {
            gb.SetActive(false);
        }
        return MoveTime;

    }

    public override float Show()
    {
        Init();
        if(enableAction!= null && gameObject.activeInHierarchy)
        {
            enableAction.Invoke();
        }
        if (_slideDir == SlideDir.None)
        {
            return 0;
        }
        var newPos = Vector2.zero;
        if (_sidePos == SlidePOS.Reletive)
        {
            newPos = new Vector2(originPos.x + width, originPos.y + height);
        }
        rect.anchoredPosition = originPos;
        gameObject.StopAllActions();
        gameObject.ForceRunActions(new MTEaseOut(new MTUIAnchorPositionChangeTo(MoveTime, newPos),RATE));

        foreach (var gb in gameObjects)
        {
            gb.SetActive(true);
        }

        return MoveTime;
    }

  
}
