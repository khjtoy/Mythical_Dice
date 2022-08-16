using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildWaveState : AIState
{
    [field:SerializeField]
    public override List<AITransition> transitions { get; set; }
    [field:SerializeField]
    public override bool IsLoop { get; set; }

    [SerializeField]
    private SirenWildWave _wave;

    public override void DoAction()
    {
        _wave.CharacterMovement(Vector2.zero);
    }
}
