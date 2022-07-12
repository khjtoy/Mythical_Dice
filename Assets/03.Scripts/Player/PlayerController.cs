using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    private CharacterMove characterMove;
    private PlayerAttack playerAttack;
    private Vector3[] dir = new Vector3[4];

    private float x, y;
    private float monsterX, monsterY;

    private GameObject enemyObject;

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
        enemyObject = GameObject.FindGameObjectWithTag("ENEMY");
        characterMove = GetComponent<CharacterMove>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        PlayerMovement();
        PressAttack();
    }


    private void PlayerMovement()
    {
        if (characterMove.IsMove) return;


        Vector3 targetPos = Vector3.zero;
        bool isCheck = false;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetPos = transform.localPosition + dir[0];
            isCheck = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetPos = transform.localPosition + dir[1];
            isCheck = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetPos = transform.localPosition + dir[2];
            isCheck = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetPos = transform.localPosition + dir[3];
            isCheck = true;
        }

        if (isCheck)
        {
            x = MapController.PosToArray(targetPos.x);
            y = MapController.PosToArray(targetPos.y);
            //Debug.Log($"Player x:{x}, y:{y}");
            monsterX = MapController.PosToArray(enemyObject.transform.localPosition.x);
            monsterY = MapController.PosToArray(enemyObject.transform.localPosition.y);
            //Debug.Log($"Monster x:{monsterX}, y:{monsterY}");

            if (x < 0 || x >= GameManager.Instance.Width || y < 0 || y >= GameManager.Instance.Height
                || (x == monsterX && y == monsterY))
                return;
            characterMove.CharacterMovement(targetPos);
        }
    }

    private void PressAttack()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            playerAttack.CheckPos(enemyObject);
        }
    }
}
