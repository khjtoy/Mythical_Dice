using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterMove : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 0.1f; // 움직일 시간
    private float inverseMoveTime; //  움직임을 효율적으로 만드는데 사용

    private Character character;

    public bool IsMove { get; private set; }
<<<<<<< HEAD
=======
    public Vector2 PlayerPos = Vector2.zero;
>>>>>>> origin/csh

    private void Start()
    {
        character = GetComponent<Character>();
        // 역수 변환
        inverseMoveTime = 1f / moveTime;
<<<<<<< HEAD
    }

    public void CharacterMovement(Vector2 target)
    {
=======
        PlayerPos = transform.localPosition;
    }

    public virtual void CharacterMovement(Vector2 target)
    {
        if (IsMove) return;
>>>>>>> origin/csh
        IsMove = true;

        target.x -= transform.localPosition.x;
        target.y -= transform.localPosition.y;

        Vector3 targetPos = transform.localPosition + new Vector3(target.x, target.y, 0);
<<<<<<< HEAD
=======
        Debug.Log(target);
>>>>>>> origin/csh
        StartCoroutine(DoMove(targetPos));
    }

    private IEnumerator DoMove(Vector3 targetPos)
    {
        Vector3 newPos = Vector3.zero;
        float sqrRemainingDistance = (transform.localPosition - targetPos).sqrMagnitude;

<<<<<<< HEAD
=======
        Debug.Log($"{transform.localPosition.z} == {targetPos.z}");
>>>>>>> origin/csh
        //극한(거의 0)보다 큰 동안
        while(sqrRemainingDistance > double.Epsilon)
        {
            newPos = Vector3.MoveTowards(transform.localPosition, targetPos, inverseMoveTime * Time.deltaTime);
            transform.localPosition = newPos;

            // 이동 후 남은 거리를 재계산
            sqrRemainingDistance = (transform.localPosition - targetPos).sqrMagnitude;
            yield return null;
        }

        //캐릭터 좌표 -> 타켓 좌표
        transform.localPosition = targetPos;
<<<<<<< HEAD
=======
        PlayerPos = transform.localPosition;
>>>>>>> origin/csh
        IsMove = false;
    }
}
