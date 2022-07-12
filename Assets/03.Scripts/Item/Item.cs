using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class Item : MonoBehaviour
{
    private EventParam eventParam;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            EventManager.TriggerEvent("RESETCHECK", eventParam);
            other.GetComponent<PlayerAttack>().isSkill = true;
            gameObject.SetActive(false);
        }
    }
}
