using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class ActionCtrler {
    float _AnimSpeed = 1;
    public float AnimSpeed
    {
        get
        {
            return _AnimSpeed;
        }
        set
        {
            if(_AnimSpeed != value)
            {
                _AnimSpeed = value;
                _Animator.speed = _AnimSpeed;
            }
        }
    }
    public AnimatorControllerParameter[] GetAnimParams
    {
        get
        {
            AnimatorControllerParameter[] animatorParams = _Animator.parameters;
            return animatorParams;
        }
    }
    //动画进度条
    public float AnimPercent
    {
        get
        {
            return _Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
    }

    public ActionCtrler(BaseActorObj actor, Animator animator, List<ActionInfo> actionList)
    {
        _ActorObj = actor;
        _Animator = animator;
        _ActionList = actionList;
    }

    BaseActorObj _ActorObj;
    Animator _Animator;
    List<ActionInfo> _ActionList;
    public void SetTriiger( string name)
    {
        _Animator.SetTrigger(name);
    }
    public void SetBool( string name, bool value )
    {
        Debug.Log("BoolName " +name);
        _Animator.SetBool(name,value);
    }
    public void SetFloat(string name, float value)
    {
        _Animator.SetFloat(name, value);
    }
    int _CurAnimName;
    BaseAction _CurAction;
    public BaseAction CurAction
    {
        get
        {
            if(_CurAction == null)
            {
                _CurAction = new BaseAction(_ActorObj,new SkillInfo());
            }
            return _CurAction;
        }
        set
        {
            _CurAction = value;
        }
    }

    public void Update()
    {
        _Animator.SetFloat("AnimTime", _Animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if (_CurAction != null)
        {
            _CurAction.Update();
        }
    }

    #region 封装接口
    //判断动画状态
    public bool IsName(string name)
    {
        return _Animator.GetCurrentAnimatorStateInfo(0).IsTag(name);
    }
    #endregion

    #region 动画逻辑
    public void SwitchState()
    {
        
        if (_CurAction != null)
        {
            //了解之前的动画逻辑
            //_CurAction.CompleteFunc();
        }

        string NewActionName = "";
        _CurAnimName = _Animator.GetCurrentAnimatorStateInfo(0).tagHash;
        SkillInfo skillInfo = new SkillInfo();

        
        //先检查是否符合自己的设定
        if (_ActionList.Count > 0)
        {
            foreach (ActionInfo AnimInfo in _ActionList)
            {
                if (_Animator.GetCurrentAnimatorStateInfo(0).IsTag(AnimInfo.ActionName))
                {
                    skillInfo = SkillManager.Mgr.GetSkillInfo(AnimInfo.SkillID);
                    NewActionName = skillInfo.Name;
                    _Animator.SetFloat("AnimTime", 0);
                    break;
                }
            }
        }
        
        //再检查是否符合通用技能设定
        if (NewActionName == "" && SkillManager.Mgr.UsualActionList.Count > 0)
        {
            foreach (ActionInfo AnimInfo in SkillManager.Mgr.UsualActionList)
            {
                if (_Animator.GetCurrentAnimatorStateInfo(0).IsTag(AnimInfo.ActionName))
                {
                    skillInfo = SkillManager.Mgr.GetSkillInfo(AnimInfo.SkillID);
                    NewActionName = skillInfo.Name;
                    _Animator.SetFloat("AnimTime", 0);
                    break;
                }
            }
        }
        if (NewActionName == "")
        {
            NewActionName = "BaseAction";
        }
        Debug.Log("SwitchState" + NewActionName);
        Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
        Type GetState = assembly.GetType(NewActionName);
        BaseAction NewState = (BaseAction)Activator.CreateInstance(GetState, new object[] { _ActorObj,skillInfo }); // 创建类的实例，返回为 object 类型，需要强制类型转换
        _CurAction = NewState;
        _ActorObj.SwitchAction();
    }
    #endregion
}
