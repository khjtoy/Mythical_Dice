using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private EventParam eventParam;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            EventManager.TriggerEvent("RESETCHECK", eventParam);

            PlayerController playerController = other.GetComponent<PlayerController>();

           // StartCoroutine(Skill(playerController.playerDir));
        }
    }
}
