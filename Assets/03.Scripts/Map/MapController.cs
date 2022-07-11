using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private GameObject dicePrefabs;
    [SerializeField]
    private Transform root;


    [SerializeField]
    private float distance;


    private Vector2 min;

    private GameObject[][] map;

    private GameManager gameManager;


    private void Awake()
    {
        gameManager = GameManager.Instance;

        min = new Vector2(GameManager.Instance.Size / 2, GameManager.Instance.Size / 2) * -1.5f;
        map = new GameObject[gameManager.Height][];
    }
    private void Start()
    {
        if(root.childCount == 1)
            SpawnMap();
    }

    private void SpawnMap()
    {
        for (int y = 0; y < gameManager.Height; y++)
        {
            map[y] = new GameObject[gameManager.Width];
            for (int x = 0; x < gameManager.Width; x++)
            {
                map[y][x] = Instantiate(dicePrefabs, new Vector3(0,0,0), Quaternion.identity);
                map[y][x].transform.SetParent(root);
                map[y][x].transform.localPosition = new Vector3(min.x+ (1.5f * x), min.y + (1.5f * y), 0);
                map[y][x].transform.localRotation = Quaternion.Euler(180, 0, 0);
                map[y][x].transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
