using UnityEngine;
using System.Collections;
using HCGDemo;

public class CameraTrack : MonoBehaviour
{
    public Transform target;
    Vector3 offset;
    // Use this for initialization
    void Awake()
    {
        target = GameObject.FindObjectOfType<MoveTarget>().transform;
        offset = transform.position - target.position;
    }


    void Start()
    {
        //设置相对偏移
        offset = target.position - this.transform.position;
    }

    void Update()
    {
        this.transform.position = target.position - offset;
    }
}
