using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StatueMove : CharacterMove, IEnemyAttack
{
    private Sequence seq;
    public Vector2Int Pos;
    private void Awake()
    {
    }
    public override void CharacterMovement(Vector2 target)
    {
        seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveZ(-3, 0.3f));
        seq.Append(transform.DOLocalMove(new Vector3(target.x, target.y, -3), 0.3f));
        seq.Append(transform.DOLocalMoveZ(-1, 0.1f).SetEase(Ease.InExpo));
        seq.AppendCallback(() =>
        {
            seq.Kill();
            DoAttack();
        });
    }

    public void DoAttack()
    {
        int random = Random.Range(0, 2);
        int bossNum = Random.Range(1, 7);
        StartCoroutine(AttackCoroutine(random));
        GameManager.Instance.BossNum = bossNum;
    }

    private IEnumerator AttackCoroutine(int type)
    {
        Pos = new Vector2Int(MapController.PosToArray(transform.localPosition.x), MapController.PosToArray(transform.localPosition.y));
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
                    if(Pos.y - i >= 0)
                    {
                        MapController.Instance.dices[Pos.y - i][Pos.x].transform.rotation = Quaternion.Euler(0, 0, 0);
                        MapController.Instance.dices[Pos.y - i][Pos.x].isDiceDirecting = true;
                    }
                    if(Pos.x - i >= 0)
                    {   
                        MapController.Instance.dices[Pos.y][Pos.x - i].transform.rotation = Quaternion.Euler(0, 0, 0);
                        MapController.Instance.dices[Pos.y][Pos.x - i].isDiceDirecting = true;
                    }
                }
                yield return new WaitForSeconds(0.1f);
                for(int i = 1; i < GameManager.Instance.Size; i++)
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
