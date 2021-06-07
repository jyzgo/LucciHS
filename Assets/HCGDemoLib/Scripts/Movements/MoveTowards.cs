using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCGDemo
{
    public class MoveTowards : MonoBehaviour,BaseNotification
    {
        public int targetIndex = 0;
        MoveTarget _tar;
        public float speed = 0.1f;
        private void Awake()
        {
            MoveTarget[] targets = GameObject.FindObjectsOfType<MoveTarget>();
            foreach (var t in targets)
            {
                if (t.TagetIndex == targetIndex)
                {
                    _tar = t;
                    break;
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (!move) return;
            if (_tar == null) return;
            transform.LookAt(_tar.transform);
            float step = speed * Time.deltaTime * speed; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, _tar.transform.position, step);

        }

        bool move =true;
        public void OnNotify(string str)
        {
            if (str.Equals("die")) {
                move = false;
            }
        }
    }
}
