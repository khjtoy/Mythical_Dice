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

	public bool isDual;
	public bool XAxis;
	public bool isDown;
	public bool isLeft;

	private void Awake()
	{
		min = new Vector2(size / 2, size / 2) * -1.5f;
		map = new GameObject[height][];
	}
	private void Start()
	{
		if (root.childCount == 1)
			SpawnMap();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			FloorDirect();
		}
	}

	private void SpawnMap()
	{
		for (int y = 0; y < height; y++)
		{
			map[y] = new GameObject[width];
			for (int x = 0; x < width; x++)
			{
				map[y][x] = Instantiate(dicePrefabs, new Vector3(0, 0, 0), Quaternion.identity);
				map[y][x].transform.SetParent(root);
				map[y][x].transform.localPosition = new Vector3(min.x + (1.5f * x), min.y + (1.5f * y), 0);
				map[y][x].transform.localRotation = Quaternion.Euler(180, 0, 0);
				map[y][x].transform.localScale = new Vector3(1, 1, 1);
			}
		}
	}

	private void FloorDirect(int x = 0, int y = 0, bool isfirst = false)
	{
		if (isDown && !isfirst)
		{
			y = height - 1;
		}
		if (isLeft && !isfirst)
		{
			x = width - 1;
		}

		if (y < 0 || y >= height || x < 0 || x >= width && isDual)
		{
			return;
		}

		if (!isDual)
		{
			y = x < 0 ? y + 1 : y;
			x = x < 0 ? 0 : x;
			x = y < 0 ? x + 1 : x;
			y = y < 0 ? 0 : y;

			y = x >= width ? y + 1 : y;
			x = x >= width ? width - 1 : x;
			x = y >= height ? x + 1 : x;
			y = y >= height ? height - 1 : y;
		}

		map[y][x].transform.GetChild(2).localRotation = Quaternion.Euler(0,0,0);
		map[y][x].transform.GetChild(2).GetComponent<DiceDirecting>().isDiceDirecting = true;


		isfirst = true;
		StartCoroutine(WaitFloor(x, y, isfirst));
	}

	private IEnumerator WaitFloor(int x, int y, bool isfirst)
	{
		yield return new WaitForSeconds(1f);

		map[y][x].transform.GetChild(2).GetComponent<DiceDirecting>().DiceNumSelect();

		if (!isDual)
		{
			if (XAxis)
				x += isLeft == true ? -1 : 1;
			else
				y += isDown == true ? -1 : 1;
			FloorDirect(x, y, isfirst);
		}
		else
		{
			if (isLeft)
				FloorDirect(x - 1, y, isfirst);
			else
				FloorDirect(x + 1, y, isfirst);

			if (isDown)
				FloorDirect(x, y - 1, isfirst);
			else
				FloorDirect(x, y + 1, isfirst);
		}
	}
}
