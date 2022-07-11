using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FX_AutoDelete : MonoBehaviour
{
    [SerializeField]
    private bool OnlyDeactivate;
    private void OnEnable()
    {
        StartCoroutine("CheckIfAlive");
    }

    IEnumerator CheckIfAlive()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            if(!GetComponent<ParticleSystem>().IsAlive(true))
            {
                if (OnlyDeactivate)
                    gameObject.SetActive(false);
                else
                    Destroy(gameObject);

                break;
            }
        }
    }
}
