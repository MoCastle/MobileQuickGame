using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFlyState : BaseState
{
    public DragFlyState( BaseActor InActor ):base( InActor )
    {
        _Actor.RigidCtrl.gravityScale = 0;
        _Actor.RigidCtrl.velocity = Vector2.zero;
        NoneState();
        _Actor.AnimCtrl.speed = 0;
    }
    public override void Attacking()
    {
        _Actor.AnimCtrl.speed = 1;
    }
    public override void IsAttackEnding()
    {
        base.IsAttackEnding();
    }
    public override void AttackEnd()
    {
        _Actor.RigidCtrl.gravityScale = 0;
        _Actor.RigidCtrl.velocity = Vector2.zero;
    }
    public override void Update()
    {
        base.Update();
    }
}
