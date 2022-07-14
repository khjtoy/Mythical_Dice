using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMove : EnemyMove
{
	private Sequence seq;

	public Vector2Int Pos;
	public override bool IsFloating { get; set; } = false;

	public override void CharacterMovement(Vector2 target)
    {
		SoundManager.Instance.SetEnemyEffectClip((int)EnemyEffectEnum.SlimeJump);
		seq = DOTween.Sequence();
		seq.Append(transform.DOLocalMoveZ(-3, 0.3f));
		seq.Append(transform.DOLocalMove(new Vector3(target.x, target.y, -3), 0.3f));
		seq.Append(transform.DOLocalMoveZ(-1, 0.1f).SetEase(Ease.InExpo));
		seq.AppendCallback(() =>
        {
			int bossNum = MapController.Instance.dices[MapController.PosToArray(transform.localPosition.y)][MapController.PosToArray(transform.localPosition.x)].randoms;
			GameManager.Instance.BossNum = bossNum;

			StartCoroutine(MoveCoroutine());

        });
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
}
