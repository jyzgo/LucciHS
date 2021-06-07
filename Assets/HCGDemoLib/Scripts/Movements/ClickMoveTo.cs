using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCGDemo
{
    public class ClickMoveTo: MonoBehaviour
    {
        public Transform moveGameObject;
        Vector3 newPosition;
        void Start()
        {
            newPosition = transform.position;
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
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
