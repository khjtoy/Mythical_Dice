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
				if (MapController.Instance.dices[i][j].randoms == brokeNum)
				{
					MeshRenderer renderer = MapController.Instance.dices[i][j].GetComponent<MeshRenderer>();
					renderer.material.DOColor(Color.red, 0.4f).OnComplete(() =>
					{
						renderer.material.DOColor(Color.white, 0.3f).OnComplete(() =>
						{
							for (int i = 0; i < GameManager.Instance.Height; i++)
							{
								for (int j = 0; j < GameManager.Instance.Width; j++)
								{
									if (MapController.Instance.dices[i][j].randoms == brokeNum)
									{
										MapController.Instance.dices[i][j].transform.rotation = Quaternion.Euler(0, 0, 0);
										MapController.Instance.dices[i][j].isDiceDirecting = true;
										MapController.Instance.dices[i][j].StartCoroutine(MapController.Instance.dices[i][j].BasicDiceNumSelect());
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
