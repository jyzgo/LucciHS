using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ActLib {
    public static class ActMgrExtension
    {

        public static void PlayAct(this Component gb, BaseAct action)
        {
            gb.gameObject.PlayAct(action);
        }


        public static void PlayActs(this Component gb, params BaseAct[] actions)
        {
            gb.gameObject.PlayActs(actions);
        }

        public static void StopPlayAllActs(this Component gb)
        {
            gb.gameObject.StopPlayAllActs();
        }

       

        public static void PlayAct(this GameObject gb, BaseAct action)
        {
            ActMgr.Instance.AddAct(action, gb);
        }


        public static void PlayActs(this GameObject gb, params BaseAct[] actions)
        {
            if(actions.Length > 1)
            {
                var seq = new ActSeqences(actions);
                ActMgr.Instance.AddAct(seq, gb);
            }else
            {
                ActMgr.Instance.AddAct(actions[0], gb);
            }
        }


        public static void StopPlayAllActs(this GameObject gb)
        {
            ActMgr.Instance.StopPlayAllActs(gb);
        }
    }
}
