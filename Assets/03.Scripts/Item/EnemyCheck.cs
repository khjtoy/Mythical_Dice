using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EnemyCheck : MonoBehaviour
{
    private ParticleSystem particleSystem;

    public int damage;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ENEMY"))
        {
            if(particleSystem.IsAlive())
            {
                Debug.Log($"Damage:{damage}");
                other.GetComponent<EnemyController>().OnHits(damage);
            }
        }
    }
}
