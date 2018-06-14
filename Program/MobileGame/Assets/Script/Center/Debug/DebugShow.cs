using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugShow : MonoBehaviour {
    public Text OutPutText;
	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        OutPutText.text = LogMgr.LogString;
    }
}
