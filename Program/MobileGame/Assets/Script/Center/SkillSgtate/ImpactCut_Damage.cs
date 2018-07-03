using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactCut_Damage : PlayerState
{
    public ImpactCut_Damage(BaseActor InActor) : base(InActor)
    {
    }
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.RocketCut;
        }
    }
    public override void Update()
    {
        Debug.Log("Damaging");
    }
}
