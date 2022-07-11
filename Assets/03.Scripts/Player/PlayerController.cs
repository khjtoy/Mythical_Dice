using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMove))]
public class PlayerController : Character
{

    private CharacterMove characterMove;
    private Vector3[] dir = new Vector3[4];

    private void Awake()
    {
        dir[0] = new Vector3(1.5f, 0, 0);
        dir[1] = new Vector3(-1.5f, 0, 0);
        dir[2] = new Vector3(0f, 1.5f, 0);
        dir[3] = new Vector3(0f, -1.5f, 0);
    }

    protected override void Start()
    {
        base.Start();
        characterMove = GetComponent<CharacterMove>();
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        Vector3 targetPos = Vector3.zero;
        int h = (int)Input.GetAxisRaw("Horizontal");
        int v = (int)Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            if (h == 1)
                targetPos = dir[0];
            else if (h == -1)
                targetPos = dir[1];
            else if (v == 1)
                targetPos = dir[2];
            else if (v == -1)
                targetPos = dir[3];

            characterMove.CharacterMovement(targetPos);
        }
    }
}
