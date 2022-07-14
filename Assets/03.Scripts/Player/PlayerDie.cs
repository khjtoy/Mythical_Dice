using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
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
            DOTween.KillAll(true);
            Define.MainCam.transform.position = new Vector3(Define.MainCam.transform.position.x, Define.MainCam.transform.position.y, -10);
            isDie = true;
            character.Animator.SetBool("IsDie", true);
            character.Animator.SetTrigger("Hit");
            fade.DOFade(1, 1.3f).OnComplete(() =>
            {
                character.Animator.SetTrigger("Die");
            });
        }
    }

    public void DieScene()
    {
        SceneManager.LoadScene("GamePlay 6");
    }
        
}
