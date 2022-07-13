using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    private PlayableDirector _director;

    private void Awake()
    {
        _director = GetComponent<PlayableDirector>();
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene("Start");
    }
    public void StopTimeLine()
    {
        _director.Pause();
    }
    public void PlayTimeLine()
    {
        _director.Play();
    }
}
