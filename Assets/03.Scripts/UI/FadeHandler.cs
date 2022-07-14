using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        rectTransform.DOAnchorPos3DY(0, 0.1f);
    }

	private void OnDestroy()
	{
        EventManager.StopListening("KILLENEMY", ShowFade);
	}
}
