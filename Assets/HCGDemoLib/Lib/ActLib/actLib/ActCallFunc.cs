
using System;
using System.Collections;
using UnityEngine;

namespace ActLib
{
    public class ActCallFunc : BaseAct
    {
        Action _selector;
        public ActCallFunc(Action selector)
        {
            _selector = selector;
        }
        public override IEnumerator IAct()
        {
            _selector();
            Done();
            return null;
        }
    }
}