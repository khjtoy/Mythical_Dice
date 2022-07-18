using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class SlimeMove : EnemyMove, IEnemyAttack
{
	private Sequence seq;

	public Vector2Int Pos;

	private GameObject dice;
	private Animator diceAni;
	private SetNumber setNumber;

	public bool isCheck = false;
	private bool isStart = false;
	private bool twoTutorial = false;
	public override bool IsFloating { get; set; } = false;
    public bool IsAttacking { get; set; }
	public Animator animator { get; set; } = null;

    private void Start()
    {
		EventManager.StartListening("RESETCHECK", OffCheck);
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
		SoundManager.Instance.SetEnemyEffectClip((int)EnemyEffectEnum.JumpSlime);

		diceAni.SetBool("IsDice", true);
		setNumber.gameObject.SetActive(false);
		setNumber.isSurple = true;
		int x = MapController.PosToArray(target.x);
		int y = MapController.PosToArray(target.y);
		Debug.Log($"X:{x}Y:{y}");
		StartCoroutine(ChangeDice(x, y));

		seq = DOTween.Sequence();
		seq.Append(transform.DOLocalMoveZ(-3, 0.3f));
		seq.Append(transform.DOLocalMove(new Vector3(target.x, target.y, -3), 0.3f));
		seq.Append(transform.DOLocalMoveZ(-1, 0.1f).SetEase(Ease.InExpo));
		DoAttack();
    }

	private IEnumerator ChangeDice(int x, int y)
	{
		yield return new WaitForSeconds(0.15f);
		diceAni.SetBool("IsDice", false);
		setNumber.SettingNumber(MapController.Instance.dices[y][x].randoms - 1);
		setNumber.gameObject.SetActive(true);
	}

	private IEnumerator MoveCoroutine()
    {
		Pos = new Vector2Int(MapController.PosToArray(transform.localPosition.x), MapController.PosToArray(transform.localPosition.y));
		SoundManager.Instance.SetEffectClip((int)EffectEnum.BOOM);
		for (int i = -1; i < 2; i++)
		{
			for (int j = -1; j < 2; j++)
			{
				if (Pos.y + i < 0 || Pos.y + i >= GameManager.Instance.Height || Pos.x + j < 0 || Pos.x + j >= GameManager.Instance.Width)
					continue;
				if(Pos.y == Pos.y + i || Pos.x == Pos.x + j)
                {
					MapController.Instance.dices[Pos.y + i][Pos.x + j].transform.rotation = Quaternion.Euler(0, 0, 0);
					MapController.Instance.dices[Pos.y + i][Pos.x + j].isDiceDirecting = true;
					MapController.Instance.dices[Pos.y + i][Pos.x + j].transform.DOLocalMoveZ(1.5f, 0.1f);
				}
			}

		}
		yield return new WaitForSeconds(0.1f);
		for (int i = -1; i < 2; i++)
		{
			for (int j = -1; j < 2; j++)
			{
				if (Pos.y + i < 0 || Pos.y + i >= GameManager.Instance.Height || Pos.x + j < 0 || Pos.x + j >= GameManager.Instance.Width)
					continue;
				if (Pos.y == Pos.y + i || Pos.x == Pos.x + j)
                {
					MapController.Instance.dices[Pos.y + i][Pos.x + j].transform.DOLocalMoveZ(0f, 0.1f);
					MapController.Instance.dices[Pos.y + i][Pos.x + j].DiceNumSelect(GameManager.Instance.BossNum);
					BoomMap.Instance.Boom(Pos.x + j, Pos.y + i);
				}

			}
		}
	}

    public void DoAttack()
    {
		seq.AppendCallback(() =>
		{
			if (!twoTutorial)
			{
				twoTutorial = true;
				TutorialAction.Instance.TuturialMode();
			}
			int bossNum = MapController.Instance.dices[MapController.PosToArray(transform.localPosition.y)][MapController.PosToArray(transform.localPosition.x)].randoms;
			GameManager.Instance.BossNum = bossNum;

			StartCoroutine(MoveCoroutine());


			//������ ����
			if (!isCheck)
			{
				int random = Random.Range(0, 5);
				if (random == 0 || (PlayerPrefs.GetInt("TUTORIAL", 0) == 0 && isStart == false))
				{
					GameObject item = PoolManager.Instance.GetPooledObject((int)PooledObject.Item);
					item.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -1);
					item.SetActive(true);
					isCheck = true;
					isStart = true;
				}
			}

		});
	}
}
