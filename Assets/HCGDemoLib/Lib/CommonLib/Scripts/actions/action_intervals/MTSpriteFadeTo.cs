using System;
using UnityEngine;

namespace MTUnity.Actions
{
    public class MTSpriteFadeTo : MTFiniteTimeAction
    {
        public float ToOpacity   { get; private set; }
        public float FromOpacity { get; private set; }
        public SpriteRenderer TargetSpriteRenderer { get; private set; }

        public MTSpriteFadeTo(SpriteRenderer sp, float duration, float opacity) : base(duration)
        {
            ToOpacity = opacity;
            FromOpacity = sp.color.a;
            TargetSpriteRenderer = sp;
        }

        protected internal override MTActionState StartAction(GameObject target)
        {
            return new MTSpriteFadeToState(this, target, FromOpacity,ToOpacity);

        }

        public override MTFiniteTimeAction Reverse()
        {
            throw new NotImplementedException();
        }

    }

    public class MTSpriteFadeToState : MTFiniteTimeActionState
    {
        protected float FromOpacity { get; set; }

        protected float ToOpacity { get; set; }

        protected SpriteRenderer TargetSpriteRenderer { get; set; }
        public MTSpriteFadeToState(MTSpriteFadeTo action, GameObject target,float fromOpacity ,float toOpacity)
            : base(action, target)
        {
            ToOpacity = toOpacity;
            TargetSpriteRenderer = action.TargetSpriteRenderer;

            FromOpacity = fromOpacity;
            
        }

        public override void Update(float time)
        {
            var oldColor = TargetSpriteRenderer.color;
            TargetSpriteRenderer.color = 
                new Color(oldColor.r,oldColor.g,oldColor.b, FromOpacity + (ToOpacity - FromOpacity) * time );

        }

    }
}

