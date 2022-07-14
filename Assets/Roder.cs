using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Roder : MonoBehaviour
{
    void Start()
    {
		SceneManager.LoadScene(6, LoadSceneMode.Additive);
	}
}
