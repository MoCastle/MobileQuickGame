using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFirst : PlayerState
{
    public AttackFirst( BaseActor _player):base( _player )
    {
        Debug.Log("AttackFirstState");
    }
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.AttackFirst;
        }
    }

    public override void Update()
    {
        NormInput HandInput = PlayerCtrl.InputRoundArr.Pop();
        if( HandInput.IsLegal )
        {
            Input(HandInput);
        }
        
    }
}
