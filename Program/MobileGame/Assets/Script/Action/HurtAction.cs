using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAction : BaseAction {
    protected HitEffect _Effect;
    public HurtAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo)
    {
        SetFaceLock(true);
        EnterPrepare();
    }
    public void EnterPrepare()
    {
        
        _Effect = _ActorObj.BeHitEffect;

        _ActorObj.BeHitEffect.HardValue = 0;
        _ActorObj.BeHitEffect.Delegate = null;
        _ActorObj.PhysicCtrl.ResetData();


        if (_ActorObj.ActionCtrl.IsName("HitBack"))
            _Effect.MoveVector.y = 0;
        Vector2 moveSpeed = _Effect.MoveVector;
        _ActorObj.PhysicCtrl.SetSpeed(moveSpeed);

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
        _ActorObj.PhysicCtrl.ResetData();
    }
}
