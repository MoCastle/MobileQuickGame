using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : BaseState
{
    
    public FallingState(BaseActor InActor) : base(InActor)
    {
        Debug.Log("Falling");
        _Actor.AnimCtrl.SetTrigger("Falling");
    }
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.Falling;
        }
    }

    public override void Input(InputInfo Input)
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        if( _Actor.IsOnGround )
        {
            _Actor.PlayerState = new FallingEndState(_Actor);
        }
    }
}
