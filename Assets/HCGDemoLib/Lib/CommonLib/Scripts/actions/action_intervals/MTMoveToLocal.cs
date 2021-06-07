using UnityEngine;

namespace MTUnity.Actions
{
    public class MTMoveToLocal : MTMoveBy
    {
        protected Vector3 EndPosition;

        #region Constructors

		public MTMoveToLocal (float duration, Vector3 position) : base (duration, position)
        {
            EndPosition = position;
        }

        #endregion Constructors

        public Vector3 PositionEnd {
            get { return EndPosition; }
        }

        protected internal override MTActionState StartAction(GameObject target)
        {
			return new MTMoveToLocalState (this, target);

        }
    }

    public class MTMoveToLocalState : MTMoveByState
	{

		public MTMoveToLocalState (MTMoveToLocal action, GameObject target)
            : base (action, target)
        { 
			if(target == null)
			{
				return;
			}
            StartPosition = target.transform.localPosition;
            EndPosition = action.PositionEnd;
            //PositionDelta = action.PositionEnd - target.transform.localPosition;
        }

        public override void Update (float time)
        {
            if (Target != null)
            {
                Vector3 newPos = Vector3.Lerp(StartPosition, EndPosition, time / 1);// StartPosition + PositionDelta * time;
				PreviousPosition = newPos;
                Target.transform.localPosition = newPos;
            }
        }
    }

}