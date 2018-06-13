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
}

public abstract class BaseState {
    protected PlayerCtrl _PlayerCtrl;
    public abstract SkillEnum SkillType
    {
        get;
    }
    public BaseState( PlayerCtrl InPlayerCtrl )
    {
        _PlayerCtrl = InPlayerCtrl;
    }
    // Use this for initialization
    public abstract void Input(InputInfo Input);

    // Update is called once per frame
    public abstract void Update();
    protected void SwitchState( )
    {

    }
}
