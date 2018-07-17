﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFlyState : BaseState {
    float Speed = 10f;
    public override Vector2 Direction
    {
        get
        {
            return _Actor.ForceMoveDirection;
        }
    }
    public override SkillEnum SkillType
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public ClickFlyState( BaseActor InActor ):base(InActor)
    {
        RangeTime = _Actor.BeCut.RangeTime;
        SpeedRate = _Actor.BeCut.SpeedRate;
        SetCutMeet();
        Attacking();
    }
    public override void IsAttackting()
    {
        _Actor.RigidCtrl.velocity = Speed * Direction;
        if (Mathf.Abs(Direction.x)>0.01f)
        {
            RotateToDirection(Direction);
        }
        
    }
    public override void AttackEnd()
    {
        base.AttackEnd();
        _Actor.RigidCtrl.gravityScale = 0;
    }
    public override void IsAttackEnding()
    {
        
        if(_Actor.RigidCtrl.velocity.y < 0 )
        {
            Vector2 NewVector = _Actor.RigidCtrl.velocity;
            _Actor.RigidCtrl.gravityScale = 0;
            NewVector.y = 0;
            _Actor.RigidCtrl.velocity = NewVector;
        }
        return;
    }
}
