using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSlider : MonoBehaviour
{
    [SerializeField] private Transform Fill;
    [Range(0f, 1f)]
    public float amount = 0;

    private void Update()
    {
        Fill.transform.localScale = new Vector3(amount, 1, 1);
    }
}
