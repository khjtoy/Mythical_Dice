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


		Debug.Log(_moveCnt);
		if (_moveCnt > 3)
		{
			IsAttacking = false;
			_moveCnt = 0;
			return;
		}

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

		seq.Append(transform.DOLocalMove(targetPos, 1f));

		//Debug.Log(1);
		Invoke("ZeroTime", 0.6f);
		Invoke("ChangeTime", 0.62f);
		seq.AppendCallback(() =>
		{
			DoAttack();
			IsFloating = false;
			seq.Kill();

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
		StartCoroutine(AttackCoroutine(_moveCnt));
		_moveCnt++;
		StartCoroutine(MoveCoroutine(1f));
	}

	private IEnumerator AttackCoroutine(int cnt)
	{
		Vector2Int pos = new Vector2Int(MapController.PosToArray(transform.localPosition.x), MapController.PosToArray(transform.localPosition.y));
		diceAni.SetBool("IsDice", true);
		//if(setNumber == null) setNumber = dice.transform.GetChild(0).GetComponent<SetNumber>();
		setNumber.gameObject.SetActive(false);
		setNumber.isSurple = true;
		coroutine = StartCoroutine(ChangeDice(pos.x, pos.y));
		int value = MapController.Instance.GetIndexCost(pos.x, pos.y);
		GameManager.Instance.BossNum = value;

		for (int i = 0; i < GameManager.Instance.Size; i++)
        {
			for (int j = 0; j < GameManager.Instance.Size; j++)
            {
				switch (cnt)
				{
					case 0:
						MapController.Instance.dices[j][GameManager.Instance.Size - 1 - i].DiceNumSelect(value);
						BoomMap.Instance.Boom(GameManager.Instance.Size - 1 - i, j);
						break;
					case 1:
						MapController.Instance.dices[j][i].DiceNumSelect(value);
						BoomMap.Instance.Boom(i, j);
						break;
					case 2:
						MapController.Instance.dices[GameManager.Instance.Size - 1 - i][j].DiceNumSelect(value);
						BoomMap.Instance.Boom(j, GameManager.Instance.Size - 1 - i);
						break;
					case 3:
						MapController.Instance.dices[i][j].DiceNumSelect(value);
						BoomMap.Instance.Boom(j, i);
						break;
				}
			}

			yield return new WaitForSeconds(0.5f);
        }
	}

	private IEnumerator MoveCoroutine(float delay)
    {
		yield return new WaitForSeconds(delay);
		CharacterMovement(Vector2.zero);
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
