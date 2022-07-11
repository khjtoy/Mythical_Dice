using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool IsInGame = false;
    private GameObject InGameCanvas;
    private GameObject PausePanel;
    private GameObject OptionPanel;

    private void Awake()
    {
        InGameCanvas = GameObject.Find("InGameCanvas");
        PausePanel = InGameCanvas.transform.GetChild(0).gameObject;
        OptionPanel = InGameCanvas.transform.GetChild(1).gameObject;
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
    }
}
