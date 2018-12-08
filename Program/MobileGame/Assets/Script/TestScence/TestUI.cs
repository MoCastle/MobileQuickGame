using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public struct TestBtn
{
    public Button Btn;
    public Text Text;
    public GameObject Obj;
}
public class TestUI : MonoBehaviour {
    public TestBtn[] BtnArr;
    public void BttnEvent()
    {

    }
	// Use this for initialization
	void Start () {
        foreach( TestBtn btn in BtnArr )
        {
            
        }
            
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
