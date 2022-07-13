using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimationAction : StateMachineBehaviour
{
    private Material spriteMaterial;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(spriteMaterial == null)
            spriteMaterial = animator.transform.GetComponent<SpriteRenderer>().material;

        spriteMaterial.EnableKeyword("_SordColor");
        spriteMaterial.SetFloat("_SordColor", 1f);
        spriteMaterial.DisableKeyword("_SordColor");
    }
}
