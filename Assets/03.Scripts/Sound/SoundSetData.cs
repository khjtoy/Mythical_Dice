using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SoundSet")]
public class SoundSetData : ScriptableObject
{
	[Header("�� ȿ���� Ŭ��")]
	public AudioClip[] EnemyEffectClip;

	[Header("����� Ŭ��")]
	public AudioClip BackgroundClips;
}
