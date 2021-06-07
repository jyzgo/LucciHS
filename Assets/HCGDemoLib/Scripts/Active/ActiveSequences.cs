using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCGDemo
{
    
    [Serializable]
    public struct ActiveGameObject
    {
        public GameObject curGameObject;
        public bool gameObjectActive;
    }

    public class ActiveSequences : BaseActive
    {
        public bool gbActiveStart = false;
        public ActiveGameObject[] activeGameObjectArray;
        int index = 0;

        public override void HCGStartDo()
        {
            var actGB = activeGameObjectArray[index];
            var curBool = !actGB.gameObjectActive;
            actGB.gameObjectActive = curBool;
            actGB.curGameObject.SetActive(curBool);
            index++;
            if(index >= activeGameObjectArray.Length)
            {
                index = 0;
            }
        }
    }
}
