using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCGDemo
{
    public class ActiveSwitcher : BaseActive
    {
        public bool gbActive = false;
        public GameObject[] gameObjects;
        private void Awake()
        {
            UpdateGameObjects();
        }

        public override void HCGStartDo()
        {
            gbActive = !gbActive;
            UpdateGameObjects();
        }    

        void UpdateGameObjects()
        {
            foreach(var gb in gameObjects)
            {
                gb.SetActive(gbActive);
            }

        }

    }
}
