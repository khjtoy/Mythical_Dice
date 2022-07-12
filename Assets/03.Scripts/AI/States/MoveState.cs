using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : AIState
{
    [field:SerializeField]
    public override List<AITransition> transitions { get; set; }
    [field: SerializeField]
    public override bool IsLoop { get; set; }

    [SerializeField]
    private StatueMove move;

    public override void DoAction()
    {
        move.CharacterMovement(Define.Player);  
    }
}
