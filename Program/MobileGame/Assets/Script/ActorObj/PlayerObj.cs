using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : BaseActorObj {


    protected override void LogicAwake()
    {
        _InputArr = new NormInput[2];
        PlayerCtrl.AddFingerOff(OnFingerOut);
        _IDLayer = 1 << LayerMask.NameToLayer("Enemy");
    }

    //手指挪开触发事件
    public void OnFingerOut()
    {
    }

    #region 手势获取
    //缓存对应动画的输入
    NormInput[] _InputArr;
    //当前输入指针
    int _CurInputPoint = 0;

    //前一个手势输入
    public NormInput PreInput
    {
        get
        {
            return _InputArr[(_CurInputPoint + 1) % 2];
        }
    }
    //存储
    public void TakeInput()
    {
        _CurInputPoint = (_CurInputPoint + 1) % 2;
    }

    //当前手势输入
    public NormInput CurInput
    {
        get
        {
            return _InputArr[_CurInputPoint];
        }
        
        set
        {
            //点击手势要单独处理 因为动作初始化时该手势的输入效果依赖于之前的点击效果
            if (value.Gesture == HandGesture.Click && CurInput.Gesture == HandGesture.Click)
            {
                if(Mathf.Abs(value.InputInfo.EndPs.x - CurInput.InputInfo.EndPs.x)> value.InputInfo.MaxDst*0.15)
                {
                    TakeInput();
                    _InputArr[_CurInputPoint] = value;
                    
                }
            }else
            {
                TakeInput();

                _InputArr[_CurInputPoint] = value;
                
            }
            
        }
    }
    //通过手势获取输入
    public NormInput GetInputByGesture( HandGesture gesture )
    {
        return LegalnputDict[gesture];
    }
    Dictionary<HandGesture, NormInput> LegalnputDict = new Dictionary<HandGesture, NormInput>();
    public override void EnterState()
    {
        //ClearAnimParam();
    }
    public virtual void Input(NormInput input)
    {
        if(input.Gesture !=HandGesture.None)
        {
            CurInput = input;

            PlayerAction action = ActionCtrl.CurAction as PlayerAction;
            if( action!=null )
            {
                action.InputNormInput(input);
            }
            SetAnimParam(input);
            PlayerCtrl.CurOrder = input;
        }else
            ClearAnimParam();

    }
    public void SetAnimParam(NormInput handInput)
    {
        ClearAnimParam();
        ActionCtrl.SetFloat("XInputPercent", handInput.InputInfo.XPercent);
        ActionCtrl.SetFloat("XInputPercent", handInput.InputInfo.YPercent);
        if (handInput.Dir != InputDir.Middle)
        {
            int InputNum = (int)handInput.Dir;
            //将逻辑枚举拆分成
            switch (handInput.GetHoriEnum())
            {
                case InputDir.Left:
                    ActionCtrl.SetBool("Dir_Left", true);
                    break;
                case InputDir.Right:
                    ActionCtrl.SetBool("Dir_Right", true);
                    break;
                default:
                    ActionCtrl.SetBool("Dir_Right", false);
                    ActionCtrl.SetBool("Dir_Left", false);
                    break;
            }
            switch (handInput.GetVertEnum())
            {
                case InputDir.Up:
                    ActionCtrl.SetBool("Dir_Up", true);
                    break;
                case InputDir.Down:
                    ActionCtrl.SetBool("Dir_Down", true);
                    break;
                default:
                    ActionCtrl.SetBool("Dir_Up", false);
                    ActionCtrl.SetBool("Dir_Down", false);
                    break;
            }
        }
        switch (handInput.Gesture)
        {
            case HandGesture.Click:
                ActionCtrl.SetBool("Hand_Click", true);
                break;
            case HandGesture.Drag:
                ActionCtrl.SetBool("Hand_Drag", true);
                break;
            case HandGesture.Slip:
                ActionCtrl.SetBool("Hand_Slip", true);
                break;
            case HandGesture.Holding:
                ActionCtrl.SetBool("Hand_Holding", true);
                break;
            default:
                ActionCtrl.SetBool("Hand_Drag", false);
                ActionCtrl.SetBool("Hand_Click", false);
                ActionCtrl.SetBool("Hand_Slip", false);
                ActionCtrl.SetBool("Hand_Holding", false);
                break;
        }
    }

    public void ClearAnimParam()
    {
        ActionCtrl.SetBool("Hand_Drag", false);
        ActionCtrl.SetBool("Hand_Click", false);
        ActionCtrl.SetBool("Hand_Slip", false);
        ActionCtrl.SetBool("Hand_Holding", false);
        ActionCtrl.SetBool("Dir_Left", false);
        ActionCtrl.SetBool("Dir_Right", false);
        ActionCtrl.SetBool("Dir_Down", false);
        ActionCtrl.SetBool("Dir_Up", false);
        ActionCtrl.SetFloat("XInputPercent", 0f);
        ActionCtrl.SetFloat("YInputPercent", 0f);
    }
    
    public override void LogicUpdate()
    {
        
        if (Alive)
        {
            NormInput HandInput = PlayerCtrl.InputRoundArr.Pop();
            if (HandInput.IsLegal)
            {
                Input(HandInput);
            }
            
            else
            {
                //ClearAnimParam();
            }

        }
    }
    #endregion

    #region 事件
    public override void SwitchAction()
    {
        //ClearAnimParam();
    }
    
    #endregion
}
