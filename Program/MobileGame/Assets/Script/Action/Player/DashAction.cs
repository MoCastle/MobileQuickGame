using System.Collections;
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
        Debug.Log("zerg Dash");
        Vector2 gestureDir = _Input.InputInfo.Shift;
        if(gestureDir.x!=0&&this._ActorObj.IsOnGround && Mathf.Abs(  gestureDir.y/gestureDir.x)<0.6)
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
    }

    protected override void Move()
    {
        if ((_Speed * _Speed) > 0)
        {
            //速率
            float speed = _Speed;
            float rate = Mathf.Abs(MoveDir.y / 0.7f);
            if ( rate > 1)
            {
                speed *= rate;
            }
            _ActorObj.PhysicCtrl.SetSpeed(MoveDir * speed);
        }
           
    }
}
