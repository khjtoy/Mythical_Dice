using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class FadeHandler : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
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
        rectTransform.DOAnchorPos3DY(0, 0.1f).onComplete += () =>
        {
            SceneManager.LoadScene("Stage");
        };
    }

    public void Fade(Action callBack = null)
    {
        rectTransform.DOAnchorPos3DY(0f, 2f).OnComplete(() => callBack?.Invoke());
    }
}
