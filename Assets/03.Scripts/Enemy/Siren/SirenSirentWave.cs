using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenSirentWave : EnemyMove, IEnemyAttack
{
    [field:SerializeField]
    public bool IsAttacking { get; set; }
    public Animator animator { get; set; }
    [field:SerializeField]
    public override bool IsFloating { get; set; }

    public override void CharacterMovement(Vector2 target)
    {

    }

    public void DoAttack()
    {
        
    }
}
