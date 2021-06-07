using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCGDemo
{
    [DisallowMultipleComponent]
    public class MoveAhead : MonoBehaviour,BaseNotification
    {

        public float movementSpeed = 0.1f;
        bool move = true;

        public void OnNotify(string str)
        {
            if(str.Equals("die"))
            {
                move = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (move)
            {
                transform.position += transform.forward * Time.deltaTime * movementSpeed;
            }
        }
    }
}
