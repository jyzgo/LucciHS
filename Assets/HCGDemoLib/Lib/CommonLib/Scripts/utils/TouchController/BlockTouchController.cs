using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchReciver
{
    void BeTouched(Vector3 pos); 
}

public class BlockTouchController : BeTouchContoller
{

    public MonoBehaviour[] reciverObjects;
    HashSet <ITouchReciver> recivers = new HashSet<ITouchReciver>();
    private void Awake()
    {
        foreach (var reciverObject in reciverObjects)
        {
            var mono = reciverObject;
            var it = mono as ITouchReciver;
            if (it != null)
            {
                recivers.Add(it);

            }else
            {
                Debug.LogError(mono.name + " don't have reciver interface");
            }

        }
        
    }

    public ITouchReciver reciver;
    protected override void OnTouched(Vector3 pos)
    {
        foreach (var reciver in recivers)
        {
            reciver.BeTouched(pos);
        }
    }

}
