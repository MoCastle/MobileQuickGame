using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMask : MonoBehaviour {
    public Action onAnimEnd;
    Animator animator;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Use this for initialization

    public void OnAnimEnd() {
        if(onAnimEnd!=null)
            onAnimEnd();
        onAnimEnd = null;
    }
}
