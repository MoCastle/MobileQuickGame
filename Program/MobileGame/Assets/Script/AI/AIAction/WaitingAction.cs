using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingAction : AIAction
{
    float Time;
    float CountTime = 0;
    public WaitingAction(EnemyActor InActor, AICtrler InCtrler,float InTime) : base(InActor, InCtrler)
    {
        Time = InTime;
    }

    public override void LogicUpdate()
    {
        CountTime += UnityEngine.Time.deltaTime;
        if( CountTime > Time )
        {
            _Ctrler.PopAIStack( );
        }
    }

    public override void Start()
    {
        //throw new System.NotImplementedException();
    }
}
