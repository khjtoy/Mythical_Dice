using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class SoundManager : MonoSingleton<SoundManager>
{
	[SerializeField]
	private AudioMixerGroup pitchBendGroup;

	protected override void Init()
	{

	}

	#region 클립
	[Header("플레이어 효과음 클립")]
	public AudioClip[] PlayerEffectClip;
	[Header("적 효과음 클립")]
	public AudioClip[] EnemyEffectClip;

	[Header("배경음 클립")]
	public AudioClip[] BackgroundClips;
	[Header("효과음 클립")]
	public AudioClip[] EffectClips;
	#endregion

	#region 오디오 소스
	[Header("배경음 오디오 소스")]
	[SerializeField]
	private AudioSource BackgroundSource;

	[Header("효과음 오디오 소스")]
	[SerializeField]
	private AudioSource EffectSource;

	[Header("적 오디오 소스")]
	[SerializeField]
	private AudioSource EnemyEffectSource;

	[Header("플레이어 대쉬 오디오 소스")]
	[SerializeField]
	private AudioSource PlayerDashEffectSource;

	[Header("플레이어 공격 소스")]
	[SerializeField]
	private AudioSource PlayerAttackEffectSource;
	#endregion

	private void Start()
	{
		if (SceneManager.GetActiveScene().name == "Intro")
		{
			SetBackgroundClip((int)BackGroundEnum.INTRO);
		}
		else if (SceneManager.GetActiveScene().name == "Start")
		{
			SetBackgroundClip((int)BackGroundEnum.START);
		}
	}

	public void InitMap()
	{
		if (SceneManager.GetActiveScene().name == "GamePlay 6")
		{
			SetBackgroundClip((int)BackGroundEnum.BASIC);
			EnemyEffectSource = GameObject.FindGameObjectWithTag("ENEMY").GetComponent<AudioSource>();
		    PlayerDashEffectSource = GameObject.Find("PlayerDashEffect").GetComponent<AudioSource>();
			PlayerAttackEffectSource = GameObject.Find("PlayerAttackEffect").GetComponent<AudioSource>();
		}
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
