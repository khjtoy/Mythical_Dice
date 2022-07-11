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

    public bool IsMove { get; private set; }

    private void Start()
    {
        character = GetComponent<Character>();
        // ���� ��ȯ
        inverseMoveTime = 1f / moveTime;
    }

    public void CharacterMovement(Vector2 target)
    {
        IsMove = true;

        target.x -= transform.localPosition.x;
        target.y -= transform.localPosition.y;

        Vector3 targetPos = transform.localPosition + new Vector3(target.x, target.y, 0);
        StartCoroutine(DoMove(targetPos));
    }

    private IEnumerator DoMove(Vector3 targetPos)
    {
        Vector3 newPos = Vector3.zero;
        float sqrRemainingDistance = (transform.localPosition - targetPos).sqrMagnitude;

        //����(���� 0)���� ū ����
        while(sqrRemainingDistance > double.Epsilon)
        {
            newPos = Vector3.MoveTowards(transform.localPosition, targetPos, inverseMoveTime * Time.deltaTime);
            transform.localPosition = newPos;

            // �̵� �� ���� �Ÿ��� ����
            sqrRemainingDistance = (transform.localPosition - targetPos).sqrMagnitude;
            yield return null;
        }

        //ĳ���� ��ǥ -> Ÿ�� ��ǥ
        transform.localPosition = targetPos;
        IsMove = false;
    }
}
