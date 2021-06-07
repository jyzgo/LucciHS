using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HCGSignalReciver : MonoBehaviour
{
    bool isStart = false;
    public void OnSignalFire(string s)
    {
        isStart = !isStart;
    }

}
