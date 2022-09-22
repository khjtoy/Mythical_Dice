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
        Define.OrginCam = Define.MainCam.transform.position;
        UIManager.Instance.IsInGame = true;
        //PlayerPrefs.SetInt("STAGE", 4);
        
    }

    private void InitMap()
    {
        GameObject BossObject = null;
        switch (PlayerPrefs.GetInt("STAGE", 0))
        {
            case 0:
                { 
                    width = 3;
                    height = 3;
                    size = 3;
                    BossObject = PoolManager.Instance.GetPooledObject((int)PooledObject.Slime);
                    break;
                }
            case 1:
                {
                    width = 5;
                    height = 5;
                    size = 5;
                    BossObject = PoolManager.Instance.GetPooledObject((int)PooledObject.Statue);
                    break;
                }
            case 2:
                {
                    width = 7;
                    height = 7;
                    size = 7;
                    BossObject = PoolManager.Instance.GetPooledObject((int)PooledObject.Mino);
                    break;
                }
            case 3:
                {
                    width = 7;
                    height = 7;
                    size = 7;
                    BossObject = PoolManager.Instance.GetPooledObject((int)PooledObject.MinoHard);
                    break;
                }
            case 4:
                {
                    width = 6;
                    height = 6;
                    size = 6;
                    BossObject = PoolManager.Instance.GetPooledObject((int)PooledObject.Siren);
                    break;
                }
        };

        BossObject.SetActive(true);
        Define.PlayerObj.GetComponent<PlayerLayer>().SetEnemys();
        BossObject.transform.SetParent(MapController.Instance.Root);
        BossObject.transform.localPosition = MapController.ArrayToPos(width - 1, height - 3) - Vector3.forward;
        Define.Controller.transform.localPosition = -MapController.ArrayToPos(width - 1, height - 1) - Vector3.forward;
        Define.Controller.GetComponent<CharacterMove>().PlayerPos = -MapController.ArrayToPos(width - 1, height - 1);
        BossObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
		SoundManager.Instance.InitMap();
		mapController.InitMap();
        Define.Controller.Init();
    }

    protected override void Init()
    {
    }
}
