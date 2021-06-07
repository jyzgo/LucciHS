using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BGFitResize : MonoBehaviour {

    SpriteRenderer sr;
    [Header("Padding")]
    public int UpDown;
    public int LeftRight;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Start () {
        Resize();

    }
    void Resize()
    {
        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;


        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / (Screen.height-UpDown) * (Screen.width - LeftRight);

        float widthScale = worldScreenWidth / width;
        float heightScale = worldScreenHeight / height;


        if (widthScale < heightScale)
        {
            //use width as scale
            Vector3 xWidth = transform.localScale;
            xWidth.x = worldScreenWidth / width;
            xWidth.y = xWidth.x;
            transform.localScale = xWidth;
        }
        else
        {
            Vector3 yHeight = transform.localScale;
            yHeight.y = worldScreenHeight / height;
            yHeight.x = yHeight.y;
            transform.localScale = yHeight;
            
        }

    }

    public void SetBg(Sprite sp)
    {
        sr.sprite = sp;
        Resize();
    }


    Vector3 touchPos = Vector3.zero;

    public UnityEvent OnMouseDownEvent;
    private void OnMouseDown()
    {

        var v3 = Input.mousePosition;
        v3.z = 10f;
        touchPos = Camera.main.ScreenToWorldPoint(v3);
        if (OnMouseDownEvent != null && !IsPointerOverUIObject())
        {
            OnMouseDownEvent.Invoke();
        }


    }

    //When Touching UI
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }

}



