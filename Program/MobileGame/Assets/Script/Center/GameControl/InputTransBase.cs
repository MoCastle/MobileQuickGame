using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputTransBase {
    public abstract void InputHoldPush( float Time );//长按
    public abstract void Drag( Vector2 Shift );//拽
    public abstract void Click( );//点击
    public abstract void Swop();//滑
}
