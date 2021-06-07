using System;

using UnityEngine;

namespace MTUnity.Actions
{
    public class MTRotateTo : MTFiniteTimeAction
    {

        #region Constructors
	
		public new float Duration{get;private set;}
        public Vector3 TargetAngle { get; private set; }
        //public Quaternion TargetQuaternion { get; private set; }

		public MTRotateTo(float duration,Vector3 toAngle):base(duration)
		{
			Duration = duration;
            TargetAngle = toAngle;

			//TargetQuaternion =Quaternion.Euler( toAngle);
		}

        public MTRotateTo(float duration,Quaternion toQuater):base(duration)
        {
            Duration = duration;
            TargetAngle = toQuater.eulerAngles;
        }

        #endregion Constructors

        protected internal override MTActionState StartAction(GameObject target)
        {
            return new MTRotateToState (this, target);
        }

        public override MTFiniteTimeAction Reverse()
        {
            throw new NotImplementedException();
        }
    }


    public class MTRotateToState : MTFiniteTimeActionState
    {


        public MTRotateToState (MTRotateTo action, GameObject target)
            : base (action, target)
        { 
			if(Target == null)
			{
				return;
			}
			FromAngle = Target.transform.localRotation.eulerAngles;
            ToAngle = action.TargetAngle;// Quaternion.Euler( action.TargetAngle);
			InTime = action.Duration;
		}

		Vector3 FromAngle;
		Vector3 ToAngle;
		float InTime;
		float curTime = 0f;

        public override void Update (float time)
        {
			if(Target != null ){
				Target.transform.localRotation = Quaternion.Euler( Vector3.Lerp(FromAngle,ToAngle,curTime/InTime));
			}

			curTime += Time.deltaTime;

        }

    }
}