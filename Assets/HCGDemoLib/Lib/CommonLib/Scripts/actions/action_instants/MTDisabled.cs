using UnityEngine;

namespace MTUnity.Actions
{
    public class MTDisabled: MTActionInstant
    {
        #region Constructors
        public static MTDisabled Create()
        {
            return new MTDisabled();
        }

        public  MTDisabled()
        {
        }

        #endregion Constructors


        protected internal override MTActionState StartAction(GameObject target)
        {
            return new MTDisabledState(this, target);

        }

        public override MTFiniteTimeAction Reverse ()
        {
            return (new MTShow ());
        }

    }

    public class MTDisabledState: MTActionInstantState
    {

        public MTDisabledState(MTDisabled action, GameObject target)
            : base (action, target)
        {
            if (target != null)
            {
                target.SetActive(false);
            }
        }

    }

}
