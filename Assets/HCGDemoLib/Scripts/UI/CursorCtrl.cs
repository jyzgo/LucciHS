using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D cursora;
    public Texture2D cursorb;

    private bool isclick = false;
    public CursorMode cm = CursorMode.Auto;//渲染形式，auto为平台自适应显示

    // Use this for initialization
    void Start()
    {
        Cursor.SetCursor(cursorb, Vector2.zero, cm);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(cursora, Vector2.zero, cm);
        }
        else
        {
            Cursor.SetCursor(cursorb, Vector2.zero, cm);
        }
    }
}
