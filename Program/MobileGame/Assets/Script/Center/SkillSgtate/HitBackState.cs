using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBackState : PlayerState {
    public override Vector2 Direction
    {
        get
        {
            Vector2 ReturnDir = base.Direction;
            ReturnDir.x = ReturnDir.x * -1;
            return ReturnDir;
        }
    }
    public float HitBackDis = 12f;
    public override SkillEnum SkillType
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    // Use this for initialization
    public HitBackState( PlayerActor InActor):base(InActor) {
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
