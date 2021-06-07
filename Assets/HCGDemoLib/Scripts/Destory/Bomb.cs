using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCGDemo
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class Bomb : MonoBehaviour
    {
        public int power = 1;
        public float damageDelay = 0f;
        public bool explodeOnStart = false;
        public float explodeStartDelay = 1f;
        public GameObject explosionPrefab;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(explodeStartDelay);
            Explode();
        }

        HashSet<Enemy> enemySet = new HashSet<Enemy>();
        public void OnNotify(string str)
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            var en = other.GetComponent<Enemy>();
            if (en != null)
            {
                enemySet.Add(en);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            var en = other.GetComponent<Enemy>();
            if (en != null)
            {
                enemySet.Remove(en);
            }
        }

        public void Explode()
        {
            foreach(var en in enemySet)
            {
                en.ReduceHp(power, damageDelay);
            }
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);

        }
    }
}
