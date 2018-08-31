using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseFirstAttackState : CloseAttackState
{
    public CloseFirstAttackState(PlayerActor InActor) : base(InActor)
    {
    }
    //第一击不转向
    public override void DealFaceTo()
    {
        //2018.7.17 目前只有普攻有转向问题 暂时的解决方案 三联时记录下最终输入的位置 下一次有输入时做比较
        InputInfo CurInput = Actor.GetTempInput(HandGesture.Click).InputInfo;
        Vector2 CurInputPS = CurInput.EndPs;
        Actor.PreInput = CurInputPS;
        Actor.PreState = SkillType;
    }
}
