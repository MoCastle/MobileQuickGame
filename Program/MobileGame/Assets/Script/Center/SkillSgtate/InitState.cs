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
    }
    public override void Input(InputInfo Input)
    {
        if( Input.IsPushing )
        {
            if( Input.XPercent >0.1 )
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
        
        if(_PlayerCtrl.InputRoundArr.HeadInfo.IsLegal )
        {
            InputInfo GetInput = _PlayerCtrl.InputRoundArr.Pop();
            Input(GetInput);
        }
    }
}
