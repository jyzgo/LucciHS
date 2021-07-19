using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ShifterDemo : MonoBehaviour
{
    public static ShifterDemo current;
    private void Awake()
    {
        current = this;
    }

    public Canvas ui;
    public Button buttonA;
    public Button buttonB;
    public GameObject cheatBtn;
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
        AdsMgr.current.ShowInter();
        ui.gameObject.SetActive(true);
        buttonA.gameObject.SetActive(false);
        buttonB.gameObject.SetActive(false);
        cheatBtn.SetActive(true);

    }

    public void RewardCheat()
    {
        AnalyzeMgr.current.OnRewardBeforeShow(AdsFrom.Max);
        print("reward cheat");
        AdsMgr.current.ShowRewardAds(CallBack);
    }

    void CallBack()
    {
        AnalyzeMgr.current.OnRewardFinished(AdsFrom.Max);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
