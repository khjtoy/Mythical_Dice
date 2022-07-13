using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMove : CharacterMove
{
    public abstract bool IsFloating { get; set; }
}
