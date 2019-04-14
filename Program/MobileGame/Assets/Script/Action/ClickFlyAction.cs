using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class ClickFlyAction : HurtAction {
    HitFlyBuff m_HitFlyBuff;
	public ClickFlyAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo)
    {
        SetFaceLock(true);
        BaseBuff baseBuff = baseActorObj.GetBuff(BuffType.HitFly);
        m_HitFlyBuff = baseBuff == null ? new HitFlyBuff() : baseBuff as HitFlyBuff;
        float hardTime = m_HitFlyBuff.hardTime;
        SetHardTime(hardTime);
        Vector2 flySpeed = m_HitFlyBuff.Speed;
        flySpeed.x *= flySpeed.x * m_ActorObj.FaceDir.x < 0 ? -1 : 1;
        m_ActorObj.Physic.SetSpeed(flySpeed);
        m_ActorObj.ActionCtrl.AnimSpeed = 0;
    }

    public override void EnterPrepare()
    {
        _Effect = m_ActorObj.BeHitEffect;
        m_ActorObj.BeHitEffect.HardValue = 0;
        m_ActorObj.BeHitEffect.Delegate = null;
        m_ActorObj.Physic.PausePhysic();

        Vector2 moveSpeed = _Effect.MoveVector;
        m_ActorObj.Physic.SetSpeed(moveSpeed);
        float hardTime = 0;// _Effect.HardValue * m_ActorObj.ActorPropty.HeavyRate;
        SetHardTime(hardTime);
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
