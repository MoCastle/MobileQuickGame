using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    #region 内部参数
    private Animator m_Animator;
    #endregion
    #region 接口
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
    public event Action<Vector2> OnSetImdMoveSpeed;
    public event Action<Vector2> OnSetDirMoveSpeed;
    #endregion
    #region 动画
    public void SetBool(string name, Boolean value)
    {
        Animator.SetBool(name, value);
    }

    public void SetTrigger(string name)
    {
        Animator.SetTrigger(name);
    }
    #endregion
    #region 运动
    //瞬移
    public void MoveActor(float distance)
    {
        if (OnMoveActor != null)
            OnMoveActor(distance);
    }

    //禁止转向
    public void SetFaceLock(int ifLock)
    {
        if (OnSetFaceLock != null)
            OnSetFaceLock(ifLock);
    }

    #endregion
    #region 匀速运动
    //设置面朝方向的速度 匀速运动
    public void SetSpeed(float speed)
    {
        if (OnSetSpeed != null)
            OnSetSpeed(speed);
    }

    //停止运动 并设置一个向前的瞬时速度作为终结
    public void StopMove(float speed)
    {
        if (OnStopMove != null)
            OnStopMove(speed);
    }
    /// <summary>
    /// 带方向的匀速运动 运动横向的正方向以角色朝向为正
    /// </summary>
    /// <param name="speed"> 表示vector的字串 必须是 速度#速度 不然会出错 </param>
    public void SetDirSpeed(string speedStr)
    {
        if(speedStr == null || speedStr == "")
        {
            return;
        }
        string[] speedList = speedStr.Split('#');
        Vector2 moveSpeed = new Vector2(Convert.ToSingle(speedList[0]), Convert.ToSingle(speedList[1]));
        if (OnSetDirMoveSpeed != null)
            OnSetDirMoveSpeed(moveSpeed);
    }
    #endregion
    #region 瞬时速度
    /// <summary>
    /// 设置面向某个方向的瞬时移动
    /// </summary>
    /// <param name="vectorStr">< x方向#y方向 例"123.1#321.3" /param>
    public virtual void SetImdMoveSpeed(string vectorStr)
    {
        if (vectorStr == "" || vectorStr == null)
        {
            return;
        }
        string[] speed = vectorStr.Split('#');
        Vector2 vector = new Vector2(Convert.ToSingle(speed[1]), Convert.ToSingle(speed[2]));
        if (OnSetImdMoveSpeed != null)
            OnSetImdMoveSpeed(vector);
    }

    //设置空中瞬时水平速度
    public void SetInAirImdHSpeed(float speed)
    {
        if (OnSetInAirImdHSpeed != null)
            OnSetInAirImdHSpeed(speed);
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
    #endregion
    #region 战斗
    //召唤
    public virtual void CallPuppet(string objName)
    {
        if (OnCallPuppet != null)
            OnCallPuppet(objName);
    }
    //进入硬直
    public void HardTime(float hardTime)
    {
        if (OnEnterHardTime != null)
            OnEnterHardTime(hardTime);
    }
    #endregion
}
