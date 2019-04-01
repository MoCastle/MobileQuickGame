﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;

public class EnemyObj : BaseActorObj
{
    #region 临时功能
    public Slider _LifeSlider;
    public Slider LifeSlider
    {
        get
        {
            if (_LifeSlider == null)
            {
                _LifeSlider = transform.Find("WorldCanvas").Find("LifeSlide").GetComponent<Slider>();
            }
            return _LifeSlider;
        }
    }
    #endregion
    #region 属性
    public string AICtrlerName;
    BaseAICtrler _AICtrler;

    public BaseAICtrler AICtrler
    {
        get
        {
            if(_AICtrler == null)
            {
                Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
                Type GetState = assembly.GetType(AICtrlerName);
                _AICtrler = (BaseAICtrler)Activator.CreateInstance(GetState, new object[] { this, ActionCtrl }); // 创建类的实例，返回为 object 类型，需要强制类型转换
            }
            return _AICtrler;
        }
    }
    BoxCollider2D _GuardBox;
    BoxCollider2D _GuardinArea
    {
        get
        {
            if(_GuardBox == null)
            {
                _GuardBox = transform.Find("GuardingArea").GetComponent<BoxCollider2D>();
            }
           return _GuardBox;
        }
           
    }
    //堆对象缓存 提快计算速度
    BoxCollider2D[] _ColliderList;
    #endregion

    #region 对外接口
    //寻找敌人
    public Collider2D[] FindEnemy( int Layer )
    {
        if(!_GuardinArea.enabled)
        {
            return null;
        }
        if(_ColliderList == null)
        {
            _ColliderList = new BoxCollider2D[5];
        }
        ContactFilter2D _contactFilter = new ContactFilter2D();
        _contactFilter.SetLayerMask(Layer);
        _GuardinArea.OverlapCollider(_contactFilter, _ColliderList);
        return _ColliderList;
    }
    #endregion

    protected override void LogicAwake()
    {
        //throw new System.NotImplementedException();
        _IDLayer = 1 << LayerMask.NameToLayer("Player");

        Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
        Type GetState = assembly.GetType(AICtrlerName);
        _AICtrler = (BaseAICtrler)Activator.CreateInstance(GetState, new object[] { this,ActionCtrl }); // 创建类的实例，返回为 object 类型，需要强制类型转换
        _IDLayer = 1 << LayerMask.NameToLayer("Player");
    }
    public override void LogicUpdate()
    {
        //Debug.Log(_ActorPropty.ph);
        base.LogicUpdate();
        if(_AICtrler!=null)
        {
            _AICtrler.Update();
        }
        LifeSlider.value = _ActorPropty.PercentLife;
    }

    void ClearAnimParams()
    {
        AnimatorControllerParameter[] animParams = ActionCtrl.GetAnimParams;
        foreach(AnimatorControllerParameter param in animParams)
        {
            if (param.type == AnimatorControllerParameterType.Bool && param.name != "IsOnGround" && param.name != "IsJustOnGround")
            {
                ActionCtrl.SetBool(param.name,false);
            }
        }
    }

    public override void SwitchAction()
    {
        base.SwitchAction();
    }
    #region 事件
    public override void BeBreak()
    {
        _AICtrler.BeBreak();
    }
    #endregion
}
