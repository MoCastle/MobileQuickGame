﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputInfo
{
    public Vector2 Shift;
    public bool IsLegal;
    public bool IsPushing;
    public float MaxDst;
    public InputInfo( bool InIsLegal = false )
    {
        Shift = Vector2.zero;
        IsLegal = InIsLegal;
        IsPushing = false;
        MaxDst = 0;
    }
    public float Percent
    {
        get
        {
            if( MaxDst>0 )
            {
                return Shift.magnitude / MaxDst;
            }
            return 0;
        }
    }
    public float XPercent
    {
        get
        {
            if (MaxDst > 0)
            {
                return Mathf.Abs( Shift.x / MaxDst );
            }
            return 0;
        }
    }
    public float YPercent
    {
        get
        {
            if (MaxDst > 0)
            {
                return Mathf.Abs( Shift.y / MaxDst );
            }
            return 0;
        }
    }
}

public enum SkillEnum
{
    Idle,//闲置状态类
    Run,//玩家跑
    Dash,//玩家冲
    AttackFirst,//一阶打击
    AttackSecond,//二阶打击
    AttackThird,//三阶打击
    Falling,//坠落
    FallingEnd,//着地
    RocketCut,//升龙击
    ImpactCut_pre,//
    ImpactCut_Falling,
    ImpactCut_Damage,
    Default,//默认状态
}
public enum HurtTypeEnum
{
    Normal
}

public abstract class BaseState {
    protected AttackEnum AttackTingState;
    protected enum AttackEnum
    {
        Start,
        Attackting,
        AttackEnd,
        None,
    }
    public virtual int Layer
    {
        get
        {
            return 1;
        }
    }
    protected Vector2 OldSpeed;
    protected float gravityScale;
    protected BaseActor _Actor;
    public virtual Vector2 Direction
    {
        get
        {
            Vector2 ReturnVect = Vector2.left;
            if (ReturnVect.x * _Actor.transform.localScale.x < 0)
            {
                ReturnVect.x = ReturnVect.x * -1;
            }
            return ReturnVect;
        }
    }
    public abstract SkillEnum SkillType
    {
        get;
    }
    public virtual HurtTypeEnum HurtType
    {
        get
        {
            return HurtTypeEnum.Normal;
        }
    }
    public BaseState(BaseActor InActor )
    {
        _Actor = InActor;
        _Actor.LockFace = false;
        AttackStart();
    }
    // Use this for initialization
    public virtual void Update()
    {
        switch (AttackTingState)
        {
            case AttackEnum.Start:
                IsAttackting();
                break;
            case AttackEnum.Attackting:
                IsAttackting();
                break;
            case AttackEnum.AttackEnd:
                IsAttackEnding();
                break;
            default:
                IsNoneState();
                break;
        }
        if(_Actor.SkillHurtBox.enabled)
        {
            SkillAttack();
        }
    }
    public virtual void IsStarting()
    {
        _Actor.LockFace = true;
    }
    public virtual void IsAttackting()
    {
        OldSpeed = _Actor.RigidCtrl.velocity;
        _Actor.RigidCtrl.velocity = Vector2.zero;
        _Actor.RigidCtrl.gravityScale = 0;
    }
    public virtual void IsAttackEnding()
    {

    }
    public virtual void IsNoneState( )
    {

    }
    
    //进入前摇
    public virtual void AttackStart()
    {
        AttackTingState = AttackEnum.None;
    }
    //进入后摇
    public virtual void Attacking()
    {
        AttackTingState = AttackEnum.Attackting;
    }
    //结束
    public virtual void AttackEnd()
    {
        AttackTingState = AttackEnum.AttackEnd;
        _Actor.RigidCtrl.gravityScale = _Actor.GetGravityScale;
    }
    // Update is called once per frame
    public virtual void SkillAttack()
    {
        Collider2D[] TargetList = AttackList();
        foreach (Collider2D TargetCollider in TargetList)
        {
            if( TargetCollider == null )
            {
                return;
            }
            BaseActor TargetActor = TargetCollider.GetComponent<BaseActor>();
            if( TargetActor!= null )
            {
                SkillEffect( TargetActor);
            }
        }
    }
    public virtual Collider2D[] AttackList( )
    {
        Collider2D[] ColliderList = new Collider2D[100];
        ContactFilter2D ContactFilter = new ContactFilter2D();
        ContactFilter.SetLayerMask(Layer);
        _Actor.SkillHurtBox.OverlapCollider(ContactFilter, ColliderList);

        BaseActor TargetActor = null;
        return ColliderList;
    }
    public virtual void SkillEffect( BaseActor TargetActor )
    {
        Vector2 FaceToVect = (TargetActor.TransCtrl.position - _Actor.TransCtrl.position);
        TargetActor.FaceForce(FaceToVect);
        TargetActor.HitBack();
    }
}
