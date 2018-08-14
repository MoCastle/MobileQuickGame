
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAction : AIAction {
    protected EnemyActor _EnemyActor;
    public EnemyAction( EnemyActor InActor,AICtrler InCtrler ):base( InActor,InCtrler )
    {
        _EnemyActor = InActor;
    }

}
