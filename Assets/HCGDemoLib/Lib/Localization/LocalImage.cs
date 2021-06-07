using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LocalImage :LocalBase
{

    Image _image;

    protected override void Awake()
    {
        base.Awake();
        _image = GetComponent<Image>();
    }


    public override void UpdateLocale()
    {
        var sp= LocalizationManager.Instance.GetSprite(localizationKey);
        _image.sprite = sp;
    }

#if UNITY_EDITOR
    public void UpdateEditor()
    {
        GetComponent<Image>().sprite = LocalizationManager.Instance.GetEditorSprite(localizationKey);
    }

#endif

}
