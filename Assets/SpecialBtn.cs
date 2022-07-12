using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBtn : MonoBehaviour
{
    [SerializeField]
    private Button _changeViewBtn = null;
    [SerializeField]
    private Sprite OnImage = null;
    [SerializeField]
    private Sprite OffImage = null;

    private bool isOn = false;

    private void Awake()
    {
        _changeViewBtn.onClick.AddListener(() =>
        {
            if(isOn)
            {
                _changeViewBtn.image.sprite = OffImage;
                isOn = false;
                Define.MainCam.orthographic = true;
            }
            else
            {
                _changeViewBtn.image.sprite = OnImage;
                isOn = true;
                Define.MainCam.orthographic = false;
            }
        });
    }
}
