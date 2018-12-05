using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAction : PlayerAction {
    float speed
    {
        get
        {
            return _ActorPropty.MoveSpeed;
        }
    }
    public RunAction(BaseActorObj actionCtrler,SkillInfo skillInfo) : base(actionCtrler, skillInfo)
    {

    }
    public override void LogicUpdate()
    {
        _ActorObj.PhysicCtrl.SetSpeed(speed);
    }
}
