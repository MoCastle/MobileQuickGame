using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour {
    public GameObject TraceObj;
    public float SmoothTime=1;
    Vector3 SmoothSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition = Vector3.SmoothDamp(transform.position,TraceObj.transform.position,ref SmoothSpeed, SmoothTime);
        newPosition.z = transform.position.z;
        transform.position = newPosition;

    }
}
