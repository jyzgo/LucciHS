using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCGDemo
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class Bullets : MonoBehaviour, BaseNotification
    {
        public int power = 1;
        public float delay = 0f;
        public GameObject impactPrefab;
        public GameObject explosionPrefab;
        

        public void OnNotify(string str)
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            var en = other.GetComponent<Enemy>();
            if(en != null)
            {
                en.ReduceHp(power, delay);
                if (impactPrefab != null)
                {
                    Instantiate(impactPrefab, transform.position,Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }
}
