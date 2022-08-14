using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class SirenSwim : EnemyMove, IEnemyAttack
{
    public bool IsAttacking { get; set; }
    public Animator animator { get; set; }
	public override bool IsFloating { get; set; } = false;

	private Sequence seq;
	private GameObject dice;
	private Animator diceAni;
	private SetNumber setNumber;
	private Vector2 Target = Vector2.zero;
	public bool isCheck = false;
	private Coroutine coroutine;

	int value;
	private void Start()
	{
		dice = GameObject.FindGameObjectWithTag("Dice");
		diceAni = dice.GetComponent<Animator>();
		setNumber = dice.transform.GetChild(0).GetComponent<SetNumber>();
		setNumber.gameObject.SetActive(false);
	}

	public void DoAttack()
    {
		StartCoroutine(SwimCoroutine());
    }

	private IEnumerator SwimCoroutine()
    {
		yield return new WaitForSeconds(0.2f);
		Vector2Int pos = new Vector2Int(MapController.PosToArray(transform.localPosition.x), MapController.PosToArray(transform.localPosition.y));
		GameManager.Instance.BossNum = value;
		SoundManager.Instance.SetEnemyEffectClip((int)EnemyEffectEnum.MINOSTAOMP);
		int range = 0;
		if (IsFloating)
			range = 2;
		else
			range = GameManager.Instance.Size;
			
		for (int i = 1; i <= range; i++)
		{
			for(int j = -i; j <= i ; j++)
            {
				for(int k = -i; k <= i ; k++)
                {
					if (pos.y + j < 0 || pos.y + j >= GameManager.Instance.Size || pos.x + k < 0 || pos.x + k >= GameManager.Instance.Size)

						continue;
					if ((Mathf.Abs(k) == i && j == 0) || (Mathf.Abs(j) == i && k == 0))
                    {
						
						MapController.Instance.dices[pos.y + j][pos.x + k].DiceNumSelect(value);
						BoomMap.Instance.Boom(pos.x + k, pos.y + j);
					}

				}
			}
			for (int j = 1; j <= i; j++)
			{
				for (int k = -i + j; k < 0; k++)
                {
					if (k != -i + j)
						continue;
					if (pos.y + j < 0 || pos.y + j >= GameManager.Instance.Size || pos.x + k < 0 || pos.x + k >= GameManager.Instance.Size)

						continue;
                    {
						MapController.Instance.dices[pos.y + j][pos.x + k].DiceNumSelect(value);

						BoomMap.Instance.Boom(pos.x + k, pos.y + j);
					}
				}
			}

			for (int j = 1; j <= i; j++)
			{
				for (int k = 1; k <= i - j; k++)
				{
					if (k != i - j)
						continue;
					if (pos.y + j < 0 || pos.y + j >= GameManager.Instance.Size || pos.x + k < 0 || pos.x + k >= GameManager.Instance.Size)

						continue;
					{
						MapController.Instance.dices[pos.y + j][pos.x + k].DiceNumSelect(value);

						BoomMap.Instance.Boom(pos.x + k, pos.y + j);
					}
				}
			}
			for (int j = -i; j <= -1; j++)
			{
				for (int k = 1; k <= i + j; k++)
				{
					if (k != i + j)
						continue;
					if (pos.y + j < 0 || pos.y + j >= GameManager.Instance.Size || pos.x + k < 0 || pos.x + k >= GameManager.Instance.Size)

						continue;
					{
						MapController.Instance.dices[pos.y + j][pos.x + k].DiceNumSelect(value);

						BoomMap.Instance.Boom(pos.x + k, pos.y + j);
					}
				}
			}
			for (int j = -i; j <= -1; j++)
			{
				for (int k = -i - j; k <= -1; k++)
				{
					if (k != -i - j)
						continue;
					if (pos.y + j < 0 || pos.y + j >= GameManager.Instance.Size || pos.x + k < 0 || pos.x + k >= GameManager.Instance.Size)

						continue;
					{
						MapController.Instance.dices[pos.y + j][pos.x + k].DiceNumSelect(value);

						BoomMap.Instance.Boom(pos.x + k, pos.y + j);
					}
				}
			}
			yield return new WaitForSeconds(0.5f);
		}

	}

	public override void CharacterMovement(Vector2 target)
    {
		IsAttacking = true;
		diceAni.SetBool("IsDice", true);
		//if(setNumber == null) setNumber = dice.transform.GetChild(0).GetComponent<SetNumber>();
		setNumber.gameObject.SetActive(false);
		setNumber.isSurple = true;
		int x = MapController.PosToArray(target.x);
		int y = MapController.PosToArray(target.y);
		coroutine = StartCoroutine(ChangeDice(x, y));
		value = MapController.Instance.GetIndexCost(x, y);

		//StartCoroutine(setNumber.SurpleNumber());

		seq = DOTween.Sequence();
		seq.Append(transform.DOLocalMove(new Vector3(target.x, target.y, -1), 0.3f));
        if (IsFloating)
        {
			seq.Append(transform.DOLocalMoveZ(-3, 0.3f));
			seq.Append(transform.DOLocalMoveZ(-1, 0.1f).SetEase(Ease.OutExpo));
		}
		seq.AppendCallback(() =>
		{
			seq.Kill();
			//setNumber.isSurple = false;

			//������ ����
			if (!isCheck)
			{
				int random = Random.Range(0, 5);
				if (random == 0)
				{
					GameObject item = PoolManager.Instance.GetPooledObject((int)PooledObject.Item);
					item.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -1);
					item.SetActive(true);
					isCheck = true;
				}
			}

			DoAttack();
		});
	}
	private IEnumerator ChangeDice(int x, int y)
	{
		yield return new WaitForSeconds(0.15f);
		diceAni.SetBool("IsDice", false);
		setNumber.SettingNumber(MapController.Instance.dices[y][x].randoms - 1);
		setNumber.gameObject.SetActive(true);
	}
}
