using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : BaseState
{
    InputInfo _Command;
    float TimeCount;
    public InputInfo Command
    {
        get
        {
            return _Command;
        }
        set
        {
            _Command = value;
        }
    }

    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.PlayerInit;
        }
    }

    public RunState(PlayerCtrl InPlayerCtrl) : base(InPlayerCtrl)
    {
        Debug.Log("EnterRunState");
        _PlayerCtrl.Player.PlayerAnimator.SetTrigger("Run");

    }

    public override void Input(InputInfo Input)
    {
        
        if( Input.IsPushing )
        {
            if (Input.Percent > 0.1)
            {
                if (Input.XPercent > 0.1 && Command.Shift.x * Input.Shift.x < 0 )
                {
                    Command = Input;
                }
                TimeCount = 0;
            }
            else
            {
                if (TimeCount < 1)
                {
                    TimeCount = Time.fixedDeltaTime;
                }
                if (Time.fixedDeltaTime > TimeCount + 0.2)
                {
                    _PlayerCtrl.PlayerState = new InitState(_PlayerCtrl);
                }
            }
        }
        else
        {
            /*
            if( Input.Percent>0.8 )
            {

            }
            else
            {
                _PlayerCtrl.PlayerState = new InitState(_PlayerCtrl);
            }*/
            _PlayerCtrl.PlayerState = new InitState(_PlayerCtrl);
        }
        
    }

    public override void Update()
    {
        if( Command.IsLegal )
        {
            Vector2 Shift = Command.Shift;
            Shift.y = 0;
            Shift = Shift.normalized;
            _PlayerCtrl.PlayerRigid.velocity = Shift * 5;
            Vector3 Scale = _PlayerCtrl.Player.transform.localScale;
            if( Scale.x * Shift.x < 0)
            {
                Scale.x = Scale.x * -1;
                _PlayerCtrl.Player.transform.localScale = Scale;
            }
            
        }
        if (_PlayerCtrl.InputRoundArr.HeadInfo.IsLegal)
        {
            InputInfo GetInput = _PlayerCtrl.InputRoundArr.Pop();
            Input(GetInput);
        }
    }
}
