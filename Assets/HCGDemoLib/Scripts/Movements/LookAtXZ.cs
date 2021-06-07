using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtXZ : MonoBehaviour
{
    public Transform Target;
    void Update()
    {
        var pos = new Vector3(Target.position.x, transform.position.y, Target.position.z);
        transform.LookAt(pos);
    }
}
