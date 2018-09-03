using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToAction : AIAction
{
    Vector2 TargetPs;
    public RunToAction( EnemyActor InActor, AICtrler InCtrler, Vector2 InTargetPs ):base( InActor, InCtrler )
    {
        TargetPs = InTargetPs;
    }
    public override void LogicUpdate()
    {
        //throw new System.NotImplementedException();
        Vector2 CurPs = _Actor.TransCtrl.position;
        if((TargetPs - CurPs).x > _Actor.MoveSpeed/30)
        {
            _Actor.FaceTo(TargetPs - CurPs);
            _Actor.AnimCtrl.SetBool("Run", true);
        }else
        {
            _Ctrler.PopAIStack();
        }
    }

    public override void Start()
    {
        //throw new System.NotImplementedException();
    }

    
}
