using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class NumText : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro num;
    public void DamageText(int text, Vector3 pos)
    {
        transform.position = new Vector3(pos.x + 1f, pos.y + 1f, pos.z);
        num.text = string.Format(text.ToString());
        num.DOFade(0, 1f).OnComplete(() =>
        {
            PoolManager.Instance.Despawn(this.gameObject);
        });
    }
}
