using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimiatorCall : BaseCallAction
{
    public string animatorAction;
    public bool animBool = true;
    public Animator anim;
    public override void CallFunc()
    {
        anim.SetBool(animatorAction,animBool);
    }

}
