using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : Character, OnHit
{
    private CharacterMove characterMove;
    private PlayerAttack playerAttack;
    private Vector3[] dir = new Vector3[4];
    private HPSlider _slider;

    private float x, y;
    private float monsterX, monsterY;

    private GameObject enemyObject;

    public int playerDir; // 0:Right 1:Left 2:Up 3:Down

    [Header("플레이어 HP")]
    [SerializeField]
    private int originHp = 0;
    [SerializeField]
    private int hp;

    public bool isStop = false;

    [Header("Move 순환")]
    [SerializeField]
    private float deleteMoveTime;
    Queue<int> moveDir;
    public void OnHits(int damage)
    {
        hp -= damage;
        float hpPer = (float)hp / originHp;
        _slider.amount = hpPer;
        if (hp <= 0)
		{
            isStop = true;
            GetComponent<PlayerDie>().DieAction();
            //SceneManager.LoadScene(1);
		}
    }

    private void Awake()
    {
        _slider = GameObject.Find("PlayerBar").GetComponent<HPSlider>();
        moveDir = new Queue<int>();
        
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
        if (isStop) return;

        if (moveDir.Count > 0 && !characterMove.IsMove)
            PopMove();

        PlayerMovement();
        PressAttack();
    }


    private void PlayerMovement()
    {
        //if (characterMove.IsMove) return;


        Vector3 targetPos = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerDir = 0;
            moveDir.Enqueue(playerDir);
            Debug.Log("queue");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerDir = 1;
            moveDir.Enqueue(playerDir);
            Debug.Log("queue");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerDir = 2;
            moveDir.Enqueue(playerDir);
            Debug.Log("queue");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerDir = 3;
            moveDir.Enqueue(playerDir);
            Debug.Log("queue");
        }
    }

    private void PopMove()
    {
        Vector3 targetPos = transform.localPosition + dir[moveDir.Dequeue()];
        Debug.Log("Dequeue");
        x = MapController.PosToArray(targetPos.x);
        y = MapController.PosToArray(targetPos.y);
        monsterX = MapController.PosToArray(enemyObject.transform.localPosition.x);
        monsterY = MapController.PosToArray(enemyObject.transform.localPosition.y);
        if (x < 0 || x >= GameManager.Instance.Width || y < 0 || y >= GameManager.Instance.Height
            || (x == monsterX && y == monsterY))
                return;
        SoundManager.Instance.SetPlayerDashEffectClip((int)PlayerEffectEunm.DASH);
        characterMove.CharacterMovement(targetPos);
    }

    private void PressAttack()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            moveDir.Clear();
            SoundManager.Instance.SetPlayerAttackEffectClip((int)PlayerEffectEunm.SWORD);
            playerAttack.CheckPos(enemyObject);
        }
    }
}
