using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState
{

    public override SkillEnum SkillType
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public AttackState(PlayerActor InaActor) : base(InaActor) { }

    public override void Update()
    {
    }
    
    public override void Attacking()
    {
        OldSpeed = _Actor.RigidCtrl.velocity;
        _Actor.RigidCtrl.velocity = Vector2.zero;
    }
    public override void AttackEnd()
    {
        _Actor.RigidCtrl.velocity = OldSpeed;
    }
}
