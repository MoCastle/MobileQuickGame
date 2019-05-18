using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class PlayerAction : BaseAction {
    protected InputInfo m_Input;
    protected PlayerObj _PlayerObj;
	public PlayerAction(BaseActorObj baseActorObj, SkillInfo skillInfo):base(baseActorObj, skillInfo)
    {
        _PlayerObj = baseActorObj as PlayerObj;
        SetStartDirection();
        _PlayerObj.ClearAnimParam();
    }
    public virtual void SetStartDirection()
    {
        if(m_DirLock)
        {
            return;
        }
        m_Input = _PlayerObj.CurOrder;
        Vector2 gestureDir = m_Input.vector;
        //方向设置
        switch (m_Input.gesture)
        {
            case HandGesture.Slip:

                //朝向设置
                if (gestureDir.x * _PlayerObj.transform.localScale.x < 0)
                {
                    Vector2 NewScale = _PlayerObj.transform.localScale;
                    NewScale.x = NewScale.x * -1;
                    _PlayerObj.TransCtrl.localScale = NewScale;
                }
                break;
            case HandGesture.Click:
                float dirX = 0;
                dirX = m_Input.endPS.x - Screen.width / 2;
                //如果计算距离够明显
                //(Mathf.Abs(dirX) > preInput.InputInfo.MaxDst * 0.1)&&
                if ((dirX * _PlayerObj.transform.localScale.x < 0))
                {
                    Vector2 NewScale = _PlayerObj.transform.localScale;
                    NewScale.x = NewScale.x * -1;
                    _PlayerObj.TransCtrl.localScale = NewScale;
                }
                /*
                //只检测之前的点击手势和划屏手势
                if(preInput.Gesture == HandGesture.Click )//|| preInput.Gesture == HandGesture.Slip)
                {
                    dirX = _Input.InputInfo.EndPs.x - preInput.InputInfo.EndPs.x;
                    //如果计算距离够明显
                    if ((Mathf.Abs(dirX) > preInput.InputInfo.MaxDst*0.1) && (dirX * baseActorObj.transform.localScale.x < 0) )
                    {
                        Vector2 NewScale = baseActorObj.transform.localScale;
                        NewScale.x = NewScale.x * -1;
                        baseActorObj.TransCtrl.localScale = NewScale;
                    }
                }
                */

        break;
        }
    }

    public virtual void InputNormInput(InputInfo curInput )
    {
        float xValue = 0;
        
        switch (curInput.gesture)
        {
            case HandGesture.Click:
                float dirX = 0;
                //xValue = curInput.InputInfo.EndPs.x - _Input.InputInfo.EndPs.x;
                xValue = m_Input.endPS.x - Screen.width / 2;
                break;
            case HandGesture.Drag:
            case HandGesture.Slip:
            case HandGesture.Holding:
                xValue = curInput.vector.x;
                break;
        }
        if(xValue!=0)
        {
            InputDirect(Vector2.right * xValue / Mathf.Abs(xValue));
        }
            
    }
}
