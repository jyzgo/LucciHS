using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDisable :BtnExtension
{
    public GameObject root;
    protected override void OnPressed()
    {
        if (root != null)
        {
            root.SetActive(false);
        }else
        {
            transform.parent.gameObject.SetActive(false);
        }
        //gameObject.SetActive(false);
    }

 }
