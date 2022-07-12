using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ĳ������ �������� Ư���� ����
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [Header("���� ������Ʈ")]
    private Rigidbody rigidbody = null;
    private Animator animator = null;
    private SpriteRenderer spriteRenderer = null;

    public Rigidbody Rigidbody
    {
        get
        {
            return rigidbody;
        }
    }

    public Animator Animator
    {
        get
        {
            return animator;
        }
    }

    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return spriteRenderer;
        }
    }

    protected virtual void Start()
    { 
        rigidbody = GetComponent<Rigidbody>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
}
