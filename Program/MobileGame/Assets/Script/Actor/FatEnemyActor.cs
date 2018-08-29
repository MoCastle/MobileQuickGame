using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatEnemyActor : EnemyActor
{
    public float CloseDis = 3;
    public float HightCheck = 3;
    public float FarDis = 7;
    public override void LogicAwake()
    {
        base.LogicAwake();
        _AICtrler = new FatAssAICtrler (this);
    }
}
