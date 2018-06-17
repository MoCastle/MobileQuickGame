using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : BaseActor {
    bool _isOnGround = false;
    Animator _PlayerAnimator;
    public Animator PlayerAnimator
    {
        get
        {
            return _PlayerAnimator;
        }
    }
    public bool IsOnGround
    {
        get
        {
            return _isOnGround;
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
        _isOnGround = Physics2D.OverlapBox(Vector2.down, Vector2.one, 0);
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
