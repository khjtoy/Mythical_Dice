using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class MinoStamp : EnemyMove, IEnemyAttack
{
    public override bool IsFloating { get; set; } = false;
    public bool IsAttacking { get; set; } = false;
    public Animator animator { get; set; } = null;

    private Sequence seq;

    private GameObject dice;
    private Animator diceAni;
    private SetNumber setNumber;

    public bool isCheck = false;

    private void Start()
    {
        EventManager.StartListening("RESETCHECK", OffCheck);
        dice = GameObject.FindGameObjectWithTag("Dice");
        diceAni = dice.GetComponent<Animator>();
        setNumber = dice.transform.GetChild(0).GetComponent<SetNumber>();
        setNumber.gameObject.SetActive(false);
    }

    private void OffCheck(EventParam eventParam)
    {
        isCheck = false;
    }

    public override void CharacterMovement(Vector2 target)
    {
        IsFloating = true;
        diceAni.SetBool("IsDice", true);
        setNumber.gameObject.SetActive(false);
        setNumber.isSurple = true;
        int x = MapController.PosToArray(target.x);
        int y = MapController.PosToArray(target.y);
        StartCoroutine(ChangeDice(x, y));

        seq = DOTween.Sequence();
        CharacterAnimation.PlayAnimator(animator, "jump");
        seq.Append(transform.DOLocalMoveZ(-3, 0.3f));
        seq.Append(transform.DOLocalMove(new Vector3(target.x, target.y, -3), 0.3f));
        seq.Append(transform.DOLocalMoveZ(-1, 0.1f).SetEase(Ease.InExpo));
        seq.AppendCallback(() => CharacterAnimation.SetTriggerAnimator(animator,"stamp"));

        DoAttack();
    }

    private IEnumerator ChangeDice(int x, int y)
    {
        yield return new WaitForSeconds(0.15f);
        diceAni.SetBool("IsDice", false);
        setNumber.SettingNumber(3 - 1);
        setNumber.gameObject.SetActive(true);
    }

    private void Awake()
    {
        EventManager.StartListening("KILLENEMY", KillEnemy);
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
    public void KillEnemy(EventParam eventParam)
    {
        seq.Kill();
        seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveZ(-1, 0.1f).SetEase(Ease.InExpo));
    }
    
    private IEnumerator StampCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        CharacterAnimation.PlayAnimator(animator, "Idle");
        Vector2Int pos = new Vector2Int(MapController.PosToArray(transform.localPosition.x), MapController.PosToArray(transform.localPosition.y));
        GameManager.Instance.BossNum = 3;
        SoundManager.Instance.SetEnemyEffectClip(EnemyEffectEnum.MINOSTAMP);
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
    private void OnDestroy()
    {
        EventManager.StopListening("KILLENEMY", KillEnemy);
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("KILLENEMY", KillEnemy);
    }

    public void DoAttack()
    {
        IsAttacking = true;
        seq.AppendCallback(() =>
        {
            Define.MainCam.orthographic = !Define.MainCam.orthographic;
            Define.MainCam.transform.DOShakePosition(0.3f);
            StartCoroutine(StampCoroutine());
            seq.Kill();
            IsFloating = false;
            IsAttacking = false;

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
        });
    }
}
