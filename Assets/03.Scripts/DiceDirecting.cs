using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiceDirecting : MonoBehaviour
{
	public ParticleSystem[] diceParticel;

	public Vector3[] DiceRotationVector;

	public GameObject DiceObjet;

	public bool isDiceDirecting = false;

	[HideInInspector]
	public int thisNum;

	public float speed = 5f;

	[Header("기다리는 기본 시간")]
	public float wait;
	void Update()
	{
		if (isDiceDirecting)
		{
			DiceObjet.transform.localRotation = Quaternion.Euler(DiceObjet.transform.localEulerAngles + new Vector3(0, 1, 1) * Time.deltaTime * speed);
		}
	}

	public void DiceNumSelect()
	{
		int Randoms = Random.Range(1, 7);
		thisNum = Randoms;
		DiceObjet.transform.localRotation = Quaternion.Euler(DiceRotationVector[Randoms-1]);
		isDiceDirecting = false;
		for (int i = 0; i < diceParticel.Length; i++)
		{
			diceParticel[i].Clear();
		}
		for (int i = 0; i < diceParticel.Length; i++)
		{
			diceParticel[i].Play();
		}
	}

	public IEnumerator BasicDiceNumSelect()
	{
		yield return new WaitForSeconds(wait);
		int Randoms = Random.Range(1, 7);
		thisNum = Randoms;
		DiceObjet.transform.localRotation = Quaternion.Euler(DiceRotationVector[Randoms - 1]);
		isDiceDirecting = false;
		for (int i = 0; i < diceParticel.Length; i++)
		{
			diceParticel[i].Clear();
		}
		for (int i = 0; i < diceParticel.Length; i++)
		{
			diceParticel[i].Play();
		}
	}
}
