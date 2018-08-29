using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : PlayerState
{
    float CountTime;
    
    public override SkillEnum SkillType
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }
    public ChargeState(PlayerActor InActor) : base(InActor)
    {
        Actor = InActor;
        CountTime = Time.time;
        if( !_Actor.IsOnGround )
        {
            Vector2 OldVector = _Actor.RigidCtrl.velocity;

            PlayerActor Player = (PlayerActor)_Actor;
            OldVector.y = Player.ChargeAddSpeed;
            _Actor.RigidCtrl.velocity = OldVector;
            _Actor.RigidCtrl.gravityScale = _Actor.RigidCtrl.gravityScale * 0.5f;
        }
        
    }
    public override void Update()
    {
        NormInput Input = Actor.CurInput;
        if (Input.Dir != InputDir.Middle)
        {
            if (Input.Direction.x * _Actor.transform.localScale.x < 0)
            {
                Vector2 NewScale = _Actor.transform.localScale;
                NewScale.x = NewScale.x * -1;
                _Actor.TransCtrl.localScale = NewScale;
            }
        }
        base.Update();
    }
}
