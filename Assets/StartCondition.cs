using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCondition : AICondition
{
	public override bool Result()
	{
		return GameManager.Instance.StageStart;
	}
}
