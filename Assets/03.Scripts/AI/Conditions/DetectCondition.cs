using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCondition : AICondition
{
    [SerializeField]
    private Transform _basePos = null;
    public bool a;
    public override bool Result()
    {
        a = false;
        if(Mathf.RoundToInt(Define.Controller.transform.localPosition.x) == Mathf.RoundToInt(_basePos.transform.localPosition.x))
            a = true;
        if(Mathf.RoundToInt(Define.Controller.transform.localPosition.y) == Mathf.RoundToInt(_basePos.transform.localPosition.y))
            a = true;
        return a;
    }
}
