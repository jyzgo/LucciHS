using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCGDemo
{
    [Serializable]
    public struct ActiveGameObjectOneByOne
    {
        public GameObject[] curGameObject;
        public bool gameObjectActive;
        public float Delay;
    }

    public class ActiveOneByOne : BaseActive
    {
        // Start is called before the first frame update

        public bool gbActiveStart = false;
        public ActiveGameObjectOneByOne[] activeGameObjectArray;

        public override void HCGStartDo()
        {
            for (int i = 0; i < activeGameObjectArray.Length; i++)
            {
                StartCoroutine(DelayCall(activeGameObjectArray[i]));

            }
        }

        IEnumerator DelayCall(ActiveGameObjectOneByOne delayGameObj)
        {
            yield return new WaitForSeconds(delayGameObj.Delay);
            var objs = delayGameObj.curGameObject;
            delayGameObj.gameObjectActive = !delayGameObj.gameObjectActive;
            foreach (var a in objs)
            {
                a.SetActive(delayGameObj.gameObjectActive);

            }
        }

    }
}
