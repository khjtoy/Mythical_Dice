using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private static Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public static void PlayAnimator(string key)
    {
        animator.Play(key);
    }
}
