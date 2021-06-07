using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActLib
{
    public abstract class BaseAct
    {
        protected GameObject _target;
        protected int _InstanceID;
        public int InstanceID { get { return _InstanceID; } }

        protected float _duration;
        public float Duration { get { return _duration; } }

        protected IEnumerator _currentIEnume;

        public Action DoneEvents;
        public abstract IEnumerator IAct();
        public void Done()
        {
            isDone = true;
            ActMgr.Instance.RemoveAct(this);
            DoneEvents?.Invoke();
        }
        public bool isDone = false;
        public virtual void SetGameObject(GameObject tar)
        {
            _target = tar;
            _InstanceID = _target.GetInstanceID();
        }

        public virtual void StartPlay()
        {
            _currentIEnume = IAct();
            ActMgr.Instance.StartCoroutine(_currentIEnume);

        }

        public virtual void StopPlay()
        {
            if(_currentIEnume !=null)
            {
                var instance = ActMgr.Instance;
                if (instance != null)
                {
                    ActMgr.Instance.StopCoroutine(_currentIEnume);
                }
            }
        }
    }
}
