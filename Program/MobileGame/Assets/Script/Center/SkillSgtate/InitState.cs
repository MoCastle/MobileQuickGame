using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitState : BaseState
{
    
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.Idle;
        }
    }
    
    public InitState( BaseActor InActor ):base(InActor)
    {
        Debug.Log("Init");
        _Actor.RigidCtrl.velocity = Vector2.zero;
        _Actor.AnimCtrl.SetTrigger("Idle");
    }
    public override void Input(InputInfo Input)
    {
        if( Input.IsPushing )
        {
            if( Input.XPercent >0.1 )
            {
                RunState NewState = new RunState(_Actor);
                NewState.Command = Input;
                _Actor.ActorState = NewState;
            }
        }
        else
        {
           if( Input.Percent < 0.3 )
           {
                _Actor.ActorState = new AttackFirst(_Actor);
           }
           else if (Input.Percent > 0.8)
           {
                Vector2 Direction = Input.Shift;
                float Rate = Mathf.Abs(Direction.y / Direction.x);
                //方向修正
                if (Rate < (4 / 3) || Rate > (3 / 4))
                {
                    Direction.x = 0.5f;
                    Direction.y = 0.5f;
                }
                else if (Rate > (4 / 3))
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

                if (Mathf.Abs(Direction.x) < 0.1 && Mathf.Abs(Direction.y) > 0.1 && Direction.y < 0 )
                { }
                else
                {
                    DashState NewState = new DashState(_Actor);
                    NewState.InputDirection(Direction);
                    _Actor.ActorState = NewState;
                }
            }
        }
        
    }

    public override void Update()
    {
        if( !_Actor.IsOnGround)
        {
            _Actor.ActorState = new FallingState(_Actor);
            return;
        }
        if(PlayerCtrl.InputRoundArr.HeadInfo.IsLegal )
        {
            InputInfo GetInput = PlayerCtrl.InputRoundArr.Pop();
            Input(GetInput);
        }
    }
}
