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