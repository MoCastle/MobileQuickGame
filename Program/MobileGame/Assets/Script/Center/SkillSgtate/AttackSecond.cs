using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSecond : PlayerState
{
    public AttackSecond(BaseActor _player) : base(_player)
    {
        Debug.Log("AttackSecondState");
    }
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.AttackSecond;
        }
    }
    public override void Update()
    {
        NormInput HandInput = PlayerCtrl.InputRoundArr.Pop();
        if (HandInput.IsLegal)
        {
            Input(HandInput);
        }
    }
}
