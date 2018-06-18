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
    PlayerInit,//闲置状态类
    PlayerRun,//玩家跑
    PlayerDash,//玩家冲
    PlayerAttackFirst,//一阶打击
    PlayerAttackSecond,//二阶打击
    PlayerAttackThird,//三阶打击
    Falling,//坠落
    FallingEnd,//着地
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
    public abstract void Input(InputInfo Input);

    // Update is called once per frame
    public abstract void Update();
    protected void SwitchState( )
    {

    }
}
