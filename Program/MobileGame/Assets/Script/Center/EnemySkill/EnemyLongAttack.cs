using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongAttack : EnemyState
{
    bool Attackting = false;
    public EnemyLongAttack(EnemyActor InaActor) : base(InaActor) { }
    Vector2 OldSpeed;

    public override void Update()
    {
        if( Attackting )
        {
            _Actor.RigidCtrl.velocity = Direction * _Actor.LAttackSpeed;
        }
    }
    public override void Attacking()
    {
        Attackting = true;
        OldSpeed = _Actor.RigidCtrl.velocity;
    }
    public override void AttackEnd()
    {
        _Actor.RigidCtrl.velocity = OldSpeed;
    }
}
