using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class FadeHandler : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        EventManager.StartListening("KILLENEMY", ShowFade);
    }

    private void ShowFade(EventParam eventParam)
    {
        Invoke("DoPos", 1f);
    }

    private void DoPos()
    {
        rectTransform.DOAnchorPos3DY(-1684.5f, 1f).onComplete += () =>
        {
            SceneManager.LoadScene("Stage");
        };
    }

    public void Fade(Action callBack = null)
    {
        callBack?.Invoke();
        rectTransform.DOAnchorPos3DY(0f, 2f);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("KILLENEMY", ShowFade);
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("KILLENEMY", ShowFade);
    }
}
