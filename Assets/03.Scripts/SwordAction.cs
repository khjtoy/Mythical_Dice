using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAction : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        EventManager.StartListening("CHANGESWORD", ChangeSword);
    }

    private void ChangeSword(EventParam eventParam)
    {
        Debug.Log($"bool:{eventParam.boolParam}");
        animator.SetBool("isEffect", eventParam.boolParam);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("CHANGESWORD", ChangeSword);
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("CHANGESWORD", ChangeSword);
    }
}
