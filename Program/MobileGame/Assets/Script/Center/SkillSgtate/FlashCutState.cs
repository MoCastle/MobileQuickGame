using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashCutState : PlayerState {
    float Speed = 20;
    public Vector2 Direction
    {
        get
        {
            Vector2 ReturnDirection = Vector2.right;
            if (_Actor.TransCtrl.localScale.x < 0)
            {
                ReturnDirection.x = -1;
            }
            return ReturnDirection;
        }
    }
    public FlashCutState(BaseActor InActor) : base(InActor)
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
        _Actor.RigidCtrl.velocity = Direction * Speed;
    }
}
