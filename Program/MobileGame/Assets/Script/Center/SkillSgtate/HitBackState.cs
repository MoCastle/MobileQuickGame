using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBackState : BaseState {
    public override Vector2 Direction
    {
        get
        {
            Vector2 ReturnDir = base.Direction;
            ReturnDir.x = ReturnDir.x * -1;
            return ReturnDir;
        }
    }
    //硬直距离
    public float HitBackDis = 4f;
    public override SkillEnum SkillType
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    // Use this for initialization
    public HitBackState( BaseActor InActor):base(InActor)
    {
        
        _Actor.RigidCtrl.gravityScale = 0;
        RangeTime = _Actor.BeCut.RangeTime;
        SpeedRate = _Actor.BeCut.SpeedRate;
        SetCutMeet();
        Attacking();
    }

    public override void IsAttackting()
    {
        _Actor.LockFace = true;
        _Actor.RigidCtrl.velocity = _Actor.HitMoveDir * HitBackDis;
    }
    public override void AttackEnd()
    {
        AttackTingState = AttackEnum.AttackEnd;
        _Actor.RigidCtrl.velocity = _Actor.RigidCtrl.velocity;
    }

}
