using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : BaseActor {
    Animator _PlayerAnimator;
    BoxCollider2D _ColliderCtrl;
    public BoxCollider2D ColliderCtrl
    {
        get
        {
            if( _ColliderCtrl == null )
            {
                _ColliderCtrl = GetComponent<BoxCollider2D>();
            }
            return _ColliderCtrl;
        }
    }
    public Animator PlayerAnimator
    {
        get
        {
            return _PlayerAnimator;
        }
    }

    Transform _FootChecker;
	// Use this for initialization
	void Awake () {
        Debug.Log("PlayerActor Is Initing");
        _FootChecker = transform.FindChild("footcheck");
        _PlayerAnimator = GetComponentInChildren<Animator>();
        if( null == _PlayerAnimator )
        {
            Debug.Log("PlayerActor:init EmptyAnimator");
        }
        Debug.Log("PlayerActor Initing Complete");
    }
	
	// Update is called once per frame
	public override void LogicUpdate() {
        float Width = ColliderCtrl.size.x;
        Vector2 Position = FootTransCtrl.position;
        Vector2 Size = Vector2.right * Width;
        Size.y = 0.5f;
        Collider2D Collider = Physics2D.OverlapBox(Position, Size, 0, 1);
        if( Collider )
        {
            IsOnGround = true;
        }
        else
        {
            IsOnGround = false;
        }
    }
    
    public void Dash( Vector2 Shift )
    {
        RigidCtrl.velocity = Shift*15;
    }
    public void NoAction( )
    {
        RigidCtrl.velocity = Vector2.zero;
    }
}
