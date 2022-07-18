using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAttack
{
    public bool IsAttacking { get; set; } 
    public Animator animator { get; set; } 
    public void DoAttack();
}
