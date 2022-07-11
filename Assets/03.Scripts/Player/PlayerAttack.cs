using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float attackDelay;

    private float timer;
    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
            // 1.�����¿� üũ(���� 1���̴� ��ġ�� üũ?)
            // 2.������ ��� �ؿ� �� ��������
            // 3. �ش� �� ��ŭ ������
    }

    public void CheckPos(GameObject enemy)
    {
        int difX = MapController.PosToArray(enemy.transform.localPosition.x) - MapController.PosToArray(transform.localPosition.x);
        int difY = MapController.PosToArray(enemy.transform.localPosition.y) - MapController.PosToArray(transform.localPosition.y);
        float add = Mathf.Abs(difX) + Mathf.Abs(difY);

        if (add == 1)
        {
            Debug.Log("����");
            AttackAction(enemy.transform, MapController.PosToArray(transform.localPosition.x), MapController.PosToArray(transform.localPosition.y));
        }
    }

    private void AttackAction(Transform enemyPos, int x, int y)
    {
        if (timer > 0) return;

        GameObject paritcle = PoolManager.Instance.GetPooledObject((int)PooledObject.AttackParticle);
        paritcle.transform.localPosition = new Vector3(enemyPos.localPosition.x, enemyPos.localPosition.y + 0.5f, -2);
        paritcle.SetActive(true);
        Debug.Log($"X:{x}Y:{y}");
        int damage = MapController.Instance.GetIndexCost(x, y);
        Debug.Log($"Damage {damage}");

        timer = attackDelay;
    }
}
