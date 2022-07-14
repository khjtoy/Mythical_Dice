using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public enum StageName
{
    InGame = 1
}
public class StageManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> stageList = new List<GameObject>();

    [SerializeField]
    private RectTransform _fadePanel;
    private int currentStage;

    private void Awake()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("CLEAR", 2);
        currentStage = PlayerPrefs.GetInt("CLEAR");
    }

    private void Start()
    {
        _fadePanel.anchoredPosition = new Vector3(0, 0, 0);
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1);
        seq.Append(_fadePanel.DOAnchorPos3DY(1080, 1f));
        seq.AppendCallback(()=> {
            seq.Kill();
            InitStage();
        });
    }

    private void InitStage()
    {
        Debug.Log("asd");
        Sequence seq = DOTween.Sequence();
        for (int i = 0;i<currentStage-1;i++)
        {
            Debug.Log(i);
            seq.Append(stageList[i].transform.DOShakePosition(1, 10, 10));
            int n = i;
            seq.AppendCallback(() => stageList[n].SetActive(false));
        }

    }

    public void Stage(int stage)
    {
        PlayerPrefs.SetInt("STAGE", stage);
        _fadePanel.anchoredPosition = new Vector3(0, -1080, 0);
        Sequence seq = DOTween.Sequence();
        seq.Append(_fadePanel.DOAnchorPosY(0, 1f));
        seq.AppendCallback(() => SceneManager.LoadScene("GamePlay 6"));
    }

}
