using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class PlayerAttack : MonoBehaviour
{
    private void Update()
    {
            // 1.�����¿� üũ(���� 1���̴� ��ġ�� üũ?)
            // 2.������ ��� �ؿ� �� ��������
            // 3. �ش� �� ��ŭ ������
    }

    public void CheckPos(GameObject enemy)
    {
        float difX = PosToArray(enemy.transform.localPosition.x) - PosToArray(transform.localPosition.x);
        float difY = PosToArray(enemy.transform.localPosition.y) - PosToArray(transform.localPosition.y);
        float add = Mathf.Abs(difX) + Mathf.Abs(difY);

        if (add == 1)
            AttackAction(enemy.transform);
    }

    private void AttackAction(Transform enemyPos)
    {
        GameObject paritcle = PoolManager.Instance.GetPooledObject((int)PooledObject.AttackParticle);
        paritcle.transform.localPosition = new Vector3(enemyPos.localPosition.x, enemyPos.localPosition.y + 0.5f, -2);
        paritcle.SetActive(true);
    }

    private float PosToArray(float pos)
    {
        return (pos - (GameManager.Instance.Size / 2 * -1.5f)) / 1.5f;
    }
}
