using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackState : AttackState {

	public CloseAttackState( PlayerActor InActor ):base( InActor )
    { }
    public override void Attacking()
    {
        base.Attacking();
        _Actor.MovePs(Direction * _Actor.CAttackMove);
    }
    public override void AttackEnd()
    {
        base.AttackEnd();
    }
}
