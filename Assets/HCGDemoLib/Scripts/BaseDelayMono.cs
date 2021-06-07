using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDelayMono : MonoBehaviour
{
    Dictionary<string, IEnumerator> _dict = new Dictionary<string, IEnumerator>();
    IEnumerator _delayFunc;
    public void DelayCallFunc(string key,float t, Action f)
    {
        _delayFunc = null;
        if(_dict.TryGetValue(key,out _delayFunc))
        {
            if (_delayFunc != null)
            {
                StopCoroutine(_delayFunc);
            }
            _delayFunc = delayCall(t, f);
        }
        else
        {
            _delayFunc = delayCall(t, f);
            _dict.Add(key, _delayFunc);
        }
        StartCoroutine(_delayFunc);

    }
    IEnumerator delayCall(float t, Action f)
    {
        yield return new WaitForSeconds(t);
        f?.Invoke();
    }
}
