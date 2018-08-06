using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAdaptor {
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
            if( value != _IsRuning)
            {
                _Anim.SetBool("Run", value);
                _IsRuning = value;
            }
        }
    }

    public AnimAdaptor( Animator InAnim )
    {
        _Anim = InAnim;
    }
}
