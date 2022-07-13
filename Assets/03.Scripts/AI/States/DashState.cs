using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : AIState
{
    [field: SerializeField]
    public override List<AITransition> transitions { get; set; }
    [field: SerializeField]
    public override bool IsLoop { get; set; }

    [SerializeField]
    private MinoDash move;

    private Vector2 dir;


    public override void DoAction()
    {
        dir = Define.Controller.transform.localPosition - move.transform.localPosition;
        move.CharacterMovement(dir.normalized);
    }
}
