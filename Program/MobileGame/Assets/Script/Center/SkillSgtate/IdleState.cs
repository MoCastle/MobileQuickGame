using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState {
    //体力消耗量 需要定义的话在对应的技能里重写
    public override int CostVITNum
    {
        get
        {
            return 0;
        }
    }
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.Idle;
        }
    }

    public IdleState(PlayerActor InActor) : base(InActor)
    {
    }
}
