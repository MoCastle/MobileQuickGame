using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;
public class NPCRunAction : NpcBaseAction
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
    public override void Update()
    {
        base.Update();
        m_ActorObj.Physic.SetSpeed(speed);
    }
}
