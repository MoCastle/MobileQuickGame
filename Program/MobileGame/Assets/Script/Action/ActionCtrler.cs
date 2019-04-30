﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using GameScene;
using BaseFunc;

public class ActionCtrler:BaseFSM {
    #region 私有属性
    float m_AnimSpeed = 1;
    BaseActorObj m_ActorObj;
    Animator m_Animator;
    int m_CurAnimName;
    BaseAction m_CurAction;
    #endregion
    #region 对外接口
    public float AnimSpeed
    {
        get
        {
            return m_AnimSpeed;
        }
        set
        {
            if (m_AnimSpeed != value)
            {
                m_AnimSpeed = value;
                m_Animator.speed = m_AnimSpeed;
            }
        }
    }
    public BaseAction CurAction
    {
        get
        {
            if (m_CurAction == null)
            {
                m_CurAction = new BaseAction(m_ActorObj, new SkillInfo());
            }
            return m_CurAction;
        }
        set
        {
            m_CurAction = value;
        }
    }
    public AnimatorControllerParameter[] GetAnimParams
    {
        get
        {
            AnimatorControllerParameter[] animatorParams = m_Animator.parameters;
            return animatorParams;
        }
    }
    //动画进度条
    public float AnimPercent
    {
        get
        {
            return m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
    }
    #endregion
    #region 流程
    public ActionCtrler(BaseActorObj actor, Animator animator)//, List<ActionInfo> actionList)
    {
        if (animator == null)
        { Debug.Log("ActionCtrler Error Animator == null"); }
        m_ActorObj = actor;
        m_Animator = animator;
        //_ActionList = actionList;
    }

    public void Update()
    {
        m_Animator.SetFloat("AnimTime", m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if (m_CurAction != null)
        {
            m_CurAction.Update();
        }
    }
    public void SwitchState(BaseState state)
    {
        base.Switch(state);
        m_CurAction = state as BaseAction;
    }
    #endregion
    #region 动画
    //List<ActionInfo> _ActionList;
    public void SetTriiger(string name)
    {
        m_Animator.SetTrigger(name);
    }

    public void SetBool(string name, bool value)
    {
        m_Animator.SetBool(name, value);
    }

    public void SetFloat(string name, float value)
    {
        m_Animator.SetFloat(name, value);
    }

    //判断动画状态
    public bool IsName(string name)
    {
        return m_Animator.GetCurrentAnimatorStateInfo(0).IsTag(name);
    }

    public void PlayerAnim(string stateName)
    {
        m_Animator.Play(stateName, -1, 0f);
        m_Animator.Update(0);
    }
    #endregion
}
