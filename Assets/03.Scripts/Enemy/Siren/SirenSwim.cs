using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class SirenSwim : EnemyMove, IEnemyAttack
{
    public bool IsAttacking { get; set; }
    public Animator animator { get; set; }
    public override bool IsFloating { get; set; }


	private Sequence seq;
	private GameObject dice;
	private Animator diceAni;
	private SetNumber setNumber;
	private Vector2 Target = Vector2.zero;
	public bool isCheck = false;
	private Coroutine coroutine;


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
		GameManager.Instance.BossNum = 3;
		SoundManager.Instance.SetEnemyEffectClip((int)EnemyEffectEnum.MINOSTAOMP);
		for (int i = 1; i <= GameManager.Instance.Size; i++)
		{
			for (int j = -i; j <= i; j++)
			{
				for (int k = -i; k <= i; k++)
				{
					if (pos.y + j < 0 || pos.y + j >= GameManager.Instance.Size || pos.x + k < 0 || pos.x + k >= GameManager.Instance.Size)
						continue;
					if (pos.y + j == pos.y + i || pos.y + j == pos.y - i
						|| pos.x + k == pos.x + i || pos.x + k == pos.x - i)
					{
						MapController.Instance.dices[pos.y + j][pos.x + k].DiceNumSelect(3);
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
		IsFloating = true;
		diceAni.SetBool("IsDice", true);
		//if(setNumber == null) setNumber = dice.transform.GetChild(0).GetComponent<SetNumber>();
		setNumber.gameObject.SetActive(false);
		setNumber.isSurple = true;
		int x = MapController.PosToArray(target.x);
		int y = MapController.PosToArray(target.y);
		coroutine = StartCoroutine(ChangeDice(x, y));
		//StartCoroutine(setNumber.SurpleNumber());

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
			IsFloating = false;
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
