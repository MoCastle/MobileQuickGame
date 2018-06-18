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
                Vector2 Direction = Input.Shift;
                float Rate = Mathf.Abs(Direction.y / Direction.x);
                //方向修正
                if (Rate < (4f / 3f) && Rate > (3f / 4f))
                {
                    Direction.x = 0.5f;
                    Direction.y = 0.5f;
                }
                else if (Rate > (4f / 3f))
                {
                    Direction.y = 1;
                    Direction.x = 0;
                }
                else
                {
                    Direction.x = 1;
                    Direction.y = 0;
                }
                if (Input.Shift.y < 0)
                {
                    Direction.y = Direction.y * -1;
                }
                if (Input.Shift.x < 0)
                {
                    Direction.x = Direction.x * -1;
                }
                //向下冲做单独处理
                if (Mathf.Abs(Direction.x) < 0.1 && Mathf.Abs(Direction.y) > 0.1 && Direction.y < 0)
                { }
                else
                {
                    DashState NewState = new DashState(_Actor);
                    NewState.InputDirection(Direction);
                    _Actor.PlayerState = NewState;
                }
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
