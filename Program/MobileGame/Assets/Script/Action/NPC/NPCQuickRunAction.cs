using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class NPCQuickRunAction : NPCRunAction
{
    public float SpeedRate = 2; 
    protected override float speed
    {
        get
        {
            return base.speed * SpeedRate;
        }
    }
    public NPCQuickRunAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo)
    {

    }
    public override void LogicUpdate()
    {
        m_ActorObj.Physic.SetSpeed(speed);
    }
}
