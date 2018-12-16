using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct TestStruct
{
    public float Value;
}
[System.Serializable]
public class ViewStruct
{
    public TestStruct TestStruct;
    public float ShowValue;
    ViewStruct()
    {
        ShowValue = TestStruct.Value;
    }
}
public class Test : MonoBehaviour {
    public ViewStruct ShowStruct;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
