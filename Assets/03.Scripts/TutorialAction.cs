using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialAction : MonoSingleton<TutorialAction>
{
    [SerializeField]
    private List<Sprite> show1 = new List<Sprite>();
    [SerializeField]
    private List<Sprite> show2 = new List<Sprite>();
    [SerializeField]
    private List<Sprite> show3 = new List<Sprite>();
    [SerializeField]
    private Image image;
    [SerializeField]
    private GameObject prevButton;
    [SerializeField]
    private GameObject nextButton;

    int currentShow = 0;
    int index = 0;

    private bool isTutorial = false;

    private void Start()
    {
       //PlayerPrefs.SetInt("TUTORIAL", 0);
    }

    protected override void Init()
    {
        // Destoryed
    }

    private void Update()
    {
        if (isTutorial && Time.timeScale > 0) Time.timeScale = 1;
    }

    public void TuturialMode()
    {
        if (PlayerPrefs.GetInt("TUTORIAL", 0) == 1) return;
        Time.timeScale = 0;
        isTutorial = true;

        if (currentShow == 0) image.sprite = show1[index];
        else if (currentShow == 1) image.sprite = show2[index];
        else if (currentShow == 2) image.sprite = show3[index];
        prevButton.SetActive(false);
        image.gameObject.SetActive(true);
    }

    public void OffTutorial()
    {
        image.gameObject.SetActive(false);
        currentShow++;
        index = 0;
        Time.timeScale = 1;
        isTutorial = false;

        if (currentShow >= 3) PlayerPrefs.SetInt("TUTORIAL", 1);
    }

    public void PrevImage()
    {
        index--;
        if (index == 0) prevButton.SetActive(false);
        if (currentShow == 0) image.sprite = show1[index];
        else if (currentShow == 1) image.sprite = show2[index];
        else if (currentShow == 2) image.sprite = show3[index];
    }

    public void NextImage()
    {
        index++;
        List<Sprite> temp = new List<Sprite>();
        if (currentShow == 0) temp = show1;
        else if (currentShow == 1) temp = show2;
        else if (currentShow == 2) temp = show3;

        if (index > 0 || !prevButton.activeSelf) prevButton.SetActive(true);
        if (index == temp.Count - 1) nextButton.SetActive(true);
        if (index == temp.Count)
        {
            OffTutorial();
            return;
        }
        image.sprite = temp[index];
    }

}
