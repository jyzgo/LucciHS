using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActLib
{
    public class ActJoint : BaseAct
    {
        
        BaseAct[] _actions;
        int doneCount = 0;
        public ActJoint(params BaseAct[] actions )
        {
            _actions = actions;
            doneCount = _actions.Length;
        }

        public override IEnumerator IAct()
        {
            foreach(var a in _actions)
            {
                var e = a.IAct();
                _set.Add(e);
                a.DoneEvents += DoneFinishCount;
                ActMgr.Instance.StartCoroutine(e);
            }
            yield return new WaitUntil(DoneCount);
            Done();
        }
        bool DoneCount()
        {
            return doneCount == 0;
        }


        public void DoneFinishCount()
        {
            doneCount--;
        }
        HashSet<IEnumerator> _set = new HashSet<IEnumerator>();

        public override void StopPlay()
        {
            base.StopPlay();

            foreach (var e in _set)
            {
                ActMgr.Instance.StopCoroutine(e);
            }
        }


        public override void SetGameObject(GameObject tar)
        {
            base.SetGameObject(tar);
            foreach (var act in _actions)
            {
                act.SetGameObject(tar);
            }
        }

    }
}
