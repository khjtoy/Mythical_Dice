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
        seq.Append(transform.DOLocalMoveZ(-3, 0.3f));
        seq.Append(transform.DOLocalMove(new Vector3(target.x, target.y, -3), 0.3f));
        seq.Append(transform.DOLocalMoveZ(-1, 0.1f).SetEase(Ease.InExpo));
    }
}
