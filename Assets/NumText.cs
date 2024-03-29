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


	[SerializeField]
	private float Power;
	[SerializeField]
	private float PowerSpeed;

	[SerializeField]
	private float Down;
	[SerializeField]
	private float DownSpeed;

	[SerializeField]
	private float HoriSpeed;

	public void DamageText(int text, Vector3 pos)
	{
		num.alpha = 1;
		Vector2 vec = Random.insideUnitCircle;
		transform.position = new Vector3(Random.Range(pos.x - 1f, pos.x + 1f), Random.Range(pos.y, pos.y + 1f), pos.z);
		//transform.localEulerAngles = new Vector3(transform.rotation.x - 45, transform.rotation.y, transform.rotation.z);
		num.text = string.Format(text.ToString());
		Sequence mySequence = DOTween.Sequence();
		mySequence.Append(transform.DOMoveX(vec.x, HoriSpeed).SetEase(Ease.Linear));
		mySequence.Join(transform.DOMoveY(Mathf.Abs(transform.position.y) + Power, PowerSpeed).SetEase(Ease.Linear)).AppendCallback(() => { num.DOFade(0, 0.35f);
		});
		mySequence.Append(transform.DOMoveY(Down, DownSpeed).SetEase(Ease.Linear)).AppendCallback(()=> { PoolManager.Instance.Despawn(this.gameObject); });
	}

}
