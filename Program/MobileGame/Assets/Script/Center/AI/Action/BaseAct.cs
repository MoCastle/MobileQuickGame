using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAct {
    BaseAI _AICtrl;
    protected EnemyActor Actor;
    public BaseAct(BaseAI InAICtrl )
    {
        _AICtrl = InAICtrl;
        Actor = _AICtrl.Actor;
    }
}
