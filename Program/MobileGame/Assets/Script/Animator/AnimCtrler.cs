using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrler {
    int _CurAnimName = 0;
    public int CurAnimName
    {
        get
        {
            return _CurAnimName;
        }
    }


    Animator _Anim;
    public bool _IsRuning = false;
    public bool IsRuning
    {
        get
        {
            return _IsRuning;
        }
        set
        {
            if (value != _IsRuning)
            {
                _Anim.SetBool("Run", value);
                _IsRuning = value;
            }
        }
    }


    public void Update()
    {
        //CurAnimName != _Anim.GetCurrentAnimatorStateInfo(0).nameHash
    }
}
