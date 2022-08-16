using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class SirenWildWave : EnemyMove, IEnemyAttack
{
	private Sequence seq;
	public Vector2Int Pos;
	public override bool IsFloating { get; set; } = false;
	
	[field:SerializeField]
	public bool IsAttacking { get; set; } = false;
	public Animator animator { get; set; } = null;

	public bool isCheck = false;

	private int _moveCnt = 0;


	private GameObject dice;
	private Animator diceAni;
	private SetNumber setNumber;
	private Coroutine coroutine;

	Vector3 targetPos;
	private void Start()
	{
		EventManager.StartListening("RESETCHECK", OffCheck);
		EventManager.StartListening("KILLENEMY", KillEnemy);
		dice = GameObject.FindGameObjectWithTag("Dice");
		diceAni = dice.GetComponent<Animator>();
		setNumber = dice.transform.GetChild(0).GetComponent<SetNumber>();
		setNumber.gameObject.SetActive(false);
	}

	private void OffCheck(EventParam eventParam)
	{
		isCheck = false;
	}
	public override void CharacterMovement(Vector2 target)
	{
		seq = DOTween.Sequence();
		targetPos = target;
		IsAttacking = true;
		IsFloating = true;
		

        switch (_moveCnt)
        {
			case 0:
				targetPos = new Vector3(3, 0, -1);
				break;
			case 1:
				targetPos = new Vector3(-4.5f, 0, -1);
				break;
			case 2:
				targetPos = new Vector3(0, 3, -1);
				break;
			case 3:
				targetPos = new Vector3(0, -4.5f, -1);
				break;

        }

		seq.Append(transform.DOLocalMove(targetPos, 0));

		//Debug.Log(1);
		Invoke("ZeroTime", 0.6f);
		Invoke("ChangeTime", 0.62f);
		seq.AppendCallback(() =>
		{
			DoAttack();
			seq.Kill();
			IsFloating = false;

		});
	}

	public void KillEnemy(EventParam eventParam)
	{
		seq.Kill();
		seq = DOTween.Sequence();
		seq.Append(transform.DOLocalMoveZ(-1, 0.1f).SetEase(Ease.InExpo));
	}

	private void ChangeTime()
	{
		Time.timeScale = 1f;
	}

	private void ZeroTime()
	{
		Time.timeScale = 0.2f;
	}

	private IEnumerator ChangeDice(int x, int y)
	{
		yield return new WaitForSeconds(0.15f);
		diceAni.SetBool("IsDice", false);
		setNumber.SettingNumber(MapController.Instance.dices[y][x].randoms - 1);
		setNumber.gameObject.SetActive(true);
	}
	public void DoAttack()
	{
		StartCoroutine(AttackCoroutine());
	}

	private IEnumerator AttackCoroutine()
	{
		Vector2Int pos = new Vector2Int(MapController.PosToArray(transform.localPosition.x), MapController.PosToArray(transform.localPosition.y));
		diceAni.SetBool("IsDice", true);
		//if(setNumber == null) setNumber = dice.transform.GetChild(0).GetComponent<SetNumber>();
		setNumber.gameObject.SetActive(false);
		setNumber.isSurple = true;
		coroutine = StartCoroutine(ChangeDice(pos.x, pos.y));
		int value = MapController.Instance.GetIndexCost(pos.x, pos.y);
		GameManager.Instance.BossNum = value;
		for(int i = 0; i < GameManager.Instance.Size; i++)
        {
			for (int j = 0; j < GameManager.Instance.Size; j++)
            {
				switch (_moveCnt)
				{
					case 0:
						MapController.Instance.dices[j][GameManager.Instance.Size - 1 - i].DiceNumSelect(value);
						BoomMap.Instance.Boom(GameManager.Instance.Size - 1 - i, j);
						break;
				}
			}

			yield return new WaitForSeconds(1);
        }
	}

	public void OnDestroy()
	{
		EventManager.StopListening("KILLENEMY", KillEnemy);
		EventManager.StopListening("RESETCHECK", OffCheck);
	}

	private void OnApplicationQuit()
	{
		EventManager.StopListening("KILLENEMY", KillEnemy);
	}
}
