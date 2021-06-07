using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPercentage : MonoBehaviour
{
    [SerializeField]
    Transform[] routes;
    int routeToGo;

    [Range(1, 100)]
    [SerializeField]
    public float percent;
    float tParam;
    Vector2 catPosition;
    float speedModifier;
    bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.5f;
        coroutineAllowed = true;
        int routeNumber = 0;
        p0 = routes[routeNumber].GetChild(0).position;
        p1 = routes[routeNumber].GetChild(1).position;
        p2 = routes[routeNumber].GetChild(2).position;
        p3 = routes[routeNumber].GetChild(3).position;


    }
    Vector2 p0;
    Vector2 p1;
    Vector2 p2;
    Vector2 p3;

    // Update is called once per frame
    void Update()
    {
        tParam = percent / 100f;
        //tParam += Time.deltaTime * speedModifier;
        catPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;
        transform.position = catPosition;


    }

    IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        while (tParam < 1)
        {
                 yield return new WaitForEndOfFrame();
        }

        tParam = 0;
        routeToGo += 1;
        if (routeToGo > routes.Length - 1)
            routeToGo = 0;
        coroutineAllowed = true;

    }
}
