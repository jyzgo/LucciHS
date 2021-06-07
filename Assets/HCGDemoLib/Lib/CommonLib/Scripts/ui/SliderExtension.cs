using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class SliderExtension : MonoBehaviour
{
    protected Slider _slider;
    protected void InitInStart()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(OnDraged);
        
    }
    protected void StartSetValue(float f)
    {
        if (_delaySeter != null)
        {
            StopCoroutine(_delaySeter);
        }
        _delaySeter = DelaySet(f);
        StartCoroutine(_delaySeter);

    }
   
    protected virtual void SetWhenDragStop(float f)
    {
    }
    protected IEnumerator _delaySeter;

    protected IEnumerator DelaySet(float f)
    {
        yield return new WaitForSeconds(0.1f);
        SetWhenDragStop(f);
    }

    protected abstract void OnDraged(float f);

}
