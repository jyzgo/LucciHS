using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ActLib
{
    [DefaultExecutionOrder(-9999)]
    public class ActMgr : Singleton<ActMgr>
    {
        //public GameObject Sp;
        private void Awake()
        {
            //    Debug.Log("Hihi");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        IEnumerator LoopDebug()
        {
            while(true)
            {
                yield return new WaitForSeconds(4);
                DebugLog();
            }
        }

        internal void RemoveAct(BaseAct baseAction)
        {
            int index = baseAction.InstanceID;
            if (!_dict.ContainsKey(index)) return;
            _dict[index].Remove(baseAction);
            if(_dict[index].Count == 0)
            {
                _dict.Remove(index);
            }
        }

        public void DebugLog()
        {
            Debug.Log("ActMgr dict count" + _dict.Count);
            
        }

        //private void Start()
        //{
        //    var delay = new ActionDelay(Sp, 1f);
        //    var move = new ActionMoveToLocal(Sp, 3f, new Vector3(22, 33, 44));
        //    var call = new ActionCallFunc(() =>
        //    {
        //        Debug.Log("Done");
        //    });
        //    var seq = new ActionSeqences(delay, move, call);

        //    StartCoroutine(seq.IAction());

        //}
        Dictionary<int, HashSet<BaseAct>> _dict = new Dictionary<int, HashSet<BaseAct>>();

        public void OnDestroy()
        {
            RemoveAllActs();
        }

        private void RemoveAllActs()
        {
            StopAllCoroutines();
            foreach(var p in _dict)
            {
                foreach (var v in p.Value)
                {
                    v.StopPlay();
                }
            }
            _dict.Clear();
        }

        public void AddAct(BaseAct act,GameObject tar)
        {
            if (tar == null) return;
            act.SetGameObject(tar);
            int instanceID = act.InstanceID;
            if(!_dict.ContainsKey(instanceID))
            {
                _dict.Add(instanceID, new HashSet<BaseAct>());
            }
            _dict[instanceID].Add(act);
            act.StartPlay();
        }

    

        public void ClearActs(GameObject tar)
        {
            if (tar == null) return;
            _dict.Remove(tar.GetInstanceID());
        }

        public void StopPlayAllActs(GameObject tar)
        {
            if (tar == null) return;
            int index = tar.GetInstanceID();
            if (!_dict.ContainsKey(index)) return;
            var set = _dict[index];
            foreach(var s in set)
            {
                s.StopPlay();
            }
            _dict.Remove(index);
        }


        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            RemoveAllActs();
        }

    }

}












