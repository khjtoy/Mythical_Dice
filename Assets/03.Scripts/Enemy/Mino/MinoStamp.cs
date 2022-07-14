using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoStamp : EnemyMove
{
    public override bool IsFloating { get; set; } = false;
    private Sequence seq;

    public override void CharacterMovement(Vector2 target)
    {
        IsFloating = true;

        seq = DOTween.Sequence();
        CharacterAnimation.PlayAnimator("jump");
        seq.Append(transform.DOLocalMoveZ(-3, 0.3f));
        seq.Append(transform.DOLocalMove(new Vector3(target.x, target.y, -3), 0.3f));
        seq.Append(transform.DOLocalMoveZ(-1, 0.1f).SetEase(Ease.InExpo));
        seq.AppendCallback(() => CharacterAnimation.PlayAnimator("stamp"));


        
        seq.AppendCallback(() =>
        {
            StartCoroutine(StapCoroutine());
            seq.Kill();
            IsFloating = false;
        });
    }

    private IEnumerator StapCoroutine()
    {
        CharacterAnimation.PlayAnimator("Idle");
        Vector2Int pos = new Vector2Int(MapController.PosToArray(transform.localPosition.x), MapController.PosToArray(transform.localPosition.y));
        GameManager.Instance.BossNum = 3;
        for (int i = 1; i <=  GameManager.Instance.Size; i++)
        {
            for (int j = -i; j <= i; j++)
            {
                for (int k = -i; k <= i; k++)
                {
                    if (pos.y + j < 0 || pos.y + j >= GameManager.Instance.Size || pos.x + k < 0 || pos.x + k >= GameManager.Instance.Size)
                        continue;
                    if(pos.y + j == pos.y + i || pos.y + j == pos.y - i
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
}
