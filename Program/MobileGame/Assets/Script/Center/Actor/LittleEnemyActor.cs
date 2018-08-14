using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleEnemyActor : EnemyActor {
    public override void LogicAwake()
    {
        base.LogicAwake();
        _AICtrler = new LittleEnemyAICtrler(this);
    }
}
