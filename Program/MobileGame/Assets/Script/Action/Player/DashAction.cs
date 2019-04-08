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
        Vector2 gestureDir = _Input.InputInfo.Shift;
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

    protected override void Move()
    {
        if ((m_Speed * m_Speed) > 0)
        {
            //速率
            float speed = m_Speed;
            float rate = Mathf.Abs(MoveDir.y / 0.7f);
            if ( rate > 1)
            {
                speed *= rate;
            }
            m_ActorObj.Physic.SetSpeed(MoveDir * speed);
        }
           
    }
}
