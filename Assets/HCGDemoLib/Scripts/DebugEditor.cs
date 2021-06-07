using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEditor : MonoBehaviour
{
    // Start is called before the first frame update
#if UNITY_EDITOR

    public const string DEBUG_SEPEED = "DEBUG_SPEED";
    private void Awake()
    {
        if(PlayerPrefs.GetInt(DEBUG_SEPEED,0) != 0)
        {
            Time.timeScale = 10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals) && Input.GetKey(KeyCode.LeftControl))
        { 
            if(Time.timeScale > 2f)
            {
                Time.timeScale = 1f;
                PlayerPrefs.SetInt(DEBUG_SEPEED, 0);
            }
            else
            {
                PlayerPrefs.SetInt(DEBUG_SEPEED, 1);
                Time.timeScale = 10f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.LeftControl))
        {
            InitMgr.current.ToWin();
        }



    }
#endif
}
