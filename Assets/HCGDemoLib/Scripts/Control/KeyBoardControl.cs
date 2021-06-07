using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HCGDemo
{
    [DisallowMultipleComponent]
    public class KeyBoardControl : MonoBehaviour
    {
        public UnityEvent keyCode1;
        public UnityEvent keyCode2;
        public UnityEvent keyCode3;
        public UnityEvent keyCode4;
        public UnityEvent keyCode5;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                keyCode1.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                keyCode2.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                keyCode2.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                keyCode3.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                keyCode4.Invoke();
            }

        }
        public void DebugTest()
        {
            Debug.Log("Testtt");
        }
    }
}
