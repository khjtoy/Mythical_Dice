using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    public abstract List<AITransition> transitions { get; set; }
    public abstract void DoAction();
    public abstract bool IsLoop { get; set; }
}
