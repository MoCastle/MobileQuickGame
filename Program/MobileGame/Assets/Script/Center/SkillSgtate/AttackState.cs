using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AttackState : PlayerState
{
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

    public AttackState(PlayerActor InaActor) : base(InaActor) { }
    public override void Update()
    {
        base.Update();
        switch(AttackTingState)
        {
            case AttackEnum.Start:
                IsAttackting();
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

    }
    public virtual void IsAttackting( )
    {
        OldSpeed = _Actor.RigidCtrl.velocity;
        _Actor.RigidCtrl.velocity = Vector2.zero;
        gravityScale = _Actor.RigidCtrl.gravityScale;
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
        _Actor.RigidCtrl.gravityScale = gravityScale;
    }
}
