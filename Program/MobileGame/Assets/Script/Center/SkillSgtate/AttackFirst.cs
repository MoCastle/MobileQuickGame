using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFirst : BaseState
{

    float ContinueTime = 0.5f;
    float StartTime;
    bool IsContinue = false;
    public AttackFirst( BaseActor _player):base( _player )
    {
        Debug.Log("AttackFirstState");
        StartTime = Time.time;
        _Actor.AnimCtrl.SetTrigger("AttackFirst");
    }
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.AttackFirst;
        }
    }

    public override void Input(InputInfo Input)
    {
        if(!Input.IsPushing )
        {
            IsContinue = true;
        }
        
    }

    public override void Update()
    {
        if (StartTime + ContinueTime < Time.time)
        {
            if(IsContinue)
            {
                _Actor.ActorState = new AttackSecond(_Actor);
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
