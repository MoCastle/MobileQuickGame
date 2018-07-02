using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongAttack : EnemyAttackState
{
    bool Attackting = false;
    public EnemyLongAttack(EnemyActor InaActor) : base(InaActor) { }
    Vector2 OldSpeed;

    public override void IsAttackting()
    {
        _Actor.RigidCtrl.velocity = Direction * _Actor.LAttackSpeed;
    }
    public override void AttackEnd()
    {
        base.AttackEnd();
        _Actor.RigidCtrl.velocity = Vector2.zero;
    }
}
