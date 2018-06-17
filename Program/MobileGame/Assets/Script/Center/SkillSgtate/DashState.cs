using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState:BaseState {
    public float ContinueTime = 0.5f;
    public float StartTime;
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.PlayerDash;
        }
    }
    public DashState(BaseActor Actor) : base(Actor)
    {
        Debug.Log("DashState");
        Vector2 Direction = Vector2.right;
        if( _Actor.RigidCtrl.velocity.x < 0 )
        {
            Direction.x = -1;
        }
        StartTime = Time.time;
        _Actor.RigidCtrl.velocity = Direction * 40;
        _Actor.AnimCtrl.SetTrigger("Dash");
    }
    public override void Input(InputInfo Input)
    {

    }

    public override void Update()
    {
        if (StartTime + ContinueTime < Time.time)
        {
            _Actor.PlayerState = new InitState(_Actor);
        }
    }
}
