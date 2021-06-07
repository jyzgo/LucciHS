using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActLib
{
    public class testActLib : MonoBehaviour
    {
        public GameObject gb;
        // Start is called before the first frame update
        void Start()
        {
            gb.PlayAct(new ActMoveToLocal(1, new Vector3(2, 2, 2)));
        }

    }
}
