using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactCut_Falling : PlayerState
{
    float Speed = 50;
    public ImpactCut_Falling(PlayerActor InActor) : base(InActor)
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
