using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynaticAction : AIAction
{
    public delegate void DynamicFunc();
    DynamicFunc Updating;
    DynamicFunc Awake;
    public DynaticAction( EnemyActor InActor,AICtrler InCtrler,
        DynamicFunc InUpdating = null,
        DynamicFunc InAwake = null
        ) :base( InActor, InCtrler )
    {
        Updating = InUpdating;
        Awake = InAwake;
    }

    public override void LogicUpdate()
    {
        if(Updating!=null)
        {
            Updating();
        }
    }

    public override void Start()
    {
        if(Awake!=null )
        {
            Awake();
        }
    }
}
