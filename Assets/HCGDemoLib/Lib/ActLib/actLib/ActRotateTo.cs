using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActLib
{
    public class ActRotateTo : BaseAct
    {
       
        public ActRotateTo(float duration, Vector3 rotation)
        {
            _duration = duration;
            _endRotation = rotation;

        }

        Vector3 _endRotation;

        public static ActRotateTo create(float duration, Vector3 position)
        {
            return new ActRotateTo(duration, position);
        }

        public override IEnumerator IAct()
        {
            float elapsedTime = 0;
            Vector3 startingPos = _target.transform.rotation.eulerAngles;
            var trans = _target.transform;
            while (elapsedTime < _duration)
            {
                trans.rotation = Quaternion.Euler(Vector3.Lerp(startingPos, _endRotation, (elapsedTime / _duration)));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            trans.rotation= Quaternion.Euler(_endRotation);
            Done();
        }
    }
}
