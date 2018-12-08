using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct PhysicStruct
{
    public float GravityScale;
    public Vector2 Velocity;
    public bool IsCoppying;
}

public class PhysicCtrler {
    PhysicStruct _PhysicData;
    Rigidbody2D _Rigid2D;
    BaseActorObj _Actior;
    public Vector2 GetSpeed
    {
        get
        {
           if(_PhysicData.IsCoppying)
            {
                return _PhysicData.Velocity;
            }
            return _Rigid2D.velocity;
        }
    }
	public PhysicCtrler(BaseActorObj actor)
    {
        _Rigid2D = actor.GetComponent<Rigidbody2D>();
        _Actior = actor;
    }

    public void SetSpeed( Vector2 speed )
    {
        _Rigid2D.velocity = speed;
    }
    public void SetSpeed(float speed)
    {
        Vector2 dir = Vector2.right * _Actior.transform.localScale.x;
        _Rigid2D.velocity = dir*speed;
    }

    public float Gravity
    {
        get
        {
            return _Rigid2D.gravityScale;
        }
        set
        {
            _Rigid2D.gravityScale = value;
        }
    }

    public void Update()
    {
        
    }
    public void CopyData()
    {
        if(!_PhysicData.IsCoppying)
        {
            _PhysicData.IsCoppying = true;
            _PhysicData.GravityScale = _Rigid2D.gravityScale;
            _PhysicData.Velocity = _Rigid2D.velocity;

            _Rigid2D.velocity = Vector2.zero;
            _Rigid2D.gravityScale = 0;
        }
        
    }
    public void ResetData()
    {
        if(_PhysicData.IsCoppying)
        {
            _PhysicData.IsCoppying = false;
            _Rigid2D.gravityScale = _PhysicData.GravityScale;
            _Rigid2D.velocity = _PhysicData.Velocity;
        }
        
    }
}
