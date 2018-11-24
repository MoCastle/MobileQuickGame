using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloseAttackState : EnemyAttackState
{
	public EnemyCloseAttackState( EnemyActor InaActor):base( InaActor ){ }
    
    public override void Attacking()
    {
        base.Attacking();
        _Actor.MovePs(Direction * _Actor.CAttackMove);
    }
}
