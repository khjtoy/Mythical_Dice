using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("Setting", LoadSceneMode.Additive);
    }
}
