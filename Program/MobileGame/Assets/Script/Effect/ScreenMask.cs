using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMask : MonoBehaviour {
    public Action onAnimEnd;
	// Use this for initialization
	
	// Update is called once per frame
	public void OnAnimEnd() {
        if(onAnimEnd!=null)
            onAnimEnd();
        onAnimEnd = null;
    }
}
