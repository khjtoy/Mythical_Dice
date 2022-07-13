using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCondition : AICondition
{
    [SerializeField]
    private int range = 0;
    [SerializeField]
    private Transform _basePos = null;
    public override bool Result()
    {
        Vector2Int playerPos = new Vector2Int(MapController.PosToArray(Define.Controller.transform.localPosition.x), MapController.PosToArray(Define.Controller.transform.localPosition.y));
        Vector2Int basePos = new Vector2Int(MapController.PosToArray(_basePos.localPosition.x), MapController.PosToArray(_basePos.localPosition.y));
        for (int i = -range; i <= range; i++)
        {
            for(int j = -range; j <= range; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                Vector2Int temp = new Vector2Int(basePos.x + j, basePos.y + i);
                if (temp == playerPos)
                    return true;

            }
        }
        return false;
    }
}
