using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GameScene;
public enum HitEffectType
{
    //无
    None,
    //击退
    HitBack,
    //击飞
    ClickFly
}

public struct HitEffect
{
    public HitEffectType HitType;
    //运动方向
    public Vector2 MoveVector;
    //击退值
    public float HitFlyValue;
    //运动时间
    public float ContinueTime;
    //硬直值
    public float HardValue;

    //补充函数
    public Action<BaseAction> Delegate;
}

public class HitAction : BaseAction {
    HitBuff m_HitBuff;
    Vector2 m_MoveDir;

    public HitAction(BaseActorObj baseActorObj, SkillInfo skillInfo):base(baseActorObj, skillInfo)
    {
        SetFaceLock(true);
        BaseBuff baseBuff = baseActorObj.GetBuffByType(BuffType.Hit);
        m_HitBuff = baseBuff == null? new HitBuff(): baseBuff as HitBuff;
        float hardTime = m_HitBuff.hardTime;
        SetHardTime(hardTime);
        
        m_ActorObj.Physic.SetSpeed( Vector2.right* m_HitBuff.addSpeed);
        m_ActorObj.ActionCtrl.AnimSpeed = 0;
    }
    protected override Vector2 MoveDir
    {
        get
        {
            return m_MoveDir;
        }
    }

    public override void CompleteFunc()
    {
        m_HitBuff.RemoveSelf();
        base.CompleteFunc();
    }
}
