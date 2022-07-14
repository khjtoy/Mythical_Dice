using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
	public bool IsInGame = false;
	private GameObject InGameCanvas;
	private GameObject PausePanel;
	private GameObject OptionPanel;
	private int uiOpen = 1;	
	private int uiClose = -1;
    protected override void Init()
    {
        base.Init();
		InGameCanvas = GameObject.Find("InGameCanvas");
		DontDestroyOnLoad(InGameCanvas);
		PausePanel = InGameCanvas.transform.GetChild(1).gameObject;
		OptionPanel = InGameCanvas.transform.GetChild(2).gameObject;
		InGameCanvas.SetActive(false);
	}

    private void Update()
	{
		if (IsInGame)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				TogglePanel(InGameCanvas);
				PausePanel.SetActive(true);
				OptionPanel.SetActive(false);
			}
		}
        else
        {
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				TogglePanel(InGameCanvas);
				PausePanel.SetActive(false);
				OptionPanel.SetActive(true);
			}
		}
	}

	public void TogglePanel(GameObject panel)
	{
		panel.SetActive(!panel.activeInHierarchy);
		uiOpen++;
		Time.timeScale = (uiOpen %= 2);
	}

	public void StageSelect()
    {
		SceneManager.LoadScene("Stage");
		DOTween.KillAll();
		Time.timeScale = 1;
		IsInGame = false;
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
