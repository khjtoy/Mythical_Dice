using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayer : Character
{
    [SerializeField]
    private GameObject[] enemys;

    private void Update()
    {
        foreach(GameObject enemy in enemys)
        {
            if(enemy.transform.position.y < transform.position.y)
            {
                SpriteRenderer.sortingOrder = 0;
                break;
            }
            SpriteRenderer.sortingOrder = 1;
        }
    }

    public void SetEnemys()
    {
        enemys = GameObject.FindGameObjectsWithTag("ENEMY");
    }
}
