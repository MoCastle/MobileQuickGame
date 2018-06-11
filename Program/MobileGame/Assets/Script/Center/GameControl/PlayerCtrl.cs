using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl {
    Rigidbody2D PlayerRigid;
    PlayerActor Player;
    public float MineDashSpeed = 10;
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
        Player = PlayerRigid.GetComponent<PlayerActor>();
    }

    public void InputHandTouch( Vector2 Shift, bool IsHandOn )
    {
        if (IsHandOn)
        {
            Shift.y = 0;
            Shift = Shift / 10;
            Player.Move(Shift);
        }
        else
        {
            if( Shift.sqrMagnitude > MineDashSpeed* MineDashSpeed)
            {
                Player.Dash(Shift);
            }
            
        }
    }

}

