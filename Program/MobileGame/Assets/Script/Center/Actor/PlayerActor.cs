using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour {
    bool _isOnGround = false;
    Rigidbody2D _Rigidbody;
    public bool IsOnGround
    {
        get
        {
            return _isOnGround;
        }
    } 
    Transform _FootChecker;
	// Use this for initialization
	void Start () {
        _FootChecker = transform.FindChild("footcheck");
        _Rigidbody = GetComponent<Rigidbody2D>();
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
