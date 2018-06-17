using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitState : BaseState
{
    
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.PlayerInit;
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
                _Actor.PlayerState = NewState;
            }
        }
        else
        {
             
           if( Input.Percent < 0.3 )
           {
                _Actor.PlayerState = new AttackFirst(_Actor);
           }
           else if (Input.XPercent > 0.8)
           {
               _Actor.PlayerState = new DashState(_Actor);
           }
        }
        
    }

    public override void Update()
    {
        
        if(PlayerCtrl.InputRoundArr.HeadInfo.IsLegal )
        {
            InputInfo GetInput = PlayerCtrl.InputRoundArr.Pop();
            Input(GetInput);
        }
    }
}
