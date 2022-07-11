using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    [field:SerializeField]
    public override List<AITransition> transitions { get; set; }
    [field:SerializeField]
    public override bool IsLoop { get; set; }

    public override void DoAction()
    {
        
    }
}
