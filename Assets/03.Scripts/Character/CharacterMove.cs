using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterMove : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 0.1f; // ������ �ð�
    private float inverseMoveTime; //  �������� ȿ�������� ����µ� ���

    private Character character;

    private void Start()
    {
        character = GetComponent<Character>();
        // ���� ��ȯ
        inverseMoveTime = 1f / moveTime;
    }

    public void CharacterMovement(Vector3 target)
    {
        target.x -= character.Rigidbody.position.x;
        target.y -= character.Rigidbody.position.y;

        Vector3 targetPos = character.Rigidbody.transform.localPosition + new Vector3(target.x, target.y, 0);
        Debug.Log(targetPos);
        StartCoroutine(DoMove(targetPos));
    }

    private IEnumerator DoMove(Vector3 targetPos)
    {
        Vector3 newPos = Vector3.zero;
        float sqrRemainingDistance = (transform.position - targetPos).sqrMagnitude;

        //����(���� 0)���� ū ����
        while(sqrRemainingDistance > double.Epsilon)
        {
            newPos = Vector3.MoveTowards(character.Rigidbody.position, targetPos, inverseMoveTime * Time.deltaTime);
            character.Rigidbody.MovePosition(newPos);

            // �̵� �� ���� �Ÿ��� ����
            sqrRemainingDistance = (transform.position - targetPos).sqrMagnitude;
            yield return null;
        }

        //ĳ���� ��ǥ -> Ÿ�� ��ǥ
        character.Rigidbody.MovePosition(targetPos);
    }
}
