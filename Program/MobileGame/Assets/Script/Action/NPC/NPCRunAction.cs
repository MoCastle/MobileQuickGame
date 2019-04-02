using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRunAction : BaseAction
{
    protected virtual float speed
    {
        get
        {
            return _ActorPropty.MoveSpeed;
        }
    }
    public NPCRunAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo)
    {

    }
    public override void LogicUpdate()
    {
        _ActorObj.Physic.SetSpeed(speed);
    }
}
