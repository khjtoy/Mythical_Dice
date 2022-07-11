using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터의 공통적인 특성을 추출
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
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
