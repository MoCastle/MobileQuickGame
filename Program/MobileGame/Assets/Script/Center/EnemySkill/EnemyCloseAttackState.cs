using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloseAttackState : EnemyState
{
	public EnemyCloseAttackState( EnemyActor InaActor):base( InaActor ){ }

    public override void Update()
    {
    }
    public override void Attacking()
    {
        _Actor.MovePs(Direction * _Actor.CAttackMove);
    }
}
