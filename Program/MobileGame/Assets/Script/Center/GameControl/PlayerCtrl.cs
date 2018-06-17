using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerCtrl {

    public static StructRoundArr<InputInfo> InputRoundArr = new StructRoundArr<InputInfo>(2);

    public static void InputHandTouch( InputInfo Input )
    {
        if( Input.IsLegal )
        {
            InputRoundArr.Push(Input);
        }
    }
    public static void RefreshInputRoundArr()
    {
        InputRoundArr = new StructRoundArr<InputInfo>( 2 );
    }
}

