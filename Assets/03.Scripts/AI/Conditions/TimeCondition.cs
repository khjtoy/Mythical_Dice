using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCondition : AICondition
{
    float time = 0;
    [SerializeField]
    private float GoalTime = 0;
    public override bool Result()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            time = GoalTime;
            return true;
        }
        return false;
    }
    private void Awake()
    {
        time = GoalTime;
    }
}
