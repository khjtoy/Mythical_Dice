using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ĳ������ �������� Ư���� ����
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
<<<<<<< HEAD
    [Header("����")]
    private int maxCost;
    [SerializeField]
    private int currentCost;
    [SerializeField]
    private int fastCost;
    [SerializeField]
    private int moveCost;
    [SerializeField]
    private int attackCost;
=======
>>>>>>> origin/csh
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
