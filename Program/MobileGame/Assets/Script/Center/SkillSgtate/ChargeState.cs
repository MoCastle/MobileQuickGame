using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : PlayerState
{
    float CountTime;
    public override SkillEnum SkillType
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }
    public ChargeState(BaseActor InActor) : base(InActor)
    {
        CountTime = Time.time;
    }
    public override void Input(NormInput Input)
    {
        Input.LifeTime = CountTime;
        if (Input.Dir != InputDir.Middle)
        {
            if (Input.Direction.x * _Actor.transform.localScale.x < 0)
            {
                Vector2 NewScale = _Actor.transform.localScale;
                NewScale.x = NewScale.x * -1;
                _Actor.TransCtrl.localScale = NewScale;
            }
        }
        SetAnimParam(Input);
        PlayerCtrl.CurOrder = Input;
    }
}
