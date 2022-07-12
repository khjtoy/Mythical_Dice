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

	[Header("��ٸ���?�⺻ �ð�")]
	public float wait;

	public int randoms;

	void Update()
	{
		if (isDiceDirecting)
		{
			DiceObjet.transform.localRotation = Quaternion.Euler(DiceObjet.transform.localEulerAngles + new Vector3(0, 1, 1) * Time.deltaTime * speed);
		}
	}

	public void DiceNumSelect()
	{
		randoms = Random.Range(1, 7);
		DiceObjet.transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms-1]);
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

	public void DiceNumSelect(int value)
	{
		randoms = value;
		DiceObjet.transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
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
		randoms = Random.Range(1, 7);
		thisNum = randoms;
		DiceObjet.transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms - 1]);
		isDiceDirecting = false;
		for (int i = 0; i < diceParticel.Length; i++)
		{
			diceParticel[i].Clear();
		}
		for (int i = 0; i < diceParticel.Length; i++)
		{
			diceParticel[i].Play();
		}

		//if(MapController.PosToArray(this.transform.position.y) == MapController.PosToArray(Define.Player.y))
		//{
		//	Define.Controller.gameObject.GetComponent<OnHit>().OnHits(thisNum);
		//}
	}
}
