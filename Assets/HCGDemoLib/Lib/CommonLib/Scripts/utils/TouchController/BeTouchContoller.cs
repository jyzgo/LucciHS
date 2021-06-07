using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D),typeof(Image))]
public abstract class BeTouchContoller : MonoBehaviour
{
 
   private void OnMouseDown()
    {
        var v3 = Input.mousePosition;
        //v3.z = 10f;
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(v3);
        if (IsPointerOverUIObject())
        {
            OnTouched(touchPos);
        }

    }
    protected abstract void OnTouched(Vector3 pos);

//When Touching UI
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //Debug.Log("re c  " + results.Count);
        //foreach (var v in results)
        //{
        //    Debug.Log(" v " + v.gameObject.name);
        //}
        return results.Count == 1;
    }


}
