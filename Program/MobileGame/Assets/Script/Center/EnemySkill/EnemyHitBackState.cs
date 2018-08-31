using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBackState : EnemyState {
    public override Vector2 Direction
    {
        get
        {
            Vector2 ReturnDir = base.Direction;
            ReturnDir.x = ReturnDir.x * -1;
            return ReturnDir;
        }
    }
    public float HitBackDis= 2f;

    // Use this for initialization
    public EnemyHitBackState(EnemyActor InActor) : base(InActor)
    {
        
        RangeTime = _Actor.BeCut.RangeTime;
        SpeedRate = _Actor.BeCut.SpeedRate;
        SetCutMeet();
        Attacking();
    }

    public override void IsAttackting()
    {
        _Actor.RigidCtrl.velocity = _Actor.HitMoveDir * HitBackDis;
    }
    public override void AttackEnd()
    {
        base.AttackEnd();
        _Actor.RigidCtrl.velocity = _Actor.RigidCtrl.velocity * 0.5f;
    }
}
