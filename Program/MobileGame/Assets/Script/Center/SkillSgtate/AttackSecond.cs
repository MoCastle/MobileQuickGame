using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSecond : BaseState {
    
    float ContinueTime = 0.5f;
    float StartTime;
    bool IsContinue = false;
    public AttackSecond(BaseActor _player) : base(_player)
    {
        Debug.Log("AttackSecondState");
        StartTime = Time.time;
        _Actor.AnimCtrl.SetTrigger("AttackSecond");
    }
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.AttackSecond;
        }
    }

    public override void Input(InputInfo Input)
    {
        if (!Input.IsPushing)
        {
            IsContinue = true;
        }
    }

    public override void Update()
    {
        if (StartTime + ContinueTime < Time.time)
        {
            if (IsContinue)
            {
                _Actor.ActorState = new AttackThird(_Actor);
            }
            else
            {
                _Actor.ActorState = new InitState(_Actor);
            }
        }

        if (PlayerCtrl.InputRoundArr.HeadInfo.IsLegal)
        {
            InputInfo GetInput = PlayerCtrl.InputRoundArr.Pop();
            Input(GetInput);
        }
    }
}
