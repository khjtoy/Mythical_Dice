using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터의 공통적인 특성을 추출
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
<<<<<<< HEAD
    [Header("스탯")]
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
    [Header("공통 컴포넌트")]
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
