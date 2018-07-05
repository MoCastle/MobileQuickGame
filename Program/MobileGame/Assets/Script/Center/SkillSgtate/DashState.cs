using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState:PlayerState {
    float Speed = 20;
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
    
    public DashState(BaseActor Actor) : base(Actor)
    {
        _Actor.RigidCtrl.gravityScale = 0f;
        InputOrder = PlayerCtrl.CurOrder;
        //朝向设置
        if (InputOrder.InputInfo.Shift.x * _Actor.transform.localScale.x < 0)
        {
            Vector2 NewScale = _Actor.transform.localScale;
            NewScale.x = NewScale.x * -1;
            _Actor.TransCtrl.localScale = NewScale;
        }
        //旋转设置
        float Rotate = 0;
        Rotate = Mathf.Atan2(Direction.y, Mathf.Abs(Direction.x))*180/Mathf.PI;
        Debug.Log(Rotate);
        if(InputOrder.InputInfo.Shift.x < 0)
        {
            Rotate = Rotate * -1;
        }
        /*
        switch( InputOrder.Dir )
        {
            case InputDir.Up:
                Rotate = 90;
                break;
            case InputDir.Down:
                Rotate = -90;
                break;
            case InputDir.Left:
            case InputDir.Right:
                Rotate = 0;
                break;
            case InputDir.LeftUp:
            case InputDir.RightUp:
                Rotate = 45;
                break;
            case InputDir.LeftDown:
            case InputDir.RightDown:
                Rotate = -45;
                break;
        }*/
        Vector3 Rotation = Vector3.forward * Rotate;
        _Actor.ActorTransCtrl.eulerAngles = Rotation;
        GameObject Effect = EffectManager.Manager.GenEffect("chong_qibo");
    }

    public override void Update()
    {
        _Actor.RigidCtrl.velocity = Direction * Speed;
    }
}
