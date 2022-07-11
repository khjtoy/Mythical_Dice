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

    private void Start()
    {
        character = GetComponent<Character>();
        // 역수 변환
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

        //극한(거의 0)보다 큰 동안
        while(sqrRemainingDistance > double.Epsilon)
        {
            newPos = Vector3.MoveTowards(character.Rigidbody.position, targetPos, inverseMoveTime * Time.deltaTime);
            character.Rigidbody.MovePosition(newPos);

            // 이동 후 남은 거리를 재계산
            sqrRemainingDistance = (transform.position - targetPos).sqrMagnitude;
            yield return null;
        }

        //캐릭터 좌표 -> 타켓 좌표
        character.Rigidbody.MovePosition(targetPos);
    }
}
