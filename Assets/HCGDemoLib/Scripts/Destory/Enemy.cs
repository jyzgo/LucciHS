using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCGDemo
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider),typeof(Rigidbody))]
    public class Enemy : MonoBehaviour, BaseNotification
    {
        //private void OnTriggerEnter(Collider other)
        //{
        //    var bu = other.GetComponent<Bullets>();
        //    if (bu != null)
        //    {
        //        hp -= bu.power;
        //        CheckHp();
        //    }

        //}

        void CheckHp()
        {
            if (hp < 0)
            {
                var baseNotifi = GetComponents<BaseNotification>();
                foreach(var no in baseNotifi)
                {
                    no.OnNotify("die");
                }
            }
        }

        public void OnNotify(string str)
        {
            if(str.Equals("die"))
            {
                Destroy(gameObject);
            }
        }

        public int hp = 10;
        public void ReduceHp(int reduceHp,float delay= 0f)
        {
            StartCoroutine(DelayReduce(reduceHp, delay));
        }
        IEnumerator DelayReduce(int reduceHp,float delay)
        {
            yield return new WaitForSeconds(delay);
            hp -= reduceHp;
            CheckHp();
        }
        


    }
}
