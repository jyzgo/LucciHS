using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public abstract class IScrollItem : MonoBehaviour
{
    private void Awake()
    {
        Rect = GetComponent<RectTransform>();
    }
    [HideInInspector]
    public RectTransform Rect;
    public abstract void SetIndex(int n);
}
