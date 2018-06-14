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
    public InitState( PlayerCtrl InPlayerCtrl ):base(InPlayerCtrl)
    {
        _PlayerCtrl.PlayerRigid.velocity = Vector2.zero;
        if(_PlayerCtrl.Player == null)
        {
            Debug.Log("InitState:Init EmptyPlayer");
        }else if(_PlayerCtrl.Player.PlayerAnimator == null)
        {
            Debug.Log("InitState:Init EmptyPlayerAnimator");
        }
        _PlayerCtrl.Player.PlayerAnimator.SetTrigger("Idle");
    }
    public override void Input(InputInfo Input)
    {
        Debug.Log("InitStateUpdate");
        if( Input.IsPushing )
        {
            if( Input.XPercent >0.1 )
            {
                Debug.Log("RunState");
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
        
        if(_PlayerCtrl.InputRoundArr.HeadInfo.IsLegal )
        {
            InputInfo GetInput = _PlayerCtrl.InputRoundArr.Pop();
            Input(GetInput);
        }
    }
}
