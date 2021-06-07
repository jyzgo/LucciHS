using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MTUnity.Actions
{
    public class MTCurveMoveTo : MTFiniteTimeAction
    {

        public float _duration;
        public Vector3 _fromPos;
        public Vector3 _tagetPos;
        public AnimationCurve _xCurve;
        public AnimationCurve _yCurve;
        public Vector3 _refVec;

        #region Constructors
        public MTCurveMoveTo(float duration,
            AnimationCurve xCurve, 
            AnimationCurve yCurve, 
            Vector3 vec
            ) : base(duration)
        {
            _duration = duration;
            _xCurve = xCurve;
            _yCurve = yCurve;
            _refVec = vec;

        }
        #endregion


        public override MTFiniteTimeAction Reverse()
        {
            return null;
        }

        protected internal override MTActionState StartAction(GameObject target)
        {
            return new MTCurveMoveToState(this,target);
        }
    }

    public class MTCurveMoveToState : MTFiniteTimeActionState
    {
        public float _duration;
        public Vector3 _fromPos;
        public Vector3 _tagetPos;
        public AnimationCurve _xCurve;
        public AnimationCurve _yCurve;
        public Vector3 PositionDelta;
        float _currentTime;
        Vector3 _reference;

        public MTCurveMoveToState(MTCurveMoveTo action, GameObject target) : base(action, target)
        {
            _duration = action._duration;
            _fromPos = target.transform.localPosition;
            _tagetPos = _fromPos;
            _xCurve = action._xCurve;
            _yCurve = action._yCurve;
            _reference = action._refVec;
            _currentTime = 0;
        }

        public override void Update(float time)
        {
            if (Target == null)
            {
                return;
            }
            _currentTime += Time.deltaTime;
            float offset = _currentTime / _duration;
            var x = _fromPos.x + _xCurve.Evaluate(offset) *_reference.x;
            var y = _fromPos.y +  _yCurve.Evaluate(offset) * _reference.y;
            Target.transform.localPosition= new Vector3(x,y,_fromPos.z);

        }
    }
}
