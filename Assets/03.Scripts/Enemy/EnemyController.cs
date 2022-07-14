using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : Character, OnHit
{
	[Header("HP �����̴�")]
	[SerializeField]
	Image playerHpSlider;
	[SerializeField]
	Image whiteSlider;

	[SerializeField]
	private float sliderSpeed;

	[SerializeField] private AIState _currentState;
	private HPSlider _slider;
	private bool _canMoveNext = true;
	private bool _canDoAgain = true;

	[Header("�� HP")]
	[SerializeField]
	private int originHp = 0;
	[SerializeField]
	private int hp = 0;

	public bool isDamage = false;
	public bool isDeath = false;

	private EventParam eventParam;

	private void Awake()
	{
		_slider = GameObject.Find("BossBar").GetComponent<HPSlider>();
		playerHpSlider = GameObject.Find("Canvas/BossBar/Enemy/Red").GetComponent<Image>();
		whiteSlider = GameObject.Find("Canvas/BossBar/Enemy/White").GetComponent<Image>();
	}

	public void OnHits(int damage)
	{
		if (isDeath) return;
		hp -= damage;
		float hpPer = (float)hp / originHp;
		_slider.UpdateAmount(hpPer);
		GameObject obj = PoolManager.Instance.GetPooledObject((int)DefineCS.PooledObject.NumText);
		obj.SetActive(true);
		obj.GetComponent<NumText>().DamageText(damage, this.transform.position);
		SoundManager.Instance.SetEnemyEffectClip((int)EnemyEffectEnum.Hit);
		isDamage = true;
		if (hp <= 0)
		{
			Animator.SetTrigger("Broken");
			EventManager.TriggerEvent("KILLENEMY", eventParam);
			Define.Controller.hasStart = false;
			isDeath = true;
			Time.timeScale = 0.15f;
			if(PlayerPrefs.GetInt("STAGE")+1>PlayerPrefs.GetInt("CLEAR"))
            {
				PlayerPrefs.SetInt("CLEAR", PlayerPrefs.GetInt("STAGE")+1);
            }
		}
	}

	void UpdateSlider()
	{
		if (isDamage)
		{
			whiteSlider.transform.localScale = Vector3.Lerp(whiteSlider.transform.localScale, playerHpSlider.transform.localScale, Time.deltaTime * sliderSpeed);
			if (playerHpSlider.transform.localScale.x >= whiteSlider.transform.localScale.x - 0.01f)
			{
				isDamage = false;
				whiteSlider.transform.localScale = playerHpSlider.transform.localScale;
			}
		}

	}

	protected virtual void Update()
	{
		if (!GameManager.Instance.StageStart)
			return;
		UpdateSlider();
		if (_canDoAgain)
		{
			_currentState.DoAction();
			if (!_currentState.IsLoop)
				_canDoAgain = false;
		}

		foreach (var transition in _currentState.transitions)
		{
			if (transition.PositiveCondition.Count == 0)
				_canMoveNext = true;
			else
			if (transition.IsPositiveAnd)
			{
				_canMoveNext = true;
				foreach (var conditon in transition.PositiveCondition)
				{
					_canMoveNext &= conditon.Result();
				}
			}
			else
			{
				_canMoveNext = false;
				foreach (var conditon in transition.PositiveCondition)
				{
					_canMoveNext |= conditon.Result();
				}
			}

			if (transition.NegativeCondition.Count == 0)
				_canMoveNext &= true;
			else
			if (transition.IsNegativeAnd)
			{
				_canMoveNext &= true;
				foreach (var conditon in transition.NegativeCondition)
				{
					_canMoveNext &= !conditon.Result();
				}
			}
			else
			{
				_canMoveNext = false;
				foreach (var conditon in transition.NegativeCondition)
				{
					_canMoveNext |= !conditon.Result();
				}
			}

			if (_canMoveNext)
			{
				Debug.Log($"Current State Has Changed To {transition.goalState}");
				_currentState = transition.goalState;
				_canDoAgain = true;
			}
		}
	}
}
