using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCGDemo
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider))]
    public class PressingMoveTo: MonoBehaviour
    {
        public Transform moveGameObject;
        Vector3 newPosition;
        void Start()
        {
            newPosition = transform.position;
        }
        bool isPressing = false;
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                isPressing = true;
            }else if(Input.GetMouseButtonUp(0))
            {
                isPressing = false;
            }
            if (isPressing) { 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hits = Physics.RaycastAll(ray);
                if(hits.Length > 0)
                {
                    foreach(var h in hits)
                    {
                        if(h.collider.gameObject == gameObject)
                        {
                            newPosition = h.point;
                            newPosition = new Vector3(newPosition.x, transform.position.y, newPosition.z);
                            moveGameObject.position = newPosition;
                            break;
                        }
                    }
                }
            }
        }
    }
}
