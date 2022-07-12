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

    private Transform camera;

    private void Start()
    {
        character = GetComponent<Character>();
        camera = Camera.main.transform;
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
            // 1.상하좌우 체크(몬스터 1명이니 위치로 체크?)
            // 2.범위에 들면 밑에 값 가져오기
            // 3. 해당 값 만큼 데미지
    }

    public void CheckPos(GameObject enemy)
    {
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
        if (add == 1)
        {
            Debug.Log($"X:{x}Y:{y}");

            Debug.Log($"Damage");
            if (!enemyPos.GetComponent<StatueMove>().IsFoating)
            {
                GameObject paritcle = PoolManager.Instance.GetPooledObject((int)PooledObject.AttackParticle);
                paritcle.transform.position = new Vector3(enemyPos.transform.position.x, enemyPos.transform.position.y + 1f, enemyPos.transform.position.z);
                paritcle.SetActive(true);
                int damage = MapController.Instance.GetIndexCost(x, y);
                enemyPos.GetComponent<EnemyController>().OnHits(damage);
                camera.DOShakePosition(0.7f, 0.1f);
            }
            timer = attackDelay;
        }
    }

    private void ChangeTime()
    {
        Time.timeScale = 1;
        camera.DOShakePosition(0.7f, 0.1f);
    }
}
