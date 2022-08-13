using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimState : AIState
{
    [field : SerializeField]
    public override List<AITransition> transitions { get; set; }
    [field: SerializeField]
    public override bool IsLoop { get; set; }

    [SerializeField]
    private SirenSwim _swim = null;
    public override void DoAction()
    {
        _swim.CharacterMovement(Define.Player);
    }
}
