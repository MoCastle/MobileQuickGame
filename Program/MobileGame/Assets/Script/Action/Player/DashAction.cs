using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

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
        Vector2 gestureDir = m_Input.vector;
        if(gestureDir.x!=0&&this.m_ActorObj.IsOnGround && Mathf.Abs(  gestureDir.y/gestureDir.x)<0.6)
        {
            gestureDir.y = 0;
        }

        RotateToDirection(gestureDir);
        _MoveDir = gestureDir.normalized;
    }

    public override void CompleteFunc()
    {
        RotateToDirection(base.MoveDir);
        base.CompleteFunc();
        //ToDo
        //_ActorObj.ReOpenPlatFoot();
    }
}
