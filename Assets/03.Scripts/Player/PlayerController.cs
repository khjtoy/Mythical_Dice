using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
<<<<<<< HEAD
    private CharacterMove characterMove;
    private PlayerAttack playerAttack;
    private Vector3[] dir = new Vector3[4];

    private float x, y;
    private float monsterX, monsterY;

    private GameObject enemyObject;
=======

    private CharacterMove characterMove;
    private Vector3[] dir = new Vector3[4];

    private float x, y;
>>>>>>> origin/csh

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
<<<<<<< HEAD
        enemyObject = GameObject.FindGameObjectWithTag("ENEMY");
        characterMove = GetComponent<CharacterMove>();
        playerAttack = GetComponent<PlayerAttack>();
=======
        characterMove = GetComponent<CharacterMove>();
>>>>>>> origin/csh
    }

    private void Update()
    {
        PlayerMovement();
<<<<<<< HEAD
        PressAttack();
    }


    private void PlayerMovement()
    {
        if (characterMove.IsMove) return;


        Vector3 targetPos = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            targetPos = transform.localPosition + dir[0];
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            targetPos = transform.localPosition + dir[1];
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            targetPos = transform.localPosition + dir[2];
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            targetPos = transform.localPosition + dir[3];

        x = PosToArray(targetPos.x);
        y = PosToArray(targetPos.y);
        monsterX = PosToArray(enemyObject.transform.localPosition.x);
        monsterY = PosToArray(enemyObject.transform.localPosition.y);

        if (x < 0 || x >= GameManager.Instance.Width || y < 0 || y >= GameManager.Instance.Height
            || (x == monsterX && y == monsterY))
=======
    }

    private void PlayerMovement()
    {

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
>>>>>>> origin/csh
            return;

        if (targetPos != Vector3.zero)
        {
            characterMove.CharacterMovement(targetPos);
        }
    }
<<<<<<< HEAD

    private float PosToArray(float pos)
    {
        return (pos - (GameManager.Instance.Size / 2 * -1.5f)) / 1.5f;
    }

    private void PressAttack()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            playerAttack.CheckPos(enemyObject);
        }
    }
=======
>>>>>>> origin/csh
}
