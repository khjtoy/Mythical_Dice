using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SoundSet")]
public class SoundSetData : ScriptableObject
{
	[Header("적 효과음 클립")]
	public AudioClip[] EnemyEffectClip;

	[Header("배경음 클립")]
	public AudioClip BackgroundClips;
}
