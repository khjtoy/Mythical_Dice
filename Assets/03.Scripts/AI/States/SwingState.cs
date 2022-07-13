using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingState : AIState
{
    [field: SerializeField]
    public override List<AITransition> transitions { get; set; } = new List<AITransition> ();
    [field: SerializeField]
    public override bool IsLoop { get; set; }

    [SerializeField]
    private MinoSwing move = null;

    public override void DoAction()
    {
        move.CharacterMovement(Vector2.zero);
    }
}
