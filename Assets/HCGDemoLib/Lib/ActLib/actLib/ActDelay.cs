
using System.Collections;
using UnityEngine;

namespace ActLib
{
    public class ActDelay : BaseAct
    {
        public ActDelay(float delay)
        {
            _duration= delay;
        }

        public override IEnumerator IAct()
        {
            yield return new WaitForSeconds(_duration);
            Done();
        }
    }
}
