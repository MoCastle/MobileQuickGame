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
    public override Vector2 ClickFly
    {
        get
        {
            return Vector2.up * 0.5f;
        }
    }
    public CloseAttackState( PlayerActor InActor ):base( InActor )
    {
        //2018.7.22 空中攻击技能流畅度调整
        if (!_Actor.IsOnGround)
        {
            _Actor.RigidCtrl.gravityScale = 0;
            _Actor.RigidCtrl.velocity = Vector2.zero;
        }
    }

    //处理转向问题
    public override void DealFaceTo()
    {
        //2018.7.17 目前只有普攻有转向问题 暂时的解决方案 三联时记录下最终输入的位置 下一次有输入时做比较
        InputInfo CurInput = Actor.GetTempInput(HandGesture.Click).InputInfo;
        Vector2 CurInputPS = CurInput.EndPs;
        if (Actor.PreState == SkillType)
        {
            Vector2 DirVector = CurInputPS - Actor.PreInput;
            if (Mathf.Abs(DirVector.x) > CurInput.MaxDst * 0.4 && DirVector.x * Direction.x < 0)
            {
                _Actor.FaceTo(DirVector);
            }
        }
        Actor.PreInput = CurInputPS;
        
    }

    public override void Attacking()
    {
        base.Attacking();
        _Actor.MovePs(Direction * 0.3f);//_Actor.CAttackMove);
    }
    /*
    public override void AttackEnd()
    {
        AttackTingState = AttackEnum.AttackEnd;
        //_Actor.RigidCtrl.gravityScale = _Actor.GetGravityScale;
    }*/
    public override void IsAttackEnding()
    {
        base.IsAttackEnding();

    }

    public override void Update()
    {
        base.Update();
    }
}
