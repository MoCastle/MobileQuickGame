using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour {
    bool _isOnGround = false;
    Rigidbody2D _Rigidbody;
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
    Transform _PlayerTtransform;
    public Transform PlayerTransform
    {
        get
        {
            return _PlayerTtransform;
        }
    }
	// Use this for initialization
	void Awake () {
        Debug.Log("PlayerActor Is Initing");
        _FootChecker = transform.FindChild("footcheck");
        _Rigidbody = GetComponent<Rigidbody2D>();
        _PlayerTtransform = GetComponent<Transform>();
        _PlayerAnimator = GetComponentInChildren<Animator>();
        if( null == _PlayerAnimator )
        {
            Debug.Log("PlayerActor:init EmptyAnimator");
        }
        Debug.Log("PlayerActor Initing Complete");
    }
	
	// Update is called once per frame
	void Update () {
        _isOnGround = Physics2D.OverlapBox(Vector2.down, Vector2.one, 0);
	}
    public void Move(Vector2 Shift)
    {
        _Rigidbody.velocity = Shift*5;
    }
    public void Dash( Vector2 Shift )
    {
        _Rigidbody.velocity = Shift*15;
    }
    public void NoAction( )
    {
        _Rigidbody.velocity = Vector2.zero;
    }
}
