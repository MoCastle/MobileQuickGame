using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackThird : BaseState {
    float ContinueTime = 0.5f;
    float StartTime;
    bool IsContinue = false;
    public AttackThird(BaseActor _player) : base(_player)
    {
        Debug.Log("AttackThirdState");
        StartTime = Time.time;
        _Actor.AnimCtrl.SetTrigger("AttackThird");
    }
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.PlayerAttackThird;
        }
    }

    public override void Input(InputInfo Input)
    {

    }

    public override void Update()
    {
        if (StartTime + ContinueTime < Time.time)
        {
            _Actor.PlayerState = new InitState(_Actor);
        }
    }
}
