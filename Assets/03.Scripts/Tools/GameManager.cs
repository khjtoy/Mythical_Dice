using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("�� ũ�� ����")]
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private int size;

    private int _bossNum = 0;
    [field: SerializeField]
    public int BossNum { get; set; }

    public int StageNum;
    public int Width
    {
        get
        {
            return width;
        }
    }
    public int Height
    {
        get
        {
            return height;
        }
    }

    public int Size

    {
        get
        {
            return size;
        }
    }
}
