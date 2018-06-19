using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEndState : BaseState {
    float ContinueTime = 0.317f;
    float StartTime;
    public FallingEndState(BaseActor InActor) : base(InActor)
    {
        Debug.Log("FallingEndState");
        _Actor.AnimCtrl.SetTrigger("FallingEnd");

        StartTime = Time.time;
    }
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.Falling;
        }
    }

    public override void Input(InputInfo Input)
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        if (StartTime + ContinueTime < Time.time)
        {
            _Actor.ActorState = new InitState(_Actor);
        }
    }
}
