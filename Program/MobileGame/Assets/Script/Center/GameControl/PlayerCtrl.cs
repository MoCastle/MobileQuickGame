using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl {
    Rigidbody2D PlayerRigid;
    static PlayerCtrl _PlayerCtrl;
    public static PlayerCtrl PlayerCtrler
    {
        get
        {
            if(_PlayerCtrl == null )
            {
                _PlayerCtrl = new PlayerCtrl();
            }
            return _PlayerCtrl;
        }
    }

    public void SetPlayer(Rigidbody2D InputPlayerRigid)
    {
        PlayerRigid = InputPlayerRigid;
    }

    public void Move( Vector2 Shift )
    {
        PlayerRigid.velocity = Shift/10; 
    }
}

