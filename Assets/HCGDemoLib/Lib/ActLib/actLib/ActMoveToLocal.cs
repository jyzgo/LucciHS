using System.Collections;
using UnityEngine;

namespace ActLib
{
    public class ActMoveToLocal : BaseAct
    {
        public ActMoveToLocal(float duration, Vector3 position)
        {
            _duration = duration;
            _endPosition = position;

        }

        Vector3 _endPosition;

        public static ActMoveToLocal create(float duration, Vector3 position)
        {
            return new ActMoveToLocal(duration, position);
        }

        public override IEnumerator IAct()
        {
            float elapsedTime = 0;
            Vector3 startingPos = _target.transform.localPosition;
            var trans = _target.transform;
            while (elapsedTime < _duration)
            {
                trans.localPosition = Vector3.Lerp(startingPos, _endPosition, (elapsedTime / _duration));
                elapsedTime += Time.deltaTime;
                yield return null;// new WaitForEndOfFrame();
            }
            trans.localPosition = _endPosition;
            Done();
        }
    }

}