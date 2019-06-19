using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class ClickFlyAction : HurtAction {
    HitFlyBuff m_HitFlyBuff;
	public ClickFlyAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo)
    {
    }
    
    public override void startPrepare()
    {
        base.startPrepare();
        HitFlyBuff baseBuff = m_ActorObj.GetBuffByType(BuffType.HitFly) as HitFlyBuff;
        m_ActorObj.FaceToDir(baseBuff.attackter.transform.position - m_ActorObj.transform.position);
        m_HitFlyBuff = baseBuff == null ? new HitFlyBuff() : baseBuff as HitFlyBuff;
        float hardTime = m_HitFlyBuff.hardTime;
        SetHardTime(hardTime);
        Vector2 flySpeed = m_HitFlyBuff.Speed;
        flySpeed.x *= flySpeed.x * m_ActorObj.FaceDir.x < 0 ? -1 : 1;
        m_ActorObj.Physic.SetSpeed(flySpeed);
        m_ActorObj.ActionCtrl.AnimSpeed = 0;
        SetFaceLock(true);
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
