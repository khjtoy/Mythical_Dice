using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseCondition : AICondition
{
    public bool hasUsed = false;
    public override bool Result()
    {
        return hasUsed;
    }
}
