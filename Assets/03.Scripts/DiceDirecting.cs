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

	public float speed = 5f;
<<<<<<< HEAD

	public int randoms;

=======
>>>>>>> origin/kdh
	void Update()
	{
		if (isDiceDirecting)
		{
			DiceObjet.transform.localRotation = Quaternion.Euler(DiceObjet.transform.localEulerAngles + new Vector3(0, 1, 1) * Time.deltaTime * speed);
		}
	}

	public void DiceNumSelect()
	{
<<<<<<< HEAD
		randoms = Random.Range(1, 6);

		DiceObjet.transform.localRotation = Quaternion.Euler(DiceRotationVector[randoms]);
=======
		int Randoms = Random.Range(1, 7);
		DiceObjet.transform.localRotation = Quaternion.Euler(DiceRotationVector[Randoms-1]);
>>>>>>> origin/kdh
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
