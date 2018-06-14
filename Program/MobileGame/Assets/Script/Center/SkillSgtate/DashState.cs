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
    public DashState(PlayerCtrl InPlayerCtrl) : base(InPlayerCtrl)
    {
        Vector2 Direction = Vector2.right;
        if( _PlayerCtrl.PlayerRigid.velocity.x < 0 )
        {
            Direction.x = -1;
        }
        
        _PlayerCtrl.PlayerRigid.velocity = Direction * 10;
        _PlayerCtrl.Player.PlayerAnimator.SetTrigger("Dash");
    }
    public override void Input(InputInfo Input)
    {
        if (Input.IsPushing)
        {
            if (Input.XPercent > 0.1)
            {
                RunState NewState = new RunState(_PlayerCtrl);
                NewState.Command = Input;
                _PlayerCtrl.PlayerState = NewState;

            }

        }
        else
        {

        }

    }

    public override void Update()
    {

        if (_PlayerCtrl.InputRoundArr.HeadInfo.IsLegal)
        {
            InputInfo GetInput = _PlayerCtrl.InputRoundArr.Pop();
            Input(GetInput);
        }
    }
}
