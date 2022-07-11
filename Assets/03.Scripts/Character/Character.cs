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

    public Rigidbody Rigidbody
    {
        get
        {
            return rigidbody;
        }
    }

    protected virtual void Start()
    { 
        rigidbody = GetComponent<Rigidbody>();
    }
}
