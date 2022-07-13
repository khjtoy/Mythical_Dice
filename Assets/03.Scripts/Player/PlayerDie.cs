using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDie : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer fade;

    private Character character;

    private bool isDie;

    private void Start()
    {
        character = GetComponent<Character>();
        isDie = false;
    }

    public void DieAction()
    {
        if (!isDie)
        {
            isDie = true;
            character.Animator.SetTrigger("Hit");
            fade.DOFade(1, 1.3f).OnComplete(() =>
            {
                character.Animator.SetTrigger("Die");
            });
        }
    }
        
}
