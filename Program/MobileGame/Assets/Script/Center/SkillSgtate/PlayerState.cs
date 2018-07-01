using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : BaseState {
    public PlayerState( BaseActor Actor ):base( Actor )
    {
        ClearAnimParam();
        _Actor.ActorTransCtrl.localEulerAngles = Vector3.zero;
        _Actor.RigidCtrl.gravityScale = _Actor.GetGravityScale;
    }
    public void ClearAnimParam()
    {
        _Actor.AnimCtrl.SetBool("Hand_Drag",false);
        _Actor.AnimCtrl.SetBool("Hand_Click", false);
        _Actor.AnimCtrl.SetBool("Hand_Slip", false);
        _Actor.AnimCtrl.SetBool("Hand_Holding", false);
        _Actor.AnimCtrl.SetBool("Dir_Left", false);
        _Actor.AnimCtrl.SetBool("Dir_Right", false);
        _Actor.AnimCtrl.SetBool("Dir_Down", false);
        _Actor.AnimCtrl.SetBool("Dir_Up", false);
        _Actor.AnimCtrl.SetFloat("InputPercent", 0f);
        _Actor.AnimCtrl.SetFloat("XInputPercent", 0f);
        _Actor.AnimCtrl.SetFloat("YInputPercent", 0f);
    }
    public void SetAnimParam( NormInput HandInput )
    {
        ClearAnimParam();
        _Actor.AnimCtrl.SetFloat("XInputPercent", HandInput.InputInfo.XPercent);
        _Actor.AnimCtrl.SetFloat("XInputPercent", HandInput.InputInfo.YPercent);
        if (HandInput.Dir != InputDir.Middle )
        {
            int InputNum = (int)HandInput.Dir;
            //将逻辑枚举拆分成
            InputDir Hori;
            InputDir Virt;
            switch ( HandInput.GetHoriEnum( ) )
            {
                case InputDir.Left:
                    _Actor.AnimCtrl.SetBool("Dir_Left", true);
                    break;
                case InputDir.Right:
                    _Actor.AnimCtrl.SetBool("Dir_Right", true);
                    break;
                default:
                    _Actor.AnimCtrl.SetBool("Dir_Right", false);
                    _Actor.AnimCtrl.SetBool("Dir_Left", false);
                    break;
            }
            switch( HandInput.GetVertEnum( ) )
            {
                case InputDir.Up:
                    _Actor.AnimCtrl.SetBool("Dir_Up", true);
                    break;
                case InputDir.Down:
                    _Actor.AnimCtrl.SetBool("Dir_Down", true);
                    break;
                default:
                    _Actor.AnimCtrl.SetBool("Dir_Up", false);
                    _Actor.AnimCtrl.SetBool("Dir_Down", false);
                    break;
            }
        }
        switch (HandInput.Gesture)
        {
            case HandGesture.Click:
                _Actor.AnimCtrl.SetBool("Hand_Click", true);
                break;
            case HandGesture.Drag:
                _Actor.AnimCtrl.SetBool("Hand_Drag", true);
                break;
            case HandGesture.Slip:
                _Actor.AnimCtrl.SetBool("Hand_Slip", true);
                break;
            case HandGesture.Holding:
                _Actor.AnimCtrl.SetBool("Hand_Holding", true);
                break;
            default:
                _Actor.AnimCtrl.SetBool("Hand_Drag", false);
                _Actor.AnimCtrl.SetBool("Hand_Click", false);
                _Actor.AnimCtrl.SetBool("Hand_Slip", false);
                _Actor.AnimCtrl.SetBool("Hand_Holding", false);
                break;
        }
    }
    public virtual void Input(NormInput Input)
    {
        SetAnimParam(Input);
        PlayerCtrl.CurOrder = Input;
    }

    public override void Update()
    {
        NormInput HandInput = PlayerCtrl.InputRoundArr.Pop();
        Input(HandInput);
    }
}
