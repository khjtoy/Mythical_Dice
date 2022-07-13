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
		Vector2 vec = Random.insideUnitCircle;
		transform.position = new Vector3(Random.Range(pos.x - 1f, pos.x + 1f), Random.Range(pos.y, pos.y + 1f), pos.z);
		transform.localEulerAngles = new Vector3(transform.rotation.x - 45, transform.rotation.y, transform.rotation.z);
		num.text = string.Format(text.ToString());
		transform.DOMove(new Vector3(vec.x, pos.y + 2f, pos.z), 0.5f).SetEase(Ease.Linear).OnComplete(() =>
		 {
			 num.DOFade(0, 0.5f);
			 transform.DOMove(new Vector3(vec.x, pos.y - 2f, pos.z), 0.5f).SetEase(Ease.Linear).OnComplete(() => { PoolManager.Instance.Despawn(this.gameObject); });
		 });
	}
}
