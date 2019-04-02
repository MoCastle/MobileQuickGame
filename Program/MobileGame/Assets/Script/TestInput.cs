using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePhysic;

public class TestInput : MonoBehaviour {
    public PhysicComponent Physic;
    public float Up = 10;
    public float Left = -10;
    public float Right = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 Speed = new Vector2();
		if( Input.GetKey(KeyCode.D))
        {
            Speed.x = Right;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            Speed.x = Left;
        }
        if(Input.GetKey(KeyCode.W))
        {
            Speed.y = Up;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            Physic.LeavePlatform();
        }
        Physic.MoveSpeed = Speed;

    }
}
