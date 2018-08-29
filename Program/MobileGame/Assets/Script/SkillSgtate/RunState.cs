using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerState
{
    public float Speed = 5f;
    public Vector2 Direction
    {
        get
        {
            Vector2 ReturnDirection = Vector2.right;
            if(_Actor.TransCtrl.localScale.x < 0)
            {
                ReturnDirection.x = -1;
            }
            return ReturnDirection;
        }
    }
    InputInfo _Command;
    float TimeCount;
    public InputInfo Command
    {
        get
        {
            return _Command;
        }
        set
        {
            _Command = value;
        }
    }

    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.Run;
        }
    }

    public RunState(PlayerActor InActor) : base(InActor)
    {
        _Actor.AnimCtrl.SetBool("IsRunning", true);
    }

    public override void Update()
    {
        if( !_Actor.IsOnGround )
        {
            return;
        }
        NormInput HandInput = Actor.CurInput;
        
        if( HandInput.Dir!= InputDir.Middle )
        {
            if (HandInput.Direction.x * _Actor.transform.localScale.x < 0)
            {
                Vector2 NewScale = _Actor.transform.localScale;
                NewScale.x = NewScale.x * -1;
                _Actor.TransCtrl.localScale = NewScale;
            }
            _Actor.RigidCtrl.velocity = Direction * Speed;
        }
    }

}
