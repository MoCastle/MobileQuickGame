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
    float _OrGravity;
    public float OrGravity
    {
        get
        {
            return _OrGravity;
        }
    }
    public void SetGravity(float gravity)
    {
        _Rigid2D.gravityScale = gravity;
        _LowGravity = true;
    }
    public void ResetGravity()
    {
        _Rigid2D.gravityScale = _OrGravity;
        _LowGravity = false;
    }
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
        _OrGravity = _Rigid2D.gravityScale;
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
    bool _LowGravity;
    public void Update()
    {
        if(_Actior.IsOnGround)
        {
            if(_LowGravity)
            {
                ResetGravity();
            }
            return;
        }
        if(_PhysicData.IsCoppying)
        {
            return;
        }
        if (!_LowGravity && Mathf.Abs(_Rigid2D.velocity.y) < 10)
        {
            SetGravity(OrGravity / 5);
        }
        else if (_LowGravity)
        {
            ResetGravity();
        }
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
