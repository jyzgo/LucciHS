using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BGResize : MonoBehaviour,INotifyListener {

    SpriteRenderer sr;
    public static BGResize current;
    public bool isPortrait = true;
    void Awake()
    {
        current = this;
        sr = GetComponent<SpriteRenderer>();
    }

    

	// Use this for initialization
	void Start () {
        SceneManager.sceneLoaded +=OnSceneLoaded;
        CheckIsTilting();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //UnconditionalResize();
    }

   
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }
    public void OnNotifying(string eventName, params object[] data)
    {
        CheckIsTilting();

    }

   
    bool isTilting = false;
    void CheckIsTilting()
    {
        Input.gyro.enabled = isTilting;
        if(!isTilting)
        {
            BackPos();
        }

    }

    float GetAroundNum(float v)
    {
        return v % 180f - (int)(v / 180f) * 180f;
    }




    void BackPos()
    {
        transform.position = new Vector3(0, 0, 100);
    }

    void Tilting()
    {
        if(isTilting)
        {
            var q = Input.gyro.attitude.eulerAngles;
            var y = GetAroundNum(q.x)/180f *3f;
            var x = GetAroundNum(q.y)/180f *2f;
            transform.position = new Vector3(x * 0.2f,y * 0.4f,  100);
        }

    }
    float screenWidth = 0;
    float screenHeight = 0;

    private void Update()
    {
        //Resize();
        Tilting();
    }

    public void UnconditionalResize()
    {

            screenWidth = Screen.width;
            screenHeight = Screen.height;
               transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;


        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 xWidth = transform.localScale;
        xWidth.x = worldScreenWidth / width;
        transform.localScale = xWidth;
        //transform.localScale.x = worldScreenWidth / width;
        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height;
        transform.localScale = yHeight * 1.2f;
        if(!isPortrait)
        {
            var tempx = transform.localScale.x;
            var tempy = transform.localScale.y;
            var tempz = transform.localScale.z;
            transform.localScale = new Vector3(tempy, tempx, tempz);

        }
        //transform.localScale.y = worldScreenHeight / height;


    }
    public void Resize()
    {

        if (screenWidth == Screen.width && screenHeight == Screen.height)
        {
            return;
        }
        UnconditionalResize();
    }

    

    public void SetBg(Sprite sp)
    {
        sr.sprite = sp;

        //Resize();
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


