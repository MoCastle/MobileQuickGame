using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAction : BaseAction {
    protected HitEffect _Effect;
    //减速速率
    protected float _DeSpeedRate = 0.02f;
    float _DeNum;
    public HurtAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo)
    {
        baseActorObj.BeBreak();
        SetFaceLock(true);
        EnterPrepare();
    }
    public virtual void EnterPrepare()
    {
        
        
        /*
        _ActorObj.BeHitEffect.HardValue = 0;
        _ActorObj.BeHitEffect.Delegate = null;
        _ActorObj.PhysicCtrl.ResetData();
        */

        if (_ActorObj.ActionCtrl.IsName("HitBack"))
        {
            _ActorObj.BeHitEffect.MoveVector.y = 0;
        }
        _Effect = _ActorObj.BeHitEffect;
        Vector2 moveSpeed = _Effect.MoveVector;
        //_ActorObj.PhysicCtrl.SetSpeed(moveSpeed);
        float numSpeed = moveSpeed.magnitude;
        SetSpeed(-1* numSpeed);
        float hardTime = _Effect.HardValue * _ActorObj.ActorPropty.HeavyRate;
        HardTime(hardTime);
    }

    public override void Update()
    {
        base.Update();
    }
    protected override void Move()
    {
        if (_CutTimeClock <= 0 && (_Speed * _Speed) > 0)
        {
            _ActorObj.Physic.SetSpeed(MoveDir.normalized * _Speed);
            if (_DeNum * _Speed > 0)
            {
                _DeNum = 0;
                _Speed = 0;
                return;
            }
            float speed = _Speed + _DeNum;
            SetSpeed(speed);
        }
            
    }
    public override void CompleteFunc()
    {
        base.CompleteFunc();
        _ActorObj.Physic.CountinuePhysic();
    }
}
