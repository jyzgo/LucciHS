using UnityEngine;

namespace MTUnity.Actions
{
    public class MTEnabled: MTActionInstant
    {
     

        protected internal override MTActionState StartAction(GameObject target)
        {
            return new MTEnabledState(this, target);

        }

        public override MTFiniteTimeAction Reverse ()
        {
            return (new MTShow ());
        }

    }

    public class MTEnabledState: MTActionInstantState
    {

        public MTEnabledState(MTEnabled action, GameObject target)
            : base (action, target)
        {
            if (target != null)
            {
                target.SetActive(true);
            }
        }

    }

}
