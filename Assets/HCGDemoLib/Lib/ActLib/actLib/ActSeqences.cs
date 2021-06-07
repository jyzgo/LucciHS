using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActLib
{
    public class ActSeqences : BaseAct
    {
        BaseAct[] _actions;
        List<IEnumerator> _ieList = new List<IEnumerator>();
        public ActSeqences(params BaseAct[] actions)
        {
            _actions = actions;
            foreach(var d in _actions)
            {
                _duration += d.Duration;
            }
        }
        public override IEnumerator IAct()
        {
            for (int i = 0; i < _actions.Length; i++)
            {
                var action = _actions[i];
                var e = action.IAct();
                _ieList.Add(e);
                yield return e;
            }
            Done();
        }

        public override void SetGameObject(GameObject tar)
        {
            base.SetGameObject(tar);
            foreach(var act in _actions)
            {
                act.SetGameObject(tar);
            }
        }

        public override void StopPlay()
        {
            base.StopPlay();
            if(ActMgr.Instance == null)
            {
                return;
            }
            foreach(var e in _ieList)
            {
                if (e == null)
                {
                    //Debug.LogError("tar " + _target.name);
                }
                else
                {
                    ActMgr.Instance.StopCoroutine(e);
                }
            }
        }


    }


}