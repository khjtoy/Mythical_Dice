using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AIState
{
    [field: SerializeField]
    public override List<AITransition> transitions { get; set; }
    [field: SerializeField]
    public override bool IsLoop { get; set; }

    [SerializeField]
    private SlimeMove move;

    public override void DoAction()
    {
        move.CharacterMovement(Define.Player);
    }
}
