using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackThirdState : CloseAttackState
{
    //重击硬直时间
    float ChargingTime = 0.0f;
    float CountTime = 0;
    float Speed = 3;
    public CloseAttackThirdState(PlayerActor InActor) : base(InActor)
    {
        NoneState();
    }
    public override void AttackStart()
    {
        CountTime = 0;
        _Actor.AnimCtrl.speed = 0;
        AttackTingState = AttackEnum.Start;
    }
    public override void Attacking()
    {
        base.Attacking();
    }
    public override void IsStarting()
    {
        if (CountTime > ChargingTime)
        {
            _Actor.AnimCtrl.speed = 1;
            Attacking();
        }else
        {
            CountTime = CountTime + Time.deltaTime;
        }
    }
    public override void IsAttackting()
    {
        Actor.RigidCtrl.velocity = Direction * Speed;

    }


}
