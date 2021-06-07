using MTUnity.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionUtils 
{


    public static MTFiniteTimeAction GetBounce(float moveTime,Transform tar, Vector3 pos,float bounceRatio = 0.15f)
    {
        Vector3 from = tar.position;
        Vector3 normalize = (pos - from).normalized;
        Vector3 bounceOverPos = pos + normalize * bounceRatio;
        Vector3 bounceBackPos = pos - normalize * bounceRatio;
        float bounceInterval = 0.02f;
        float realMoveTime = moveTime - bounceInterval * 3f;
        var seq = new MTSequence(new MTMoveToWorld(realMoveTime, bounceOverPos), new MTMoveToWorld(bounceInterval * 1f, bounceBackPos), new MTMoveToWorld(bounceInterval, pos));
        return seq;
    }


}
