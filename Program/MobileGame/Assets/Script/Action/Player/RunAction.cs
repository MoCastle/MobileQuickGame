using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class RunAction : PlayerAction {
    float speed
    {
        get
        {
            return m_ActorPropty.moveSpeed;
        }
    }
    public RunAction(BaseActorObj actionCtrler,SkillInfo skillInfo) : base(actionCtrler, skillInfo)
    {

    }
    public override void LogicUpdate()
    {
        m_ActorObj.Physic.SetSpeed(speed);
    }
}
