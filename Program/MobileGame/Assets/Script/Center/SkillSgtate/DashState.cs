using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState:BaseState {

    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.PlayerInit;
        }
    }
    public DashState(BaseActor Actor) : base(Actor)
    {
        Vector2 Direction = Vector2.right;
        if( _Actor.RigidCtrl.velocity.x < 0 )
        {
            Direction.x = -1;
        }
        
        _Actor.RigidCtrl.velocity = Direction * 10;
        _Actor.AnimCtrl.SetTrigger("Dash");
    }
    public override void Input(InputInfo Input)
    {
        if (Input.IsPushing)
        {
            if (Input.XPercent > 0.1)
            {
                RunState NewState = new RunState(_Actor);
                NewState.Command = Input;
                _Actor.PlayerState = NewState;

            }

        }
        else
        {

        }

    }

    public override void Update()
    {

        if ( PlayerCtrl.InputRoundArr.HeadInfo.IsLegal)
        {
            InputInfo GetInput = PlayerCtrl.InputRoundArr.Pop();
            Input(GetInput);
        }
    }
}
