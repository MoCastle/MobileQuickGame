using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactCut_Falling : PlayerState
{
    float Speed = 20;
    public ImpactCut_Falling(BaseActor InActor) : base(InActor)
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
        _Actor.RigidCtrl.velocity = Vector2.down * Speed;
    }
}
