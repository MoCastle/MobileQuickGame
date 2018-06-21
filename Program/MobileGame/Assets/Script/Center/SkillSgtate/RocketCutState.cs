using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCutState : PlayerState {
    float Speed = 20;
    public RocketCutState(BaseActor InActor) : base(InActor)
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
