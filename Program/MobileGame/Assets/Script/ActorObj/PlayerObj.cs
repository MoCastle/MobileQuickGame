using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace GameScene
{
    public class PlayerObj : BaseActorObj
    {
        #region 内部属性
        InputInfo m_CurOrder;
        #endregion

        #region 对外接口
        public InputInfo CurOrder
        {
            get
            {
                return m_CurOrder;
            }
        }
        #endregion
        protected override void Init()
        {
            m_IDLayer = 1 << LayerMask.NameToLayer("Enemy");
            m_ActionCtrler = new ActionCtrler(this, m_CharacterAnim.Animator);//, info.ActorActionList);

        }
        #region 手势获取

        //通过手势获取输入
        public InputInfo GetInputByGesture(HandGesture gesture)
        {
            return LegalnputDict[gesture];
        }
        Dictionary<HandGesture, InputInfo> LegalnputDict = new Dictionary<HandGesture, InputInfo>();
        public override void EnterState()
        {
            //ClearAnimParam();
        }
        public virtual void Input(InputInfo input)
        {
            if (input.gesture != HandGesture.None)
            {
                m_CurOrder = input;
                PlayerAction action = ActionCtrl.CurAction as PlayerAction;
                if (action != null)
                {
                    action.InputNormInput(input);
                }
                SetAnimParam(input);
                PlayerCtrl.CurOrder = input;
            }
            else
                ClearAnimParam();

        }

        public void SetAnimParam(InputInfo handInput)
        {
            ClearAnimParam();
            if (handInput.directionEnum != InputDir.Middle)
            {
                int InputNum = (int)handInput.directionEnum;
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
            switch (handInput.gesture)
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
                case HandGesture.Realease:
                    ActionCtrl.SetBool("Hand_Release", true);
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
            ActionCtrl.SetBool("Hand_Release", false);
        }

        public override void LogicUpdate()
        {

            if (Alive)
            {
                InputInfo HandInput = PlayerCtrl.InputRoundArr.Pop();
                if (HandInput.isLegal)
                {
                    Input(HandInput);
                }
            }
        }
        #endregion

        #region 事件
        public override void SwitchAction(BaseAction baseAction)
        {
            base.SwitchAction(baseAction);
            ClearAnimParam();
        }

        #endregion
    }
}

