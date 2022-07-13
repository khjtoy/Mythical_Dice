using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSlider : MonoBehaviour
{
    [SerializeField] private Transform Fill;
    [Range(0f, 1f)]
    public float amount = 0;

    public void UpdateAmount(float value)
	{
        amount = value;
        Fill.transform.localScale = new Vector3(amount, 1, 1);
    }
}
