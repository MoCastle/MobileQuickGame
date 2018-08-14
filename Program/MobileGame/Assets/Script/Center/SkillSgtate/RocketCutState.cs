using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCutState : PlayerState
{
    float Speed = 25;
    bool IsFireOff;
    List<BaseActor> DragList = new List<BaseActor>();
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
        _Actor.RigidCtrl.gravityScale = 0f;
        foreach (BaseActor TargetActor in DragList)
        {
            TargetActor.Attackting();
        }
    }
    public override void Update()
    {
        foreach (BaseActor TargetActor in DragList)
        {
            FixDragPs(TargetActor);
        }
        base.Update();
    }
    //攻击效果
    public override void AttackEffect(BaseActor TargetActor)
    {
        CutEffect Cut = new CutEffect();
        //设置间隔时间
        Cut.RangeTime = RangeTime;
        //减速后速率
        Cut.SpeedRate = SpeedRate;
        if(TargetActor.DragFly())
        {
            DragList.Add(TargetActor);
        }
    }

    void FixDragPs( BaseActor TargetActor )
    {
        //偏移量
        Vector3 Shifting = (Vector3)Direction * 1.5f;
        Shifting.y = 0;
        //位置修正
        Vector3 FixVector = TargetActor.FootTransCtrl.localPosition - _Actor.FootTransCtrl.localPosition + Shifting;
        Vector3 NewPs = _Actor.TransCtrl.position + FixVector;
        TargetActor.TransCtrl.position = NewPs;
    }
}
