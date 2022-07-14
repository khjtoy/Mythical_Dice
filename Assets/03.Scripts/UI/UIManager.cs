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

    protected override void Init()
    {
        
    }
    private void Awake()
	{
		InGameCanvas = GameObject.Find("InGameCanvas");
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
    }

	public void QuitGame()
	{
		Application.Quit();
	}
}
