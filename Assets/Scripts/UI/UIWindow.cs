using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UIWindow : MonoBehaviour
{
    //Animations
    public LeanTweenType inType;
    public LeanTweenType outType;

    //Events
    public UnityEvent OnCloseEvent;

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, Vector3.one,1f).setDelay(0.1f).setEase(inType);
    }

    public void OnClose()
    {
        LeanTween.scale(gameObject, Vector3.zero, 1f).setDelay(0.1f).setEase(outType).setOnComplete(KillWindow);
    }

    void KillWindow()
    {
        OnCloseEvent?.Invoke();
        Destroy(gameObject);
    }
}
