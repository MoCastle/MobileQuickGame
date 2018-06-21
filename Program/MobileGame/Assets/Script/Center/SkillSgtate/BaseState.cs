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
}

public abstract class BaseState {
    protected BaseActor _Actor;
    public abstract SkillEnum SkillType
    {
        get;
    }
    public BaseState(BaseActor InActor )
    {
        _Actor = InActor;
    }
    // Use this for initialization
    public abstract void Input(NormInput HandInput);

    // Update is called once per frame
    public abstract void Update();
}
