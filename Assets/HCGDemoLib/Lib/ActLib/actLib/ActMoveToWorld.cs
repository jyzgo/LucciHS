using System.Collections;
using UnityEngine;

namespace ActLib
{
    public class ActMoveToWorld : BaseAct
    {
        public ActMoveToWorld(float duration, Vector3 position)
        {
            _duration = duration;
            _endPosition = position;

        }

        Vector3 _endPosition;

        public override IEnumerator IAct()
        {
            float elapsedTime = 0;
            Vector3 startingPos = _target.transform.position;
            var trans = _target.transform;
            while (elapsedTime < _duration)
            {
                trans.position = Vector3.Lerp(startingPos, _endPosition, (elapsedTime / _duration));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            trans.position = _endPosition;
            Done();
        }
    }

}