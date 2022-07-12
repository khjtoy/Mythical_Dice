using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character, OnHit
{
    [SerializeField] private AIState _currentState;
    private HPSlider _slider;
    private bool _canMoveNext = false;
    private bool _canDoAgain = true;

    [Header("적 HP")]
    [SerializeField]
    private int originHp = 0;
    [SerializeField]
    private int hp = 0;

    private void Awake()
    {
        _slider = GameObject.Find("BossBar").GetComponent<HPSlider>();
    }

    public void OnHits(int damage)
	{
        hp -= damage;
        float hpPer = (float)hp / originHp;
        _slider.amount = hpPer;
		SoundManager.Instance.SetPlayerEffectClip((int)PlayerEffectEnm.ATTACK);
		if (hp <= 0)
        {
            //종료씬으로
        }
    }
    protected virtual void Update()
    {
        if (_canDoAgain)
        {
            _currentState.DoAction();
            if (!_currentState.IsLoop)
                _canDoAgain = false;
        }

        foreach (var transition in _currentState.transitions)
        {
            if (transition.PositiveCondition.Count == 0)
                _canMoveNext &= true;
            else
            if (transition.IsPositiveAnd)
            {
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
