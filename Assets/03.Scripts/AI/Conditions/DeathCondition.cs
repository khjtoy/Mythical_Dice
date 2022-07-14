using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCondition : AICondition
{
    [SerializeField]
    EnemyController controller;
    public override bool Result()
    {
        return controller.isDeath;
    }
}
