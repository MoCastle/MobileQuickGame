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
        SetAnimParam(Input);
        PlayerCtrl.CurOrder = Input;
    }
}
