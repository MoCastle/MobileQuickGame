using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackState : AttackState {
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.Attack;
        }
    }
    public CloseAttackState( PlayerActor InActor ):base( InActor )
    {
        //2018.7.17 目前只有普攻有转向问题 暂时的解决方案 三联时记录下最终输入的位置 下一次有输入时做比较
        InputInfo CurInput = InActor.GetTempInput(HandGesture.Click).InputInfo;
        Vector2 CurInputPS = CurInput.EndPs;
        if (InActor.PreState == SkillType)
        {
            Vector2 DirVector = CurInputPS - InActor.PreInput;
            if (DirVector.x * Direction.x<0 && DirVector.magnitude > CurInput.MaxDst* 0.3)
            {
                _Actor.FaceTo(DirVector);
            }
        }
        InActor.PreInput = CurInputPS;
        InActor.PreState = SkillType;
    }
    public override void Attacking()
    {
        base.Attacking();
        _Actor.MovePs(Direction * _Actor.CAttackMove);
    }
    public override void IsAttackEnding()
    {
        base.IsAttackEnding();

    }
}
