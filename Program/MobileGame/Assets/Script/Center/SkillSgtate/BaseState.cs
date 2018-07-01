using System.Collections;
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

public abstract class BaseState {
    protected BaseActor _Actor;
    public Vector2 Direction
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
    public BaseState(BaseActor InActor )
    {
        _Actor = InActor;
    }
    // Use this for initialization
    public virtual void AttackStart()
    {
    }
    public virtual void Attacking()
    {
    }
    public virtual void AttackEnd()
    {
    }
    // Update is called once per frame
    public abstract void Update();
}
