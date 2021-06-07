using ActLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
        gameObject.PlayActs(new ActMoveToLocal(1, Vector3.zero),
                                new ActCallFunc(()=> { Debug.Log("mid"); } ),
                                new ActDelay(0.2f),
                                new ActCallFunc(() =>{
                                    ActMgr.Instance.DebugLog();
                                }),

                                new ActJoint(
                                new ActRotateTo(1,new Vector3(0,0,180f)),
                                new ActScaleTo(1, new Vector3(2, 2, 2))
                                ),
                                      new ActCallFunc(() =>
                                      {
                                          Debug.Log("Done");
                                          ActMgr.Instance.DebugLog();
                                      })


                                );
    }

    // Update is called once per frame
    void Update()
    {

    }
}
