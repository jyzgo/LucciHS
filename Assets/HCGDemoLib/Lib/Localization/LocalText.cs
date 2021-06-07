using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalText :LocalBase
{
    Text _text;

    public override void UpdateLocale()
    {
        if (_text == null)
        {
            Debug.Log("text " + localizationKey);
        }
        _text.text = LocalizationManager.Instance.GetLocalStringByKey(localizationKey.ToString());
    }


    protected override void Awake()
    {
        base.Awake();
        _text = GetComponent<Text>();
    }

#if UNITY_EDITOR
    public void UpdateEditor()
    {
        
        GetComponent<Text>().text = LocalizationManager.GetEditorText(localizationKey);
    }

#endif

}
