using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActor : MonoBehaviour {
    Animator _AnimCtrl;
    public Animator AnimCtrl
    {
        get
        {
            if( _AnimCtrl == null )
            {
                _AnimCtrl = GetComponentInChildren<Animator>();
            }
            return _AnimCtrl;
        }
    }
    public Transform TransCtrl
    {
        get
        {
            return transform;
        }
    }
    Rigidbody2D _RigidCtrl;
    public Rigidbody2D RigidCtrl
    {
        get
        {
            if( _RigidCtrl == null )
            {
                _RigidCtrl = GetComponent<Rigidbody2D>();
            }
            return _RigidCtrl;
        }
    }
    Transform _FootTransCtrl;
    public Transform FootTransCtrl
    {
        get
        {
            if( _FootTransCtrl == null )
            {
                _FootTransCtrl = TransCtrl.FindChild("FootCheck");
            }
            return _FootTransCtrl;
        }
    }

    BaseState _PlayerState;
    public BaseState PlayerState
    {
        get
        {
            if( _PlayerState == null )
            {
                _PlayerState = new InitState( this );
            }
            return _PlayerState;
        }
        set
        {
            _PlayerState = value;
        }
    }
    bool _IsOnGround = false;
    public virtual bool IsOnGround
    {
        get { return _IsOnGround; }
        set { _IsOnGround = value; }
    }

    public virtual void LogicUpdate( )
    {
    }
    public void Update()
    {
        PlayerState.Update();
        LogicUpdate();

    }
}
