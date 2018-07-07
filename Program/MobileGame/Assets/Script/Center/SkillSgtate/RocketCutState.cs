using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCutState : PlayerState
{
    float Speed = 20;
    bool IsFireOff;
    public RocketCutState(PlayerActor InActor) : base(InActor)
    {
        Attacking();
    }
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.RocketCut;
        }
    }
    public void FireOff()
    {
        IsFireOff = true;
    }
    public override void IsAttackting()
    {
        _Actor.RigidCtrl.velocity = Vector2.up * Speed;
    }
    public override void AttackEnd()
    {
        _Actor.RigidCtrl.velocity = _Actor.RigidCtrl.velocity * 0.5f;
        base.AttackEnd();
    }
    //攻击效果
    public override void AttackEffect(BaseActor TargetActor)
    {
        CutEffect Cut = new CutEffect();
        //设置间隔时间
        Cut.RangeTime = RangeTime;
        //减速后速率
        Cut.SpeedRate = SpeedRate;
        TargetActor.ClickFly(Cut,Vector2.up);
    }
}
