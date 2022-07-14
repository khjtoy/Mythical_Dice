using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : Character, OnHit
{
	[Header("HP ?�라?�더")]
	[SerializeField]
	Image playerHpSlider;
	[SerializeField]
	Image whiteSlider;

	[SerializeField]
	private float sliderSpeed;

	public bool isDamage = true;

	private CharacterMove characterMove;
	private PlayerAttack playerAttack;
	private Vector3[] dir = new Vector3[4];
	private HPSlider _slider;

	private float x, y;
	private float monsterX, monsterY;

	private GameObject enemyObject;

	public int playerDir; // 0:Right 1:Left 2:Up 3:Down

	[Header("�÷��̾� HP")]
	[SerializeField]
	private int originHp = 0;
	[SerializeField]
	private int hp;

	public bool isStop = false;

	[Header("Move ��ȯ")]
	[SerializeField]
	private float deleteMoveTime;
	Queue<int> moveDir;

	[SerializeField]
	private Image image;

	private Material spriteMaterial;

	private EnemyController enemyController;

	public bool hasStart = false;
    private bool isDeath = false;

    public void OnHits(int damage)
	{
		if (isDeath) return;

		if (isStop) return;
		hp -= damage;
		float hpPer = (float)hp / originHp;
		_slider.UpdateAmount(hpPer);
		isDamage = true;
		Animator.SetTrigger("Hit");
		spriteMaterial.EnableKeyword("_SordColor");
		spriteMaterial.SetFloat("_SordColor", 0f);
		spriteMaterial.DisableKeyword("_SordColor");
		Define.MainCam.transform.DOShakePosition(0.3f);
		if (hp <= 0)
		{
			isStop = true;
			isDeath = true;
			GetComponent<PlayerDie>().DieAction();
			//SceneManager.LoadScene(1);
		}
		else if ((float)hp / originHp * 100 <= 50)
		{
			if ((float)hp / originHp * 100 <= 30)
				SoundManager.Instance.SetBackgroundSpeed(1.5f);
			Color a = image.color;
			a.a = 0.5f - (0.5f * ((float)((float)hp / originHp * 100) / 100));
			image.color = a;
		}
	}

	void UpdateSlider()
	{
		if (isDamage)
		{
			whiteSlider.transform.localScale = Vector3.Lerp(whiteSlider.transform.localScale, playerHpSlider.transform.localScale, Time.deltaTime * sliderSpeed);
			if (playerHpSlider.transform.localScale.x >= whiteSlider.transform.localScale.x - 0.01f)
			{
				isDamage = false;
				whiteSlider.transform.localScale = playerHpSlider.transform.localScale;
			}
		}

	}

	private void Awake()
	{
		_slider = GameObject.Find("PlayerBar").GetComponent<HPSlider>();
		spriteMaterial = transform.GetChild(0).GetComponent<SpriteRenderer>().material;
		moveDir = new Queue<int>();

		dir[0] = new Vector3(1.5f, 0, 0);
		dir[1] = new Vector3(-1.5f, 0, 0);
		dir[2] = new Vector3(0f, 1.5f, 0);
		dir[3] = new Vector3(0f, -1.5f, 0);
	}

	protected override void Start()
	{
		base.Start();
		characterMove = GetComponent<CharacterMove>();
		playerAttack = GetComponent<PlayerAttack>();
	}

    public void Init()
    {
		enemyObject = GameObject.FindGameObjectWithTag("ENEMY");
		enemyController = enemyObject.GetComponent<EnemyController>();
		hasStart = true;
	}

    private void Update()
	{

		if (!hasStart)
			return;
		if (isStop) return;

		if (moveDir.Count > 0 && !characterMove.IsMove && !enemyController.isDeath)
			PopMove();

		UpdateSlider();

		if (enemyController.isDeath) return;

		PlayerMovement();
		PressAttack();
	}

	//private void LateUpdate()
	//{
	//	UpdateSlider();
	//}


	private void PlayerMovement()
	{
		//if (characterMove.IsMove) return;

		if (moveDir.Count > 3) return;

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
		if (Input.GetKeyDown(KeyCode.Z))
		{
			moveDir.Clear();
			SoundManager.Instance.SetPlayerAttackEffectClip((int)PlayerEffectEunm.SWORD);
			playerAttack.CheckPos(enemyObject);
		}
	}
}
