using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    private static Camera _mainCam = null;
    public static Camera MainCam
    {
        get
        {
            if (_mainCam == null)
                _mainCam = Camera.main;
            return _mainCam;
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

    private static CharacterMove _controller = null;

    public static CharacterMove Controller
	{
		get
		{
            if (_controller == null)
                _controller = GameObject.Find("Player").GetComponent<CharacterMove>();
            return _controller;
        }
	}
}
