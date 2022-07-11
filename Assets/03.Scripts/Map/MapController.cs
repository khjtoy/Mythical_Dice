using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private GameObject dicePrefabs;
    [SerializeField]
    private Transform root;

    [Header("크기 지정 변수")]
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;

    [SerializeField]
    private float distance;

    [SerializeField]
    private int size;

    private Vector2 min;

    private GameObject[][] map;


    private void Awake()
    {
        min = new Vector2(size / 2, size / 2) * -1.5f;
        map = new GameObject[height][];
    }
    private void Start()
    {
        if(root.childCount == 1)
            SpawnMap();
    }

    private void SpawnMap()
    {
        for (int y = 0; y < height; y++)
        {
            map[y] = new GameObject[width];
            for (int x = 0; x < width; x++)
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
