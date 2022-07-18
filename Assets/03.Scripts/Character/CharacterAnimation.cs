using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public static void PlayAnimator(Animator animator, string key)
    {
        animator.Play(key);
    }

    public static void SetTriggerAnimator(Animator animator, string key)
    {
        animator.SetTrigger(key);
    }
}
