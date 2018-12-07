﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAction : PlayerAction {
    float _HNum = 2.5f;

    Vector2 _MoveDir;
    protected override Vector2 MoveDir
    {
        get
        {
            return _MoveDir;
        }
    }

    public DashAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo)
    {
        
        Vector2 gestureDir = _Input.InputInfo.Shift;
        RotateToDirection(gestureDir);
        _MoveDir = gestureDir.normalized;
    }

    public override void CompleteFunc()
    {
        RotateToDirection(base.MoveDir);
        base.CompleteFunc();
    }

    protected override void Move()
    {
        if ((_Speed * _Speed) > 0)
        {
            //速率
            float speed = _Speed;
            float rate = Mathf.Abs(MoveDir.y / 0.5f);
            if ( rate > 1)
            {
                speed *= rate;
            }
            _ActorObj.PhysicCtrl.SetSpeed(MoveDir * speed);
        }
           
    }
}