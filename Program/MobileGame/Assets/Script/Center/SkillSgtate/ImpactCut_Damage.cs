using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactCut_Damage : PlayerState
{
    public ImpactCut_Damage(PlayerActor InActor) : base(InActor)
    {
    }
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.RocketCut;
        }
    }
}
