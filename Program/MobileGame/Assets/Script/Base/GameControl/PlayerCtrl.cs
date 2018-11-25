﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InputDir
{
    Middle = 0,
    Up = 1,
    Down = 2,
    Left = 10,
    LeftUp = 11,
    LeftDown = 12,
    Right = 20,
    RightUp = 21,
    RightDown = 22
}
public enum HandGesture
{
    None,
    Holding,
    Drag,
    Slip,
    Click,
}

public struct NormInput
{

    public InputInfo InputInfo;
    public InputDir Dir;
    public HandGesture Gesture;
    public Vector2 Direction;
    public float LifeTime;

    public bool IsLegal
    {
        get
        {
            return InputInfo.IsLegal;
        }
    }
    public NormInput(InputInfo InInfo = new InputInfo( ), float InLifeTime = 0 )
    {
        InputInfo = InInfo;
        LifeTime = InLifeTime;
        
        if (InInfo.IsLegal == false )
        {
            Dir = InputDir.Middle;
            Direction = Vector2.zero;
            Gesture = HandGesture.None;
            Dir = InputDir.Middle;
            return;
        }

        if (InInfo.Percent < 0.1)
        {
            Dir = InputDir.Middle;
            Direction = Vector2.zero;
        }
        else
        {
            Direction = InInfo.Shift;
            float Rate = Mathf.Abs(InInfo.Shift.y / InInfo.Shift.x);
            if (Rate < (4f / 3f) && Rate > (3f / 4f))
            {
                Direction.x = 0.5f;
                Direction.y = 0.5f;
            }
            else if (Rate > (4f / 3f))
            {
                Direction.y = 1;
                Direction.x = 0;
            }
            else
            {
                Direction.x = 1;
                Direction.y = 0;
            }

            if (InInfo.Shift.y < 0)
            {
                Direction.y = Direction.y * -1;
            }
            if (InInfo.Shift.x < 0)
            {
                Direction.x = Direction.x * -1;
            }

            int DirNum = (int)InputDir.Middle;
            if ( Direction.x> 0.1)
            {
                DirNum = DirNum + (int)InputDir.Right;
            }else if(Direction.x < -0.1)
            {
                DirNum = DirNum + (int)InputDir.Left;
            }
            if( Direction.y > 0.1 )
            {
                DirNum = DirNum + (int)InputDir.Up;
            }else if (Direction.y < -0.1)
            {
                DirNum = DirNum + (int)InputDir.Down;
            }
            Dir = (InputDir)DirNum;
        }
        if (InputInfo.IsPushing)
        {
            Gesture = HandGesture.None;
            if ( Dir != InputDir.Middle )
            {
                Gesture = HandGesture.Drag;
            }
            else if( LifeTime > 0.30f )
            {
                Gesture = HandGesture.Holding;
            }
        }
        else
        {
            Gesture = HandGesture.None;
            if( Dir!= InputDir.Middle && InputInfo.Percent > 0.4 )
            {
                Gesture = HandGesture.Slip;
            }else if(LifeTime < 0.3)
            {
                Gesture = HandGesture.Click;
            }
        }
    }
    public InputDir GetHoriEnum( )
    {
        
        int NumDir = (int)Dir;
        NumDir = NumDir / 10 * 10;
        return (InputDir)NumDir;
    }
    public InputDir GetVertEnum( )
    {
        int NumDir = (int)Dir;
        NumDir = NumDir % 10;
        return (InputDir)NumDir;
    }
}

public static class PlayerCtrl {

    //游戏控制器
    static GameCtrl _GmCtrler;
    public static GameCtrl GmCtrler
    {
        get
        {
            if (_GmCtrler == null)
            {
                _GmCtrler = GameCtrl.GameCtrler;
            }
            return _GmCtrler;
        }
    }

    public static event Input InputEvent;
    public delegate void Input(NormInput Input);
    public static void AddInputEvent(Input InFunction)
    {
        InputEvent += InFunction;
    }

    public static NormInput CurOrder;
    //手指放入事件
    public static event InputFunc FingerOn;
    //手指挪开事件
    static event InputFunc _FingerOff;
    public static void AddFingerOff(InputFunc Func)
    {
        _FingerOff += Func;
    }
    public static void RemoveFingerOff(InputFunc Func)
    {
        _FingerOff -= Func;
    }

    public delegate void InputFunc();
    static private bool _IsInputing;
    static public bool IsInputing
    {
        get
        {
            return _IsInputing;
        }
        set
        {
            if( value != _IsInputing)
            {
                if(value)
                {
                    CountTime = Time.time;
                    if(FingerOn!=null)
                    {
                        FingerOn();
                    }
                }
                else
                {
                    CountTime = 0;
                    if (_FingerOff != null)
                    {
                        _FingerOff();
                    }
                }
                _IsInputing = value;
            }
        }
    }
    static float CountTime = 0;
    public static StructRoundArr<NormInput> InputRoundArr = new StructRoundArr<NormInput>(2);

    public static void InputHandTouch( InputInfo Input )
    {
        if( Input.IsLegal && !GmCtrler.IsPaused)
        {
            if( Input.IsPushing )
            {
                IsInputing = true;
                
            }
            NormInput NewInput = new NormInput(Input,Time.time - CountTime);
            InputEvent(NewInput);
            /*
            if (NewInput.Gesture != HandGesture.None)
            {
                InputRoundArr.Push(NewInput);
            }*/
            //松手的指令也需要缓存
            InputRoundArr.Push(NewInput);
            if ( !Input.IsPushing )
            {
                IsInputing = false;
            }
        }
    }
    public static void RefreshInputRoundArr()
    {
        InputRoundArr = new StructRoundArr<NormInput>( 2 );
    }

}
