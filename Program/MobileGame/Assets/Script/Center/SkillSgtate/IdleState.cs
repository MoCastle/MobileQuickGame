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

    public IdleState(BaseActor InActor) : base(InActor)
    {
        Debug.Log("Init");
    }
}
