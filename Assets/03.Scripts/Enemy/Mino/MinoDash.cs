using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoDash : EnemyMove
{
    private Sequence seq;
    public bool IsDashing = false;

    public override bool IsFloating { get; set; } = false;

    public override void CharacterMovement(Vector2 target)
    {
        SoundManager.Instance.SetEnemyEffectClip((int)EnemyEffectEnum.MINORUN);
        IsDashing = true;
        CharacterAnimation.PlayAnimator("dash");
        seq = DOTween.Sequence();
        GameManager.Instance.BossNum = MapController.Instance.dices[MapController.PosToArray(transform.localPosition.y)][MapController.PosToArray(transform.localPosition.x)].randoms;
        Vector2Int targetInt = new Vector2Int(Mathf.RoundToInt(target.x), Mathf.RoundToInt(target.y));
        if (target.x > 0.5f)
        {
            int x = MapController.PosToArray(transform.localPosition.x);
            for (int i = x; i < GameManager.Instance.Width; i++)
            {
                int n = i;
                Vector2Int pos = new Vector2Int(MapController.PosToArray(transform.localPosition.x),MapController.PosToArray(transform.localPosition.y));
                seq.AppendCallback(() =>
                {
                    if (n < GameManager.Instance.Width - 1)
                    {
                        if (pos.y + 1 < GameManager.Instance.Height)
                        {
                            MapController.Instance.dices[pos.y + 1][n + 1].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[pos.y + 1][n].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[pos.y + 1][n + 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[pos.y + 1][n + 1].isDiceDirecting = true;
                            MapController.Instance.dices[pos.y + 1][n].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[pos.y + 1][n].isDiceDirecting = true;
                        }
                        if (pos.y - 1 >= 0)
                        {
                            MapController.Instance.dices[pos.y - 1][n + 1].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[pos.y - 1][n].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[pos.y - 1][n + 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[pos.y - 1][n + 1].isDiceDirecting = true;
                            MapController.Instance.dices[pos.y - 1][n].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[pos.y - 1][n].isDiceDirecting = true;
                        }
                        MapController.Instance.dices[pos.y][n + 1].transform.DOLocalMoveZ(1f, 0.1f);
                        MapController.Instance.dices[pos.y][n + 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                        MapController.Instance.dices[pos.y][n + 1].isDiceDirecting = true;
                    }
                });
                seq.Append(transform.DOLocalMoveX(n * 1.5f - GameManager.Instance.Width / 2 * 1.5f, 0.1f));
                seq.AppendCallback(() =>
                {

                    if (n < GameManager.Instance.Width - 1)
                    {
                        if (pos.y + 1 < GameManager.Instance.Height)
                        {
                            MapController.Instance.dices[pos.y + 1][n + 1].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[pos.y + 1][n].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[pos.y + 1][n + 1].DiceNumSelect(GameManager.Instance.BossNum);
                            MapController.Instance.dices[pos.y + 1][n].DiceNumSelect(GameManager.Instance.BossNum);
                        }
                        if (pos.y - 1 >= 0)
                        {
                            MapController.Instance.dices[pos.y - 1][n + 1].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[pos.y - 1][n + 1].DiceNumSelect(GameManager.Instance.BossNum);
                            MapController.Instance.dices[pos.y - 1][n].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[pos.y - 1][n].DiceNumSelect(GameManager.Instance.BossNum);
                        }
                        MapController.Instance.dices[pos.y][n + 1].transform.DOLocalMoveZ(0f, 0.1f);
                        MapController.Instance.dices[pos.y][n + 1].DiceNumSelect(GameManager.Instance.BossNum);
                    }
                });
            }
        }
        if (target.x < -0.5f)
        {
            int x = MapController.PosToArray(transform.localPosition.x);

            for (int i = x; i >= 0; i--)
            {
                int n = i;
                Vector2Int pos = new Vector2Int(MapController.PosToArray(transform.localPosition.x),MapController.PosToArray(transform.localPosition.y));
                seq.AppendCallback(() =>
                {
                    if (n > 0)
                    {
                        if (pos.y + 1 < GameManager.Instance.Height)
                        {
                            MapController.Instance.dices[pos.y + 1][n - 1].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[pos.y + 1][n].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[pos.y + 1][n - 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[pos.y + 1][n - 1].isDiceDirecting = true;
                            MapController.Instance.dices[pos.y + 1][n].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[pos.y + 1][n].isDiceDirecting = true;
                        }
                        if (pos.y - 1 >= 0)
                        {
                            MapController.Instance.dices[pos.y - 1][n - 1].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[pos.y - 1][n].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[pos.y - 1][n - 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[pos.y - 1][n - 1].isDiceDirecting = true;
                            MapController.Instance.dices[pos.y - 1][n].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[pos.y - 1][n].isDiceDirecting = true;
                        }
                        MapController.Instance.dices[pos.y][n - 1].transform.DOLocalMoveZ(1f, 0.1f);
                        MapController.Instance.dices[pos.y][n - 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                        MapController.Instance.dices[pos.y][n - 1].isDiceDirecting = true;
                    }
                });
                seq.Append(transform.DOLocalMoveX(n * 1.5f - GameManager.Instance.Width / 2 * 1.5f, 0.1f));

                seq.AppendCallback(() =>
                {
                    if (n > 0)
                    {
                        if (pos.y + 1 < GameManager.Instance.Height)
                        {
                            MapController.Instance.dices[pos.y + 1][n - 1].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[pos.y + 1][n - 1].DiceNumSelect(GameManager.Instance.BossNum);
                            MapController.Instance.dices[pos.y + 1][n].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[pos.y + 1][n].DiceNumSelect(GameManager.Instance.BossNum);
                        }
                        if (pos.y - 1 >= 0)
                        {
                            MapController.Instance.dices[pos.y - 1][n - 1].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[pos.y - 1][n - 1].DiceNumSelect(GameManager.Instance.BossNum);
                            MapController.Instance.dices[pos.y - 1][n].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[pos.y - 1][n].DiceNumSelect(GameManager.Instance.BossNum);
                        }
                        MapController.Instance.dices[pos.y][n - 1].transform.DOLocalMoveZ(0f, 0.1f);
                        MapController.Instance.dices[pos.y][n - 1].DiceNumSelect(GameManager.Instance.BossNum);
                    }
                });
            }
        }
        if (target.y > 0.5f)
        {
            int y = MapController.PosToArray(transform.localPosition.y);

            for (int i = y; i < GameManager.Instance.Height; i++)
            {
                int n = i;
                Vector2Int pos = new Vector2Int(MapController.PosToArray(transform.localPosition.x),MapController.PosToArray(transform.localPosition.y));
                seq.AppendCallback(() =>
                {
                    if (n < GameManager.Instance.Height - 1)
                    {
                        if (pos.x + 1 < GameManager.Instance.Height)
                        {
                            MapController.Instance.dices[n + 1][pos.x + 1].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[n][pos.x + 1].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[n + 1][pos.x + 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[n + 1][pos.x + 1].isDiceDirecting = true;
                            MapController.Instance.dices[n][pos.x + 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[n][pos.x + 1].isDiceDirecting = true;
                        }
                        if (pos.x - 1 >= 0)
                        {
                            MapController.Instance.dices[n + 1][pos.x - 1].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[n][pos.x - 1].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[n + 1][pos.x - 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[n + 1][pos.x - 1].isDiceDirecting = true;
                            MapController.Instance.dices[n][pos.x - 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[n][pos.x - 1].isDiceDirecting = true;
                        }
                        MapController.Instance.dices[n + 1][pos.x].transform.DOLocalMoveZ(1f, 0.1f);
                        MapController.Instance.dices[n + 1][pos.x].transform.rotation = Quaternion.Euler(0, 0, 0);
                        MapController.Instance.dices[n + 1][pos.x].isDiceDirecting = true;
                    }
                });
                seq.Append(transform.DOLocalMoveY(n * 1.5f - GameManager.Instance.Width / 2 * 1.5f, 0.1f));
                seq.AppendCallback(() =>
                {
                    if (n < GameManager.Instance.Height - 1)
                    {
                        if (pos.x + 1 < GameManager.Instance.Height)
                        {
                            MapController.Instance.dices[n + 1][pos.x + 1].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[n + 1][pos.x + 1].DiceNumSelect(GameManager.Instance.BossNum);
                            MapController.Instance.dices[n][pos.x + 1].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[n][pos.x + 1].DiceNumSelect(GameManager.Instance.BossNum);

                        }
                        if (pos.x - 1 >= 0)
                        {
                            MapController.Instance.dices[n + 1][pos.x - 1].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[n + 1][pos.x - 1].DiceNumSelect(GameManager.Instance.BossNum);
                            MapController.Instance.dices[n][pos.x - 1].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[n][pos.x - 1].DiceNumSelect(GameManager.Instance.BossNum);

                        }
                        MapController.Instance.dices[n + 1][pos.x].transform.DOLocalMoveZ(0f, 0.1f);
                        MapController.Instance.dices[n + 1][pos.x].DiceNumSelect(GameManager.Instance.BossNum);
                    }
                });
            }
        }
        if (target.y < -0.5f)
        {
            int y = MapController.PosToArray(transform.localPosition.y);
            for (int i = y; i >= 0; i--)
            {
                int n = i;
                Vector2Int pos = new Vector2Int(MapController.PosToArray(transform.localPosition.x),MapController.PosToArray(transform.localPosition.y));
                seq.AppendCallback(() =>
                {
                    if(n > 0)
                    {
                        if (pos.x + 1 < GameManager.Instance.Height)
                        {
                            MapController.Instance.dices[n - 1][pos.x + 1].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[n][pos.x + 1].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[n - 1][pos.x + 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[n - 1][pos.x + 1].isDiceDirecting = true;
                            MapController.Instance.dices[n][pos.x + 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[n][pos.x + 1].isDiceDirecting = true;
                        }
                        if (pos.x - 1 >= 0)
                        {
                            MapController.Instance.dices[n -  1][pos.x - 1].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[n][pos.x - 1].transform.DOLocalMoveZ(1f, 0.1f);
                            MapController.Instance.dices[n - 1][pos.x - 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[n - 1][pos.x - 1].isDiceDirecting = true;
                            MapController.Instance.dices[n][pos.x - 1].transform.rotation = Quaternion.Euler(0, 0, 0);
                            MapController.Instance.dices[n][pos.x - 1].isDiceDirecting = true;
                        }
                        MapController.Instance.dices[n - 1][pos.x].transform.DOLocalMoveZ(1f, 0.1f);
                        MapController.Instance.dices[n - 1][pos.x].transform.rotation = Quaternion.Euler(0, 0, 0);
                        MapController.Instance.dices[n - 1][pos.x].isDiceDirecting = true;
                    }
                });
                seq.Append(transform.DOLocalMoveY(n * 1.5f - GameManager.Instance.Width / 2 * 1.5f, 0.1f));
                seq.AppendCallback(() =>
                {
                    if (n > 0)
                    {
                        if (pos.x + 1 < GameManager.Instance.Height)
                        {
                            MapController.Instance.dices[n - 1][pos.x + 1].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[n - 1][pos.x + 1].DiceNumSelect(GameManager.Instance.BossNum);
                            MapController.Instance.dices[n][pos.x + 1].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[n][pos.x + 1].DiceNumSelect(GameManager.Instance.BossNum);

                        }
                        if (pos.x - 1 >= 0)
                        {
                            MapController.Instance.dices[n - 1][pos.x - 1].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[n - 1][pos.x - 1].DiceNumSelect(GameManager.Instance.BossNum);
                            MapController.Instance.dices[n][pos.x - 1].transform.DOLocalMoveZ(0f, 0.1f);
                            MapController.Instance.dices[n][pos.x - 1].DiceNumSelect(GameManager.Instance.BossNum);

                        }
                        MapController.Instance.dices[n - 1][pos.x].transform.DOLocalMoveZ(0f, 0.1f);
                        MapController.Instance.dices[n - 1][pos.x].DiceNumSelect(GameManager.Instance.BossNum);
                    }
                });

            }
        }
        seq.AppendCallback(() =>
        {
            seq.Kill();
            CharacterAnimation.PlayAnimator("Idle");
            BoomMap.Instance.Boom();
            IsDashing = false;
        });
    }
}
