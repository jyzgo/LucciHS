using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitStarter : MonoBehaviour
{
    private void Awake()
    {
        InitMgr.current.InitCall();
        int curLevl = InitMgr.current.GetCurrentLevelIndex();
        InitMgr.current.LoadLevel(curLevl);
    }
}
