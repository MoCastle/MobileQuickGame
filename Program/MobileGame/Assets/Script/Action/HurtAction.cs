using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

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
        if (m_ActorObj.ActionCtrl.IsName("HitBack"))
        {
            m_ActorObj.BeHitEffect.MoveVector.y = 0;
        }
        _Effect = m_ActorObj.BeHitEffect;
        Vector2 moveSpeed = _Effect.MoveVector;
        //_ActorObj.PhysicCtrl.SetSpeed(moveSpeed);
        float numSpeed = moveSpeed.magnitude;
        SetSpeed(-1* numSpeed);
        float hardTime = 0;// _Effect.HardValue * m_ActorObj.ActorPropty.HeavyRate;
        SetHardTime(hardTime);
    }

    public override void Update()
    {
        base.Update();
    }
    protected override void Move()
    {
        if (m_CutTimeClock <= 0 && (m_HSpeed * m_HSpeed) > 0)
        {
            m_ActorObj.Physic.SetSpeed(MoveDir.normalized * m_HSpeed);
            if (_DeNum * m_HSpeed > 0)
            {
                _DeNum = 0;
                m_HSpeed = 0;
                return;
            }
            float speed = m_HSpeed + _DeNum;
            SetSpeed(speed);
        }
            
    }
    public override void CompleteFunc()
    {
        base.CompleteFunc();
        m_ActorObj.Physic.CountinuePhysic();
    }
}
