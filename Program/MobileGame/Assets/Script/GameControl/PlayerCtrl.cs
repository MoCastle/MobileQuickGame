using System.Collections;
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
    Realease,
    Touching
}

public struct InputInfo
{

    public HandGesture gesture;

    public Vector2 vector;
    public bool isLegal;
    public Vector2 startPS;
    public Vector2 endPS;


    public InputDir directionEnum
    {
        get
        {
            InputDir dir;
            if (isLegal == false)
            {
                dir = InputDir.Middle;
            }
            else
            {
                Vector2 direction = Vector2.zero;
                float abY = Mathf.Abs(vector.y);
                float abX = Mathf.Abs(vector.x);
                float Rate = abY / (abX>0.0001f?abX:0.0001f);
                if(abY<0.0001f&&abX<0.0001f)
                {
                    direction.x = 0;
                    direction.y = 0;
                }
                else if (Rate < (4f / 3f) && Rate > (3f / 4f))
                {
                    direction.x = 0.5f;
                    direction.y = 0.5f;
                }
                else if (Rate > (4f / 3f))
                {
                    direction.y = 1;
                    direction.x = 0;
                }
                else
                {
                    direction.x = 1;
                    direction.y = 0;
                }
                if (vector.y < 0)
                {
                    direction.y = direction.y * -1;
                }
                if (vector.x < 0)
                {
                    direction.x = direction.x * -1;
                }
                int dirNum = (int)InputDir.Middle;
                if (direction.x > 0.1)
                {
                    dirNum = dirNum + (int)InputDir.Right;
                }
                else if (direction.x < -0.1)
                {
                    dirNum = dirNum + (int)InputDir.Left;
                }
                if (direction.y > 0.1)
                {
                    dirNum = dirNum + (int)InputDir.Up;
                }
                else if (direction.y < -0.1)
                {
                    dirNum = dirNum + (int)InputDir.Down;
                }
                dir = (InputDir)dirNum;
            }
            return dir;

        }
    }
    public InputDir GetHoriEnum()
    {

        int numDir = (int)directionEnum;
        numDir = numDir / 10 * 10;
        return (InputDir)numDir;
    }
    public InputDir GetVertEnum()
    {
        int numDir = (int)directionEnum;
        numDir = numDir % 10;
        return (InputDir)numDir;
    }
}

public static class PlayerCtrl
{

    static HandGesture curHandGesture;
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
    public delegate void Input(InputInfo Input);
    public static void AddInputEvent(Input InFunction)
    {
        InputEvent += InFunction;
    }

    public static InputInfo CurOrder;
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

    static float CountTime = 0;
    public static StructRoundArr<InputInfo> InputRoundArr = new StructRoundArr<InputInfo>(2);

    public static void InputHandTouch(InputInfo Input)
    {
        if (Input.isLegal && !GmCtrler.IsPaused)
        {
            InputInfo NewInput = Input;
            if (InputEvent != null)
                InputEvent(NewInput);
            //松手的指令也需要缓存
            InputRoundArr.Push(NewInput);
        }

    }
    public static void RefreshInputRoundArr()
    {
        InputRoundArr = new StructRoundArr<InputInfo>(2);
    }

}

