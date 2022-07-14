using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static DefineCS;

[RequireComponent(typeof(Character))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float attackDelay;

    private float timer;

    private Character character;
    
    private PlayerController playerController;

    private Transform camera;

    public bool isSkill = false;

    private bool isStop = false;
    private bool isKill = false;

    private EventParam swordParam;

    private void Start()
    {
        character = GetComponent<Character>();
        playerController = GetComponent<PlayerController>();
        camera = Camera.main.transform;
        EventManager.StartListening("CHANGESTOP", ChangeStop);
    }

    private void ChangeStop(EventParam eventParam)
    {
        isStop = eventParam.boolParam;
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void CheckPos(GameObject enemy)
    {
        if (isStop || isKill) return;
        if (enemy.transform.localPosition.x > transform.localPosition.x)
            transform.localScale = new Vector3(1, 1, 1);
        else if (enemy.transform.localPosition.x < transform.localPosition.x)
            transform.localScale = new Vector3(-1, 1, 1);
            AttackAction(enemy, MapController.PosToArray(transform.localPosition.x), MapController.PosToArray(transform.localPosition.y));
        //}
    }

    private void AttackAction(GameObject enemyPos, int x, int y)
    {
        if (timer > 0) return;

        int difX = MapController.PosToArray(enemyPos.transform.localPosition.x) - MapController.PosToArray(transform.localPosition.x);
        int difY = MapController.PosToArray(enemyPos.transform.localPosition.y) - MapController.PosToArray(transform.localPosition.y);
        float add = Mathf.Abs(difX) + Mathf.Abs(difY);
        
        character.Animator.SetTrigger("Attack");
        if (isSkill)
        {
            swordParam.boolParam = false;
            StartCoroutine(Skill(playerController.playerDir));
            EventManager.TriggerEvent("CHANGESWORD", swordParam);
        }
        else
        {
            if (add == 1)
            {
                //Debug.Log($"X:{x}Y:{y}");

                //Debug.Log($"Damage");
                if (!enemyPos.GetComponent<EnemyMove>().IsFloating)
                {
                    GameObject paritcle = PoolManager.Instance.GetPooledObject((int)PooledObject.AttackParticle);
                    paritcle.transform.position = new Vector3(enemyPos.transform.position.x, enemyPos.transform.position.y + 0, enemyPos.transform.position.z);
                    paritcle.SetActive(true);
                    int damage = MapController.Instance.dices[y][x].randoms;
                    enemyPos.GetComponent<EnemyController>().OnHits(damage);
                    camera.DOShakePosition(0.7f, 0.1f);
                }
                timer = attackDelay;
            }
        }
    }

    private void ChangeTime()
    {
        Time.timeScale = 1;
        camera.DOShakePosition(0.7f, 0.1f);
    }

    private IEnumerator Skill(int dir)
    {
        int x = MapController.PosToArray(transform.localPosition.x);
        int y = MapController.PosToArray(transform.localPosition.y);
        int damage = MapController.Instance.dices[y][x].randoms;
        Debug.Log($"X:{x} Y:{y} Damage:{damage}");
        isKill = true;
        if (dir == 0)
        {
            for (int i = x + 1; i < GameManager.Instance.Width; i++)
            {
                SkillAction(i,y,damage);
                yield return new WaitForSeconds(0.2f);
            }
        }
        else if(dir == 1)
        {
            for(int i = x - 1; i >= 0; i--)
            {
                SkillAction(i,y,damage);
                yield return new WaitForSeconds(0.2f);
            }
        }
        else if(dir == 2)
        {
            for(int i = y + 1; i < GameManager.Instance.Height; i++)
            {
                SkillAction(x, i, damage);
                yield return new WaitForSeconds(0.2f);
            }
        }
        else if(dir == 3)
        {
            for (int i = y - 1; i >= 0; i--)
            {
                SkillAction(x, i, damage);
                yield return new WaitForSeconds(0.2f);
            }
        }

        isSkill = false;
        isKill = false;
        yield return null;
    }

    private void SkillAction(int x, int y, int damage)
    {
        Vector3 skillPos = MapController.ArrayToPos(x, y);
        skillPos.y += 1f;
        //Debug.Log($"POS{skillPos}");
        GameObject effect = PoolManager.Instance.GetPooledObject((int)PooledObject.SkillEffect);
        effect.transform.localPosition = skillPos;
        effect.GetComponent<EnemyCheck>().damage = damage * 2; 
        effect.SetActive(true);
    }

	public void OnDestroy()
	{
        EventManager.StopListening("CHANGESTOP", ChangeStop);
    }
}
