using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState {

    protected enum AttackEnum
    {
        Start,
        Attackting,
        AttackEnd
    }
    protected AttackEnum AttackTingState;
    public override SkillEnum SkillType
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public EnemyAttackState(EnemyActor InaActor) : base(InaActor)
    {
    }
    public override void Update()
    {
        switch (AttackTingState)
        {
            case AttackEnum.Start:
                IsStarting();
                break;
            case AttackEnum.Attackting:
                IsAttackting();
                break;
            case AttackEnum.AttackEnd:
                IsAttackEnding();
                break;
        }
    }
    public virtual void IsStarting()
    {
        _Actor.LockFace = true;
    }
    public virtual void IsAttackting()
    {
        _Actor.RigidCtrl.velocity = Vector2.zero;
        _Actor.RigidCtrl.gravityScale = 0;
    }
    public virtual void IsAttackEnding()
    {

    }
    public override void AttackStart()
    {
        AttackTingState = AttackEnum.Start;
    }
    public override void Attacking()
    {
        AttackTingState = AttackEnum.Attackting;
    }
    public override void AttackEnd()
    {
        AttackTingState = AttackEnum.AttackEnd;
        _Actor.RigidCtrl.gravityScale = _Actor.GetGravityScale;
    }
}
