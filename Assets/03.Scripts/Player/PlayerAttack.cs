using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log("����");
    }

    private void AttackAction()
    {

    }

    private float PosToArray(float pos)
    {
        return (pos - (GameManager.Instance.Size / 2 * -1.5f)) / 1.5f;
    }
}
