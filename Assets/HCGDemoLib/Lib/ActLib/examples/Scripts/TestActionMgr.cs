using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActLib;
public class TestActionMgr : MonoBehaviour
{
    public GameObject sp1;
    public GameObject sp2;

    // Start is called before the first frame update
    void Start()
    {
        sp1.PlayAct(new ActSeqences(new ActMoveToLocal(3, new Vector3(1, 1, 2)), new ActMoveToLocal(3,Vector3.zero)));
        sp1.StopPlayAllActs();
        sp2.PlayAct(new ActSeqences(new ActMoveToLocal(3, new Vector3(1, 1, 2)), new ActMoveToLocal(3, Vector3.zero)));
    }

}
