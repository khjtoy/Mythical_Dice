using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoomMap : MonoSingleton<BoomMap>
{
	[Header("몇초 후 부서짐")]
	public float wait;

	public int brokeNum;

	public void Boom()
	{
		for (int i = 0; i < GameManager.Instance.Height; i++)
		{
			for (int j = 0; j < GameManager.Instance.Width; j++)
			{
				if (MapController.Instance.MAP[i][j].transform.GetChild(2).GetComponent<DiceDirecting>().thisNum == brokeNum)
				{
					MeshRenderer renderer = MapController.Instance.MAP[i][j].transform.GetChild(2).GetComponent<MeshRenderer>();
					renderer.material.DOColor(Color.red, 0.5f).OnComplete(() =>
					{
						renderer.material.DOColor(Color.white, 0.5f).OnComplete(() =>
						{
							for (int i = 0; i < GameManager.Instance.Height; i++)
							{
								for (int j = 0; j < GameManager.Instance.Width; j++)
								{
									if (MapController.Instance.MAP[i][j].transform.GetChild(2).GetComponent<DiceDirecting>().thisNum == brokeNum)
									{
										MapController.Instance.MAP[i][j].transform.GetChild(2).GetComponent<DiceDirecting>().isDiceDirecting = true;
										MapController.Instance.MAP[i][j].transform.GetChild(2).GetComponent<DiceDirecting>().StartCoroutine(MapController.Instance.MAP[i][j].transform.GetChild(2).GetComponent<DiceDirecting>().BasicDiceNumSelect());
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
