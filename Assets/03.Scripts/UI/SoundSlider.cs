using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private string key;
    public void SetVolume(float value)
    {
        AudioManager.Instance.SetAudioVolume(key, value);
    }
}
