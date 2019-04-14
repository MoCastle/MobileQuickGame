using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;
public class NPCRunAction : BaseAction
{
    protected virtual float speed
    {
        get
        {
            return m_ActorPropty.moveSpeed;
        }
    }
    public NPCRunAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo)
    {

    }
    public override void LogicUpdate()
    {
        m_ActorObj.Physic.SetSpeed(speed);
    }
}
