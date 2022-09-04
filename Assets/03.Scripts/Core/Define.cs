using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    private static Camera _mainCam = null;
    private static Vector3 originCam;
    public static Camera MainCam
    {
        get
        {
            if (_mainCam == null)
                _mainCam = Camera.main;
            return _mainCam;
        }
    }

    public static Vector3 OrginCam
    {
        get
        {
            return originCam;
        }
        set
        {
            originCam = value;
        }
    }

    private static CharacterMove _player = null;
    public static Vector2 Player
    {
        get
        {
            if (_player == null)
                _player = GameObject.Find("Player").GetComponent<CharacterMove>();
            return _player.PlayerPos;
        }
    }

    public static GameObject PlayerObj
    {
        get
        {
            if (_player == null)
                _player = GameObject.Find("Player").GetComponent<CharacterMove>();
            return _player.gameObject;
        }
    }

    private static PlayerController _controller = null;

    public static PlayerController Controller
	{
		get
		{
            if (_controller == null)
                _controller = GameObject.Find("Player").GetComponent<PlayerController>();
            return _controller;
        }
	}
}
