using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;
using DG.Tweening;

public class StatueMove : CharacterMove, IEnemyAttack
{
	private Sequence seq;
	public Vector2Int Pos;
	public bool IsFoating = false;
	public bool isCheck = false;
	private void Start()
	{
		EventManager.StartListening("RESETCHECK", OffCheck);
	}

	private void OffCheck(EventParam eventParam)
	{
		isCheck = false;
	}
	public override void CharacterMovement(Vector2 target)
	{
		IsFoating = true;

		seq = DOTween.Sequence();
		seq.Append(transform.DOLocalMoveZ(-3, 0.3f));
		seq.Append(transform.DOLocalMove(new Vector3(target.x, target.y, -3), 0.3f));
		seq.Append(transform.DOLocalMoveZ(-1, 0.1f).SetEase(Ease.InExpo));
		//Debug.Log(1);
		Invoke("ZeroTime", 0.6f);
		Invoke("ChangeTime", 0.62f);
		seq.AppendCallback(() =>
		{
			seq.Kill();
			IsFoating = false;

			//아이템 생성
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

	private void ChangeTime()
	{
		Time.timeScale = 1f;
	}

	private void ZeroTime()
	{
		Time.timeScale = 0.2f;
	}

	public void DoAttack()
	{
		int random = Random.Range(0, 2);
		int bossNum = MapController.Instance.dices[MapController.PosToArray(transform.localPosition.y)][MapController.PosToArray(transform.localPosition.x)].randoms;
		Debug.Log(bossNum);
		StartCoroutine(AttackCoroutine(random));
		GameManager.Instance.BossNum = bossNum;
	}

	private IEnumerator AttackCoroutine(int type)
	{
		Pos = new Vector2Int(MapController.PosToArray(transform.localPosition.x), MapController.PosToArray(transform.localPosition.y));
		SoundManager.Instance.SetEffectClip((int)EffectEnum.BOOM);
		switch (type)
		{
			case 0:
				for (int i = -1; i < 2; i++)
				{
					for (int j = -1; j < 2; j++)
					{
						if (Pos.y + i < 0 || Pos.y + i >= GameManager.Instance.Height || Pos.x + j < 0 || Pos.x + j >= GameManager.Instance.Width)
							continue;
						if (i == 0 && j == 0)
							continue;
						MapController.Instance.dices[Pos.y + i][Pos.x + j].transform.rotation = Quaternion.Euler(0, 0, 0);
						MapController.Instance.dices[Pos.y + i][Pos.x + j].isDiceDirecting = true;
						MapController.Instance.dices[Pos.y + i][Pos.x + j].transform.DOLocalMoveZ(1.5f, 0.1f);
					}

				}
				yield return new WaitForSeconds(0.1f);
				for (int i = -1; i < 2; i++)
				{
					for (int j = -1; j < 2; j++)
					{
						if (Pos.y + i < 0 || Pos.y + i >= GameManager.Instance.Height || Pos.x + j < 0 || Pos.x + j >= GameManager.Instance.Width)
							continue;
						if (i == 0 && j == 0)
							continue;
						MapController.Instance.dices[Pos.y + i][Pos.x + j].transform.DOLocalMoveZ(0f, 0.1f);
						MapController.Instance.dices[Pos.y + i][Pos.x + j].DiceNumSelect(GameManager.Instance.BossNum);
					}
				}
				BoomMap.Instance.Boom();
				break;
			case 1:
				for (int i = 1; i < GameManager.Instance.Size; i++)
				{
					if (Pos.y + i < GameManager.Instance.Height)
					{
						MapController.Instance.dices[Pos.y + i][Pos.x].transform.rotation = Quaternion.Euler(0, 0, 0);
						MapController.Instance.dices[Pos.y + i][Pos.x].isDiceDirecting = true;
					}
					if (Pos.x + i < GameManager.Instance.Width)
					{
						MapController.Instance.dices[Pos.y][Pos.x + i].transform.rotation = Quaternion.Euler(0, 0, 0);
						MapController.Instance.dices[Pos.y][Pos.x + i].isDiceDirecting = true;
					}
					if (Pos.y - i >= 0)
					{
						MapController.Instance.dices[Pos.y - i][Pos.x].transform.rotation = Quaternion.Euler(0, 0, 0);
						MapController.Instance.dices[Pos.y - i][Pos.x].isDiceDirecting = true;
					}
					if (Pos.x - i >= 0)
					{
						MapController.Instance.dices[Pos.y][Pos.x - i].transform.rotation = Quaternion.Euler(0, 0, 0);
						MapController.Instance.dices[Pos.y][Pos.x - i].isDiceDirecting = true;
					}
				}
				yield return new WaitForSeconds(0.1f);
				for (int i = 1; i < GameManager.Instance.Size; i++)
				{
					if (Pos.y + i < GameManager.Instance.Height)
						MapController.Instance.dices[Pos.y + i][Pos.x].DiceNumSelect(GameManager.Instance.BossNum);
					if (Pos.x + i < GameManager.Instance.Width)
						MapController.Instance.dices[Pos.y][Pos.x + i].DiceNumSelect(GameManager.Instance.BossNum);
					if (Pos.y - i >= 0)
						MapController.Instance.dices[Pos.y - i][Pos.x].DiceNumSelect(GameManager.Instance.BossNum);
					if (Pos.x - i >= 0)
						MapController.Instance.dices[Pos.y][Pos.x - i].DiceNumSelect(GameManager.Instance.BossNum);
				}

				BoomMap.Instance.Boom();
				break;
		}

	}
}
