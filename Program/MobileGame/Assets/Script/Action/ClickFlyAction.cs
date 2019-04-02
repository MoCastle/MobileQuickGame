using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFlyAction : HurtAction {

	public ClickFlyAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo)
    { 
    }

    public override void EnterPrepare()
    {
        _Effect = _ActorObj.BeHitEffect;
        _ActorObj.BeHitEffect.HardValue = 0;
        _ActorObj.BeHitEffect.Delegate = null;
        _ActorObj.Physic.PausePhysic();

        Vector2 moveSpeed = _Effect.MoveVector;
        _ActorObj.Physic.SetSpeed(moveSpeed);
        float hardTime = _Effect.HardValue * _ActorObj.ActorPropty.HeavyRate;
        HardTime(hardTime);
    }
    public override void Update()
    {
        base.Update();
    }
    public override void CompleteFunc()
    {
        base.CompleteFunc();
    }
    /*
    protected override void Move()
    {
        if (_CutTimeClock <= 0 && (_Speed * _Speed) > 0)
        {
            _ActorObj.PhysicCtrl.SetSpeed(MoveDir.normalized * _Speed);
            
        }

    }*/
}
