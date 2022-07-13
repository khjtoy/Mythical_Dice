using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class Item : MonoBehaviour
{
    private EventParam eventParam;
    private EventParam swordParam;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            swordParam.boolParam = true;
            EventManager.TriggerEvent("RESETCHECK", eventParam);
            EventManager.TriggerEvent("CHANGESWORD", swordParam);
            other.GetComponent<PlayerAttack>().isSkill = true;
            gameObject.SetActive(false);
        }
    }
}
