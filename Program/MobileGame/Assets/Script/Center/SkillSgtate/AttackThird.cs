using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackThird : PlayerState
{
    public AttackThird(BaseActor _player) : base(_player)
    {
        Debug.Log("AttackThirdState");
    }
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.AttackThird;
        }
    }
}
