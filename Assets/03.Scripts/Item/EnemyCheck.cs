using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(ParticleSystem))]
public class EnemyCheck : MonoBehaviour
{
    private ParticleSystem particleSystem;

    public int damage;

    private Transform camera;

    private EventParam eventParam;

    private void OnEnable()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        camera = Camera.main.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ENEMY"))
        {
            if(particleSystem.IsAlive() && !other.GetComponent<StatueMove>().IsFloating)
            {
                Debug.Log($"Damage:{damage}");
                eventParam.boolParam = true;
                EventManager.TriggerEvent("CHANGESTOP", eventParam);
                other.GetComponent<EnemyController>().OnHits(damage);
                camera.DOShakePosition(0.7f, 0.1f);
                transform.DOScale(1.5f, 0.2f);
                Time.timeScale = 0.1f;
                Invoke("OriginTime", 0.06f);
            }
        }
    }

    private void OriginTime()
    {
        Time.timeScale = 1;
        eventParam.boolParam = false;
        EventManager.TriggerEvent("CHANGESTOP", eventParam);
    }
}
