using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoomMap : MonoSingleton<BoomMap>
{
	[Header("몇초 후 부서짐")]
	public float wait;


	public void Boom()
	{
		int brokeNum = GameManager.Instance.BossNum;
		for (int i = 0; i < GameManager.Instance.Height; i++)
		{
			for (int j = 0; j < GameManager.Instance.Width; j++)
			{
				if (MapController.Instance.dices[j][i].randoms == brokeNum)
				{
					MeshRenderer renderer = MapController.Instance.dices[j][i].GetComponent<MeshRenderer>();
					renderer.material.DOColor(Color.red, 0.5f).OnComplete(() =>
					{
						renderer.material.DOColor(Color.white, 0.5f).OnComplete(() =>
						{
							for (int i = 0; i < GameManager.Instance.Height; i++)
							{
								for (int j = 0; j < GameManager.Instance.Width; j++)
								{
									if (MapController.Instance.dices[j][i].thisNum == brokeNum)
									{
										MapController.Instance.dices[j][i].isDiceDirecting = true;
										MapController.Instance.dices[j][i].StartCoroutine(MapController.Instance.dices[j][i].BasicDiceNumSelect());
									}
								}
							}
						});
					});
				}
			}
		}
	}
}
