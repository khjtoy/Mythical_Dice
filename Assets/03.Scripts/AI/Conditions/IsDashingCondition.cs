using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDashingCondition : AICondition
{
    [SerializeField] private MinoMove _mino = null;
    public override bool Result()
    {
        return _mino.IsDashing;
    }
}
