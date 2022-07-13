using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public void OnHits(int damage)
    {
        hp -= damage;
        float hpPer = (float)hp / originHp;
        _slider.amount = hpPer;
        if (hp <= 0)
		{
            SceneManager.LoadScene(1);
		}
    }

    private void Awake()
    {
        _slider = GameObject.Find("PlayerBar").GetComponent<HPSlider>();
        
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
            playerDir = 0;
            isCheck = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerDir = 1;
            isCheck = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerDir = 2;
            isCheck = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerDir = 3;
            isCheck = true;
        }

        targetPos = transform.localPosition + dir[playerDir];
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
            SoundManager.Instance.SetPlayerEffectClip((int)PlayerEffectEnm.SWORD);
            playerAttack.CheckPos(enemyObject);
        }
    }
}
