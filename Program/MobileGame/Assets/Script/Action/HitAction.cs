﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public enum HitEffectType
{
    //无效果
    None,
    //击退
    HitBack,
    //击飞
    HitFly
}

public struct HitEffect
{
    //运动方向
    public Vector2 MoveDir;
    //运动时间
    public float ContinueTime;
    //运动速度
    public float Speed;
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
        _MoveDir = CurEffect.MoveDir;
        _Speed = CurEffect.Speed;
        CountTime = Time.time + CurEffect.ContinueTime;
        _ActorObj.ActionCtrl.AnimSpeed = 0;
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
                _Speed = 0;
                _ActorObj.ActionCtrl.AnimSpeed = 1;
            }
        }
        base.Update();
    }

    public override void CompleteFunc()
    {
        _ActorObj.BeHitEffect.Speed = _ActorObj.PhysicCtrl.GetSpeed.magnitude;
        _ActorObj.BeHitEffect.ContinueTime = CountTime > 0 ? CountTime - Time.time : 0;
        _ActorObj.BeHitEffect.MoveDir = _ActorObj.PhysicCtrl.GetSpeed.normalized;
        _ActorObj.BeHitEffect.HardValue = 0;
        _ActorObj.BeHitEffect.Delegate = null;
        //_ActorObj.BeHitEffect.ContinueTime = 
        base.CompleteFunc();
    }
}