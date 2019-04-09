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
    HitEffect CurEffect;
    float CountTime = -1;
    public HitAction(BaseActorObj baseActorObj, SkillInfo skillInfo):base(baseActorObj, skillInfo)
    {
        SetFaceLock(true);
        CurEffect = baseActorObj.BeHitEffect;
        if(CurEffect.Delegate!=null)
        {
            CurEffect.Delegate(this);
        }
        float hardTime = CurEffect.HardValue - baseActorObj._ActorPropty.Heavy * baseActorObj._ActorPropty.HeavyRate;
        if(CurEffect.HardValue>0)
            HardTime(hardTime);
        //_MoveDir = CurEffect.MoveDir;
        //_Speed = CurEffect.Speed;
        CountTime = Time.time + CurEffect.ContinueTime;
        m_ActorObj.ActionCtrl.AnimSpeed = 0;
    }
    Vector2 _MoveDir;
    protected override Vector2 MoveDir
    {
        get
        {
            return _MoveDir;
        }
    }

    //帧事件
    public override void Update()
    {
        if(CountTime > 0)
        {
            if(CountTime < Time.time)
            {
                CountTime = -1;
                m_HSpeed = 0;
                m_ActorObj.ActionCtrl.AnimSpeed = 1;
            }
        }
        base.Update();
        
    }

    public override void CompleteFunc()
    {
        m_ActorObj.BeHitEffect.ContinueTime = CountTime > 0 ? CountTime - Time.time : 0;
        m_ActorObj.BeHitEffect.MoveVector = m_ActorObj.Physic.MoveSpeed;
        m_ActorObj.BeHitEffect.HardValue = 0;
        m_ActorObj.BeHitEffect.Delegate = null;
        //_ActorObj.BeHitEffect.ContinueTime = 
        base.CompleteFunc();
    }
}
