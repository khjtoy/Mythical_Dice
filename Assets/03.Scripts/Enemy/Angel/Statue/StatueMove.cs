using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StatueMove : CharacterMove
{
    private Sequence seq;
    private void Awake()
    {
    }
    public override void CharacterMovement(Vector2 target)
    {
        seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveZ(-3, 0.3f));
        seq.Append(transform.DOLocalMove(new Vector3(target.x, target.y, -3), 0.3f));
        seq.Append(transform.DOLocalMoveZ(-1, 0.1f).SetEase(Ease.InExpo));
        seq.Kill();
    }
}
