using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActLib
{
    public class ActClear : BaseAct
    {
        public override IEnumerator IAct()
        {

            yield return null;
            Done();
        }

    }
}
