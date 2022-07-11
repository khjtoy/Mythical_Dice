using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCondition : AICondition
{
    public override bool Result()
    {
        return Input.GetKey(KeyCode.F);
    }
}
