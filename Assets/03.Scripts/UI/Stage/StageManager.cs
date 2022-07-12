using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum StageName
{
    InGame = 1
}
public class StageManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> stageList = new List<GameObject>();

    private int currentStage;

    private void Awake()
    {
        PlayerPrefs.SetInt("STAGE", 2);
        currentStage = PlayerPrefs.GetInt("STAGE");
    }

    private void Start()
    {
        foreach (GameObject gb in stageList)
        {
            gb.SetActive(false);
        }
        InitStage();
    }

    private void InitStage()
    {
        
        for(int i = 0;i<currentStage;i++)
        {
            stageList[i].SetActive(true);
        }

    }

    public void Stage(string name)
    {
        SceneManager.LoadScene(name);
    }
}
