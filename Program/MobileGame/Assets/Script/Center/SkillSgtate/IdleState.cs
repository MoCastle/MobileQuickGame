using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState {

    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.Idle;
        }
    }

    public IdleState(PlayerActor InActor) : base(InActor)
    {
    }
}
