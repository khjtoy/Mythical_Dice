using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioMixer _mixer = null;
    public void SetAudioVolume(string key, float value)
    {
        _mixer.SetFloat(key, Mathf.Log10(value) * 20);
    }
}
