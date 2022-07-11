using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMove))]
public class PlayerController : Character
{

    private CharacterMove characterMove;
    private Vector3[] dir = new Vector3[4];

    private float x, y;

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
        if (characterMove.IsMove) return;


        Vector3 targetPos = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.D))
            targetPos = transform.localPosition + dir[0];
        else if (Input.GetKeyDown(KeyCode.A))
            targetPos = transform.localPosition + dir[1];
        else if (Input.GetKeyDown(KeyCode.W))
            targetPos = transform.localPosition + dir[2];
        else if (Input.GetKeyDown(KeyCode.S))
            targetPos = transform.localPosition + dir[3];

        x = (targetPos.x - (GameManager.Instance.Size / 2 * -1.5f)) / 1.5f;
        y = (targetPos.y - (GameManager.Instance.Size / 2 * -1.5f)) / 1.5f;

        if (x < 0 || x >= GameManager.Instance.Width || y < 0 || y >= GameManager.Instance.Height)
            return;

        if (targetPos != Vector3.zero)
        {
            characterMove.CharacterMovement(targetPos);
        }
    }
}
