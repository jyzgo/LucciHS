using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HCGDemo
{
    public class Generator : MonoBehaviour
    {
        public GameObject prefab;
        public Transform root;
        // Start is called before the first frame update
        public float interval = 3f;

        float curTime = 0;
        private void Update()
        {
            if (prefab != null)
            {
                curTime -= Time.deltaTime;
                if (curTime < 0)
                {
                    curTime = interval;
                    var gb = Instantiate(prefab);
                    gb.transform.SetParent(root);
                    gb.transform.position = transform.position;
                    gb.transform.rotation = transform.rotation;
                }
            }
            
        }

    }
}
