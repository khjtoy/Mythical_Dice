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
    public Vector2Int PlayerPos = Vector2Int.zero;

    private void Start()
    {
        character = GetComponent<Character>();
        // ���� ��ȯ
        inverseMoveTime = 1f / moveTime;
    }


    public virtual void CharacterMovement(Vector2 target)
    {
        if (IsMove) return;
        IsMove = true;

        PlayerPos = new Vector2Int(Mathf.RoundToInt(transform.localPosition.x), Mathf.RoundToInt(transform.localPosition.y));
        target.x -= transform.localPosition.x;
        target.y -= transform.localPosition.y;

        Vector3 targetPos = transform.localPosition + new Vector3(target.x, target.y, 0);

        if (targetPos.x < transform.localPosition.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (targetPos.x > transform.localPosition.x)
            transform.localScale = new Vector3(1, 1, 1);

        Debug.Log(target);
        StartCoroutine(DoMove(targetPos));
    }

    private IEnumerator DoMove(Vector3 targetPos)
    {
        Vector3 newPos = Vector3.zero;
        float sqrRemainingDistance = (transform.localPosition - targetPos).sqrMagnitude;


        character.Animator.SetTrigger("Move");
        //����(���� 0)���� ū ����
        while (sqrRemainingDistance > double.Epsilon)
        {
            newPos = Vector3.MoveTowards(transform.localPosition, targetPos, inverseMoveTime * Time.deltaTime);
            transform.localPosition = newPos;

            // �̵� �� ���� �Ÿ��� ����
            sqrRemainingDistance = (transform.localPosition - targetPos).sqrMagnitude;
            yield return null;
        }

        //ĳ���� ��ǥ -> Ÿ�� ��ǥ
        transform.localPosition = targetPos;
        PlayerPos = new Vector2Int(Mathf.RoundToInt(transform.localPosition.x), Mathf.RoundToInt(transform.localPosition.y));
        IsMove = false;
    }
}
