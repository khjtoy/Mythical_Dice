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


    private void Awake()
    {
        min = new Vector2(size / 2, size / 2) * -1.5f;
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
            for (int x = 0; x < width; x++)
            {
                GameObject dice = Instantiate(dicePrefabs, new Vector3(0,0,0), Quaternion.identity);
                dice.transform.SetParent(root);
                dice.transform.localPosition = new Vector3(min.x+ (1.5f * x), min.y + (1.5f * y), 0);
                dice.transform.localRotation = Quaternion.Euler(180, 0, 0);
                dice.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
