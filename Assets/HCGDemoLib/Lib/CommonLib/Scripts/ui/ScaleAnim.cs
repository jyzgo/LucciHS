using UnityEngine;
using System.Collections;
using MTUnity.Actions;

public class ScaleAnim : BaseUIAction{

    void Awake()
    {
        _oriScale = transform.localScale;
    }

    Vector3 _oriScale;

    const float t = 0.1f;
    void OnEnable()
    {
        if (isShowOnEnable)
        {
            Show();
        }
    }

    public bool isShowOnEnable = false;


    public override float Show()
    {
        transform.localScale = new Vector3(t, 0.1f, 0.1f);
        var scale1 = new MTScaleTo(t * 2f, _oriScale * 1.1f);
        var scale2 = new MTScaleTo(t, 0.95f * _oriScale);
        var scale3 = new MTScaleTo(t, 1.02f * _oriScale);
        var scale4 = new MTScaleTo(t, _oriScale);
        this.RunAction(new MTSequence(scale1, scale2, scale3, scale4));
        return t * 5f;
    }

    const float HIDE_TIME = 0.15f;
    public override float Hide()
    {
        var finalScale = new Vector3(t, 0.1f, 0.1f);

        var scale4 = new MTScaleTo(HIDE_TIME, finalScale);
        this.RunAction(scale4);
        return HIDE_TIME;
    }
}
