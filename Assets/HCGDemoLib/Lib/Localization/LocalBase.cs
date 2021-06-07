using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LocalBase : MonoBehaviour
{
    public string localizationKey;

    public abstract void UpdateLocale();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        UpdateLocale();
    }

    protected virtual void Awake()
    {
        LocalizationManager.Instance.AddListener(this);
    }


    private void OnDestroy()
    {
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.RemoveListener(this);
        }
        
    }

}
