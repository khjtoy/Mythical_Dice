using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
	[SerializeField]
	private AudioMixerGroup pitchBendGroup;

	#region Ŭ��
	[Header("�÷��̾� ȿ���� Ŭ��")]
	public AudioClip[] PlayerEffectClip;
	[Header("�� ȿ���� Ŭ��")]
	public AudioClip[] EnemyEffectClip;

	[Header("����� Ŭ��")]
	public AudioClip[] BackgroundClips;
	[Header("ȿ���� Ŭ��")]
	public AudioClip[] EffectClips;
	#endregion

	#region ����� �ҽ�
	[Header("����� ����� �ҽ�")]
	[SerializeField]
	private AudioSource BackgroundSource;

	[Header("ȿ���� ����� �ҽ�")]
	[SerializeField]
	private AudioSource EffectSource;

	[Header("�÷��̾� ����� �ҽ�")]
	[SerializeField]
	private AudioSource PlayerEffectSource;

	[Header("�� ����� �ҽ�")]
	[SerializeField]
	private AudioSource EnemyEffectSource;

	[Header("�÷��̾� �뽬 ����� �ҽ�")]
	[SerializeField]
	private AudioSource PlayerDashEffectSource;

	[Header("�÷��̾� ���� �ҽ�")]
	[SerializeField]
	private AudioSource PlayerAttackEffectSource;
	#endregion

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
	public void SetBackgroundSpeed(float speed)
	{
		BackgroundSource.outputAudioMixerGroup = pitchBendGroup;
		BackgroundSource.pitch = speed;
		pitchBendGroup.audioMixer.SetFloat("Pitch Shifter", speed);
	}
	public void SetEffectClip(int index)
	{
		EffectSource.Stop();
		EffectSource.clip = EffectClips[index];
		EffectSource.Play();
	}

	public void SetPlayerAttackEffectClip(int index)
	{
		PlayerAttackEffectSource.Stop();
		PlayerAttackEffectSource.clip = PlayerEffectClip[index];
		PlayerAttackEffectSource.Play();
	}
	public void SetPlayerDashEffectClip(int index)
	{
		PlayerDashEffectSource.Stop();
		PlayerDashEffectSource.clip = PlayerEffectClip[index];
		PlayerDashEffectSource.Play();
	}
	public void SetEnemyEffectClip(int index)
	{
		EnemyEffectSource.Stop();
		EnemyEffectSource.clip = EnemyEffectClip[index];
		EnemyEffectSource.Play();
	}
}
