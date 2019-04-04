using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    private Animator m_Animator;
    public Animator Animator
    {
        get
        {
            if (!m_Animator)
            {
                m_Animator = GetComponent<Animator>();
            }
            return m_Animator;
        }
    }

    public void SetBool(string name, Boolean value)
    {
        Animator.SetBool(name, value);
    }

    public event Action<float> OnEnterHardTime;
    public event Action<float> OnSetSpeed;
    public event Action<float> OnSetImdVSpeed;
    public event Action<float> OnSetImdHSpeed;
    public event Action<float> OnSetInAirImdVSpeed;
    public event Action<float> OnSetInAirImdHSpeed;
    public event Action<float> OnStopMove;
    public event Action<int> OnSetFaceLock;
    public event Action<float> OnMoveActor;
    public event Action OnSwitchAction;
    public event Action<string> OnCallPuppet;
    public event Action OnBeBreak;
    public event Action OnLeaveComplete;
    public event Action OnLeave;

    //通知硬直事件
    public void HardTime(float hardTime)
    {
        if (OnEnterHardTime != null)
            OnEnterHardTime(hardTime);
    }

    //设置面朝方向的速度 匀速运动
    public void SetSpeed(float speed)
    {
        if (OnSetSpeed != null)
            OnSetSpeed(speed);
    }

    //设置瞬时垂直速度
    public void SetImdVSpeed(float speed)
    {
        if (OnSetImdVSpeed != null)
            OnSetImdVSpeed(speed);
    }

    //设置瞬时水平速度
    public void SetImdHSpeed(float speed)
    {
        //m_Physic.SetSpeed(new Vector2(speed, m_Physic.MoveSpeed.y));
        if (OnSetImdHSpeed != null)
            OnSetImdHSpeed(speed);
    }

    //设置空中瞬时垂直速度
    public void SetInAirImdVSpeed(float speed)
    {
        if (OnSetInAirImdVSpeed != null)
            OnSetInAirImdVSpeed(speed);
    }
    
    //设置空中瞬时水平速度
    public void SetInAirImdHSpeed(float speed)
    {
        if(OnSetInAirImdHSpeed!=null)
            OnSetInAirImdHSpeed(speed);
    }

    //停止运动
    public void StopMove(float speed)
    {
        if (OnStopMove != null)
            OnStopMove(speed);
    }
    
    //禁止转向
    public void SetFaceLock(int ifLock)
    {
        if (OnSetFaceLock != null)
            OnSetFaceLock(ifLock);
    }

    //瞬移
    public void MoveActor(float distance)
    {
        if (OnMoveActor != null)
            OnMoveActor(distance);
    }
    
    
    
    //召唤
    public virtual void CallPuppet(string objName)
    {
        if (OnCallPuppet != null)
            OnCallPuppet(objName);
    }

    //转换状态
    public virtual void SwitchAction()
    {
        if (OnSwitchAction != null)
            OnSwitchAction();
    }
    public void TestEvent( string str )
    { }
}
