using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBinit : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        print("fb start init");
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
            print("start init sdk");
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
            print("fb active finished");
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            print("fb active finished");
            //FB.LogAppEvent(Application.identifier +" Init ");
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
}
