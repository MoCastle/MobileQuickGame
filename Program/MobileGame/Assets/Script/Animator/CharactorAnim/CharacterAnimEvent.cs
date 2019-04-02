using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class CharacterAnimEvent : MonoBehaviour {
    //进入硬直
    public event Action<float> OnEnterHardTime;
    public event Action<float> OnSetSpeed;
    public event Action<float> OnSetImdVSpeed;

    //通知硬直事件
    public void HardTime(float hardTime)
    {
        //_ActionCtrler.CurAction.HardTime(hardTime);
        OnEnterHardTime(hardTime);
    }

    //设置运动速度 匀速运动
    public void SetSpeed(float speed)
    {
        //_ActionCtrler.CurAction.SetSpeed(speed);
        OnSetSpeed(speed);
    }

    //设置瞬时垂直速度
    public void SetImdVSpeed(float speed)
    {
        //m_Physic.SetSpeed(new Vector2(m_Physic.MoveSpeed.x, speed));
        OnSetImdVSpeed(speed);
    }

    //设置瞬时水平速度
    public void SetImdHSpeed(float speed)
    {
        //m_Physic.SetSpeed(new Vector2(speed, m_Physic.MoveSpeed.y));
    }

    //设置空中瞬时垂直速度
    public void SetInAirImdVSpeed(float speed)
    {
        //m_Physic.SetSpeed(new Vector2(m_Physic.MoveSpeed.x, speed));
    }
    //设置空中瞬时水平速度
    public void SetInAirImdHSpeed(float speed)
    {
        //m_Physic.SetSpeed(new Vector2(speed, m_Physic.MoveSpeed.y));
    }

    public void StopMove(float speed)
    {
        /*
        if (-0.01f < speed && speed < 0.01f)
            _ActionCtrler.CurAction.SetSpeed(0);
        else
            _ActionCtrler.CurAction.SetFinalSpeed(speed);
            */
    }
    public void SetFaceLock(int ifLock)
    {
        //_ActionCtrler.CurAction.SetFaceLock(ifLock == 1);
    }
    //瞬移
    public void MoveActor(float distance)
    {
        //transform.position += FaceDir3D * distance;

    }
    //转换状态
    public virtual void SwitchAction()
    {
    }
    //召唤
    public virtual void CallPuppet(string objName)
    {
        /*
        BaseActorObj Obj = ActorManager.Mgr.GenActor(objName);
        PuppetNpc puppet = Obj as PuppetNpc;
        _ActionCtrler.CurAction.CallPuppet(puppet);
        */
    }
    //被打断
    public virtual void BeBreak()
    {

    }
    //离开完成 执行离开事件
    public virtual void LeaveComplete()
    {

    }
    public void Leave()
    {

    }
}
