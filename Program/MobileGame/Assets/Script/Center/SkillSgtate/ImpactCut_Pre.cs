using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactCut_Pre : PlayerState
{
    float Speed = 20;
    public ImpactCut_Pre(BaseActor InActor) : base(InActor)
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
        _Actor.RigidCtrl.velocity = Vector2.up * Speed;
    }
}
