using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoSwing : EnemyMove, IEnemyAttack
{
    public override bool IsFloating { get; set; } = false;
    public bool IsAttacking { get; set; } = false;
    public Animator animator { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    [SerializeField]
    private int range = 0;
    public override void CharacterMovement(Vector2 target)
    {
		SoundManager.Instance.SetEnemyEffectClip(EnemyEffectEnum.MINOSWING);
		Vector2Int basePos = new Vector2Int(MapController.PosToArray(transform.localPosition.x), MapController.PosToArray(transform.localPosition.y));
        for (int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                Vector2Int temp = new Vector2Int(basePos.x + j, basePos.y + i);
                if((temp.x >= 0 && temp.x < GameManager.Instance.Size) && (temp.y >= 0 && temp.y < GameManager.Instance.Size))
                {

                    GameManager.Instance.BossNum = 2;
                    MapController.Instance.dices[temp.y][temp.x].DiceNumSelect(2);
                    BoomMap.Instance.Boom(temp.x, temp.y);
                    Debug.Log(temp);
                }

            }
        }
    }

    public void DoAttack()
    {

    }
}
