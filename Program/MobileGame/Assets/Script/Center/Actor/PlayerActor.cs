using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActEnum
{
    Hand_Drag,
    Hand_Click,
    Hand_Slip,
    Hand_Holding,
    Dir_Left,
    Dir_Right,
    Dir_Up,
    InputPercent
}

public class PlayerActor : BaseActor {
    public float MoveVector;
    public float ChargeAddSpeed;
    NormInput _CurInput;
    public NormInput CurInput
    {
        get
        {
            return _CurInput;
        }
    }
    // Use this for initialization
    public override void LogicAwake(){
    }
	
    public void FireOff()
    {
        if( ActorState.SkillType == SkillEnum.RocketCut )
        {
            RocketCutState State = (RocketCutState)ActorState;
            State.FireOff();
        }
    }
    public void MoveForward()
    {
        Vector3 OldPst = TransCtrl.position;
        OldPst.x = OldPst.x + MoveVector;
        TransCtrl.position = OldPst;
    }
    public override void SwitchState()
    {
        base.SwitchState();
        ClearAnimParam();
    }
    public void ClearAnimParam()
    {
        this.AnimCtrl.SetBool("Hand_Drag", false);
        this.AnimCtrl.SetBool("Hand_Click", false);
        this.AnimCtrl.SetBool("Hand_Slip", false);
        this.AnimCtrl.SetBool("Hand_Holding", false);
        this.AnimCtrl.SetBool("Dir_Left", false);
        this.AnimCtrl.SetBool("Dir_Right", false);
        this.AnimCtrl.SetBool("Dir_Down", false);
        this.AnimCtrl.SetBool("Dir_Up", false);
        this.AnimCtrl.SetFloat("InputPercent", 0f);
        this.AnimCtrl.SetFloat("XInputPercent", 0f);
        this.AnimCtrl.SetFloat("YInputPercent", 0f);
    }
    public void SetAnimParam(NormInput HandInput)
    {
        ClearAnimParam();
        this.AnimCtrl.SetFloat("XInputPercent", HandInput.InputInfo.XPercent);
        this.AnimCtrl.SetFloat("XInputPercent", HandInput.InputInfo.YPercent);
        if (HandInput.Dir != InputDir.Middle)
        {
            int InputNum = (int)HandInput.Dir;
            //将逻辑枚举拆分成
            switch (HandInput.GetHoriEnum())
            {
                case InputDir.Left:
                    this.AnimCtrl.SetBool("Dir_Left", true);
                    break;
                case InputDir.Right:
                    this.AnimCtrl.SetBool("Dir_Right", true);
                    break;
                default:
                    this.AnimCtrl.SetBool("Dir_Right", false);
                    this.AnimCtrl.SetBool("Dir_Left", false);
                    break;
            }
            switch (HandInput.GetVertEnum())
            {
                case InputDir.Up:
                    this.AnimCtrl.SetBool("Dir_Up", true);
                    break;
                case InputDir.Down:
                    this.AnimCtrl.SetBool("Dir_Down", true);
                    break;
                default:
                    this.AnimCtrl.SetBool("Dir_Up", false);
                    this.AnimCtrl.SetBool("Dir_Down", false);
                    break;
            }
        }
        switch (HandInput.Gesture)
        {
            case HandGesture.Click:
                this.AnimCtrl.SetBool("Hand_Click", true);
                break;
            case HandGesture.Drag:
                this.AnimCtrl.SetBool("Hand_Drag", true);
                break;
            case HandGesture.Slip:
                this.AnimCtrl.SetBool("Hand_Slip", true);
                break;
            case HandGesture.Holding:
                this.AnimCtrl.SetBool("Hand_Holding", true);
                break;
            default:
                this.AnimCtrl.SetBool("Hand_Drag", false);
                this.AnimCtrl.SetBool("Hand_Click", false);
                this.AnimCtrl.SetBool("Hand_Slip", false);
                this.AnimCtrl.SetBool("Hand_Holding", false);
                break;
        }
    }
    public virtual void Input(NormInput Input)
    {
        _CurInput = Input;
        SetAnimParam(Input);
        PlayerCtrl.CurOrder = Input;
    }
    

    public override void LogicUpdate()
    {
        NormInput HandInput = PlayerCtrl.InputRoundArr.Pop();
        Input(HandInput);
    }
}
