using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MTUnity.Actions
{
	public class MTGraphicsFadeTo : MTFiniteTimeAction
	{
		public float ToOpacity { get; private set; }
        Graphic[] texts;

		#region Constructors

        public static MTGraphicsFadeTo Create(float duration,float opacity,params Graphic[] target)
        {
            return new MTGraphicsFadeTo(duration, opacity, target);
        }

		public MTGraphicsFadeTo (float duration, float opacity,params Graphic[] target) : base (duration)
		{
			ToOpacity = opacity;
            if (target.Length == 0)
            {
                Debug.LogError("At least one text!");
            }
            texts = target;
		}

		#endregion Constructors

		protected internal override MTActionState StartAction(GameObject target)
		{
			return new MTGraphicsFadeToState (this, target,texts);
		}

		public override MTFiniteTimeAction Reverse()
		{
			throw new NotImplementedException();
		}
	}

    public class MTGraphicsFadeToState : MTFiniteTimeActionState
    {
        protected List<float> FromOpacity = new List<float>();
        protected float ToOpacity { get; set; }
        Graphic[] _graphics;

        TextMesh _textMesh;

        public MTGraphicsFadeToState(MTGraphicsFadeTo action, GameObject target, params Graphic[] graphics)
            : base(action, target)
        {
            if (action != null)
            {
                ToOpacity = action.ToOpacity;
            }
            _graphics = graphics;

            if (target != null)
            {
                if (_graphics != null)
                {
                    for (int i = 0; i < _graphics.Length; i++)
                    {
                        FromOpacity.Add(_graphics[i].color.a);
                    }
                }

            }
        }

        public override void Update(float time)
        {
            if (_graphics != null)
            {
                for (int i = 0; i < _graphics.Length; i++)
                {
                    var text = _graphics[i];
                    if (text != null)
                    {
                        Color newColor = text.color;
                        newColor.a = FromOpacity[i] + (ToOpacity - FromOpacity[i]) * time;
                        text.color = newColor;
                    }

                }
            }
        }
    }

}