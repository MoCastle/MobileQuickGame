using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDelegate {
    static PlayerDelegate _Delegate;
    public static PlayerDelegate Deleg
    {
        get
        {
            if( _Delegate == null )
            {
                _Delegate = new PlayerDelegate();
            }
            return _Delegate;
        }
    }
    //玩家属性
    Propty PlayerPropty;
    public static void SetPropty( Propty ResetProptu )
    {
    }
    public static Propty GetPropty()
    {
        return Deleg.PlayerPropty;
    }

    // Use this for initialization
    PlayerDelegate( )
    {
        //PlayerPropty = new Propty(1);
    }
}
