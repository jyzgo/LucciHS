using UnityEngine;

namespace MTUnity.Actions
{
    public class MTDelayTime : MTFiniteTimeAction
    {
        #region Constructors

        public static MTDelayTime Create(float duration)
        {
            return new MTDelayTime(duration);
        }
        public MTDelayTime (float duration) : base (duration)
        {
        }

        #endregion Constructors

        protected internal override MTActionState StartAction(GameObject target)
        {
            return new MTDelayTimeState (this, target);

        }

        public override MTFiniteTimeAction Reverse ()
        {
            return new MTDelayTime (Duration);
        }
    }

    public class MTDelayTimeState : MTFiniteTimeActionState
    {

        public MTDelayTimeState (MTDelayTime action, GameObject target)
            : base (action, target)
        {
        }

        public override void Update (float time)
        {
        }

    }
}