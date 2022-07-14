using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("¸Ê Å©±â ÁöÁ¤")]
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private int size;

    [field: SerializeField]
    public int BossNum { get; set; }

    public int StageNum;

    public bool StageStart;

    public bool thirdTutorial = false;

    [SerializeField]
    private FadeHandler fade;
    [SerializeField]
    private MapController mapController;
    public int Width
    {
        get
        {
            return width;
        }
    }
    public int Height
    {
        get
        {
            return height;
        }
    }

    public int Size

    {
        get
        {
            return size;
        }
    }

    private void Start()
    {
        DOTween.KillAll();
        fade.Fade(InitMap);
        //PlayerPrefs.SetInt("STAGE", 1);
        
    }

    private void InitMap()
    {
        GameObject BossObject = null;
        switch (PlayerPrefs.GetInt("STAGE", 1))
        {
            case 0:
                {
                    width = 5;
                    height = 5;
                    size = 5;
                    BossObject = PoolManager.Instance.GetPooledObject((int)PooledObject.Statue);
                    break;
                }
            case 1:
                {
                    width = 7;
                    height = 7;
                    size = 7;
                    BossObject = PoolManager.Instance.GetPooledObject((int)PooledObject.Mino);
                    break;
                }
        };

        BossObject.SetActive(true);

        BossObject.transform.SetParent(MapController.Instance.Root);
        BossObject.transform.localPosition = MapController.ArrayToPos(width - 1, height - 1) - Vector3.forward;
        BossObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        SoundManager.Instance.InitMap();
        mapController.InitMap();
        Define.Controller.Init();
    }

    protected override void Init()
    {
    }
}
