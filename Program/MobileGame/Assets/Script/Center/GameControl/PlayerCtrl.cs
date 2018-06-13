using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl {
    public Rigidbody2D PlayerRigid;
    PlayerActor Player;
    BaseState _PlayerState;
    public BaseState PlayerState
    {
        get
        {
            return _PlayerState;
        }
        set
        {
            _PlayerState = value;
        }
    }
    public StructRoundArr<InputInfo> InputRoundArr;
    float PressingTime;
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

    public PlayerCtrl()
    {
        
        InputRoundArr = new StructRoundArr<InputInfo>(2);
        GameWorldTimer.GameInBattleEvent += Update;
    }
    public void Update( )
    {
        
        if( PlayerState != null )
        {
            PlayerState.Update();
        }
    }

    public void SetPlayer(Rigidbody2D InputPlayerRigid)
    {
        PlayerRigid = InputPlayerRigid;
        Player = PlayerRigid.GetComponent<PlayerActor>();
        PlayerState = new InitState(this);
    }

    public void InputHandTouch( InputInfo Input )
    {
        if( Input.IsLegal )
        {
            InputRoundArr.Push(Input);
        }
    }

}

