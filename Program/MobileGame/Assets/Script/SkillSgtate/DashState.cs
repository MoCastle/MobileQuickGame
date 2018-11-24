using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState:PlayerState {
    protected float Speed = 5.5f;
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.Dash;
        }
    }
    NormInput InputOrder;
    public Vector2 Direction
    {
        get
        {
            Vector2 CurDirection = InputOrder.InputInfo.Shift;
            if( _Actor.IsOnGround && CurDirection.y<0)
            {
                CurDirection.y = 0;
            }
            return CurDirection.normalized;
        }
    }
    
    public DashState(PlayerActor Actor) : base(Actor)
    {
        Actor.ClosePlatFoot();
        Actor.Dashed = Actor.Dashed + 1;
        _Actor.RigidCtrl.gravityScale = 0f;
        InputOrder = Actor.GetTempInput(HandGesture.Slip);
        //朝向设置
        if (InputOrder.InputInfo.Shift.x * _Actor.transform.localScale.x < 0)
        {
            Vector2 NewScale = _Actor.transform.localScale;
            NewScale.x = NewScale.x * -1;
            _Actor.TransCtrl.localScale = NewScale;
        }
        RotateToDirection(Direction);
        GameObject Effect = EffectManager.Manager.GenEffect("chong_qibo");
        Effect.transform.position = _Actor.TransCtrl.position;
        _Actor.IsHoly = true;
    }

    public override void Update()
    {
        _Actor.RigidCtrl.velocity = Direction * Speed;
    }
}
