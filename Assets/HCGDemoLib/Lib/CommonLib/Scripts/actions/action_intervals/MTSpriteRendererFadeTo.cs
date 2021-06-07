using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MTUnity.Actions
{
	public class MTSpriteRendererFadeTo : MTFiniteTimeAction
	{
		public float ToOpacity { get; private set; }
        SpriteRenderer[] sp;

		#region Constructors

        public static MTSpriteRendererFadeTo Create(float duration,float opacity,params SpriteRenderer[] target)
        {
            return new MTSpriteRendererFadeTo(duration, opacity, target);
        }

		public MTSpriteRendererFadeTo (float duration, float opacity,params SpriteRenderer[] target) : base (duration)
		{
			ToOpacity = opacity;
            if (target.Length == 0)
            {
                Debug.LogError("At least one text!");
            }
            sp = target;
		}

		#endregion Constructors

		protected internal override MTActionState StartAction(GameObject target)
		{
			return new MTSpriteRendererFadeToState (this, target,sp);
		}

		public override MTFiniteTimeAction Reverse()
		{
			throw new NotImplementedException();
		}
	}

    public class MTSpriteRendererFadeToState : MTFiniteTimeActionState
    {
        protected List<float> FromOpacity = new List<float>();
        protected float ToOpacity { get; set; }
        SpriteRenderer[] _sps;

        TextMesh _textMesh;

        public MTSpriteRendererFadeToState(MTSpriteRendererFadeTo action, GameObject target, params SpriteRenderer[] texts)
            : base(action, target)
        {
            if (action != null)
            {
                ToOpacity = action.ToOpacity;
            }
            _sps = texts;

            if (target != null)
            {
                if (_sps != null)
                {
                    for (int i = 0; i < _sps.Length; i++)
                    {
                        FromOpacity.Add(_sps[i].color.a);
                    }
                }

            }
        }

        public override void Update(float time)
        {
            if (_sps != null)
            {
                for (int i = 0; i < _sps.Length; i++)
                {
                    var sp = _sps[i];
                    if (sp != null)
                    {
                        Color newColor = sp.color;
                        newColor.a = FromOpacity[i] + (ToOpacity - FromOpacity[i]) * time;
                        sp.color = newColor;
                    }

                }
            }
        }
    }

}