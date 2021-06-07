using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActLib
{
    public class ActScaleTo : BaseAct
    {
        public ActScaleTo(float duration, Vector3 scale)
        {
            _duration = duration;
            _endScale = scale;

        }
        public ActScaleTo(float duration, float scale)
        {
            _duration = duration;
            _endScale = new Vector3(scale,scale,scale);
        }

        Vector3 _endScale;

        public static ActScaleTo create(float duration, Vector3 position)
        {
            return new ActScaleTo(duration, position);
        }

        public override IEnumerator IAct()
        {
            float elapsedTime = 0;
            Vector3 startScale = _target.transform.localScale;
            var trans = _target.transform;
            while (elapsedTime < _duration)
            {
                trans.localScale= Vector3.Lerp(startScale, _endScale, (elapsedTime / _duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            trans.localScale= _endScale;
            Done();
        }
    }
}
