using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneSkillCondition : AICondition
{
    [SerializeField]
    private EnemyMove _enemyMove = null;
    private IEnemyAttack _enemyAttack = null;

    private void Awake()
    {
        _enemyAttack = _enemyMove.GetComponent<IEnemyAttack>();
    }
    public override bool Result()
    {
        Debug.Log(!_enemyAttack.IsAttacking);
        return !_enemyAttack.IsAttacking;
    }
}
