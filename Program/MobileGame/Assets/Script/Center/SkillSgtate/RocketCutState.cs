using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCutState : PlayerState {
    float Speed = 20;
    bool IsFireOff;
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
        if( !IsFireOff)
        {
            _Actor.RigidCtrl.velocity = Vector2.up * Speed;
        }
    }
    public void FireOff( )
    {
        IsFireOff = true;
    }
}
