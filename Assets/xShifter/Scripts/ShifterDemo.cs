using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ShifterDemo : MonoBehaviour
{

    public Canvas ui;
    public Button buttonA;
    public Button buttonB;
    // Start is called before the first frame update
    void Start()
    {
        buttonA.onClick.AddListener(PressA);
        buttonB.onClick.AddListener(PressB);
        
    }

    public PlayableDirector dirA;
    public PlayableDirector dirB;

    void PressA()
    {
        double t = dirA.playableAsset.duration;
        dirA.Play();
        StartCoroutine(DelayBackUI((float)t));

    }
    void PressB()
    {
        double t = dirB.playableAsset.duration;
        dirB.Play();
        StartCoroutine(DelayBackUI((float)t));
    }


    IEnumerator DelayBackUI(float t)
    {
        ui.gameObject.SetActive(false);
        yield return new WaitForSeconds(t);
        ui.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
