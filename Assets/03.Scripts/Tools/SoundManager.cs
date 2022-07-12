using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField]
    private AudioSource BackgroundSource;
    [SerializeField]
    private AudioSource EffectSource;

    [SerializeField]
    private AudioClip[] BackgroundClips;
    [SerializeField]
    private AudioClip[] EffectClips;

	private void Start()
	{
        SetBackgroundClip(0);
	}

    public void SetBackgroundClip(int index)
	{
        BackgroundSource.Stop();
        BackgroundSource.clip = BackgroundClips[index];
        BackgroundSource.Play();
	}

    public void SetEffectClip(int index)
    {
        EffectSource.Stop();
        EffectSource.clip = EffectClips[index];
        EffectSource.Play();
    }
}
