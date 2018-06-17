﻿using System.Collections;
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
            return SkillEnum.PlayerRun;
        }
    }

    public RunState(BaseActor InActor) : base(InActor)
    {
        Debug.Log("RunState");
        _Actor.AnimCtrl.SetTrigger("Run");

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
                    _Actor.PlayerState = new InitState(_Actor);
                }
            }
        }
        else
        {
            
            if( Input.Percent>0.8 )
            {
                _Actor.PlayerState = new DashState(_Actor);
            }
            else
            {
                _Actor.PlayerState = new InitState(_Actor);
            }
        }
        
    }

    public override void Update()
    {
        if( Command.IsLegal )
        {
            Vector2 Shift = Command.Shift;
            Shift.y = 0;
            Shift = Shift.normalized;
            _Actor.RigidCtrl.velocity = Shift * 5;
            Vector3 Scale = _Actor.TransCtrl.localScale;
            if( Scale.x * Shift.x < 0)
            {
                Scale.x = Scale.x * -1;
                _Actor.TransCtrl.localScale = Scale;
            }
            
        }
        if (PlayerCtrl.InputRoundArr.HeadInfo.IsLegal)
        {
            InputInfo GetInput = PlayerCtrl.InputRoundArr.Pop();
            Input(GetInput);
        }
    }
}
