using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAimationAction : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.parent.GetComponent<PlayerDie>().DieScene();
    }
}
