using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState:BaseState {
    public float ContinueTime = 0.25f;
    public float StartTime;
    public bool IsInited = false;
    public Vector2 _Direction;
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.Dash;
        }
    }
    public DashState(BaseActor Actor) : base(Actor)
    {
        Debug.Log("DashState");
        _Actor.AnimCtrl.SetTrigger("Dash");
    }
    public void InputDirection( Vector2 Direction )
    {
        //朝向设置
        if( Mathf.Abs( Direction.x ) > 0.1 )
        {
            if( _Actor.TransCtrl.localScale.x * Direction.x < 0)
            {
                Vector2 NewVector = _Actor.TransCtrl.localScale;
                NewVector.x = NewVector.x * -1;
                _Actor.TransCtrl.localScale = NewVector;
            }
        }
        
        //角度设置
        if( Mathf.Abs( Direction.y ) > 0 )
        {
            float Rotate = 45;
            if (Mathf.Abs(Direction.y) > 0.6)
            {
                Rotate = 90;
            }
            
            if( Direction.y < 0 )
            {
                Rotate = Rotate * -1;
            }
            Vector3 Rotation = Vector3.forward * Rotate;

            _Actor.ActorTransCtrl.localEulerAngles = Rotation;
        }
        StartTime = Time.time;
        
        _Direction = Direction;
        IsInited = true;
    }
    public override void Input(InputInfo Input)
    {

    }

    public override void Update()
    {
        
        if ( IsInited )
        {
            if(StartTime + ContinueTime > Time.time)
            {
                _Actor.RigidCtrl.velocity = _Direction * 80;
            }
            else
            {
                _Actor.ActorState = new InitState(_Actor);
                _Actor.ActorTransCtrl.localEulerAngles = Vector3.zero;
            }
            
           
        }
    }
}
