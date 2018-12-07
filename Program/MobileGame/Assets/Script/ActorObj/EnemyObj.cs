using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class EnemyObj : BaseActorObj
{
    #region 属性
    public string AICtrlerName;
    BaseAICtrler _AICtrler;
    BoxCollider2D _GuardBox;
    BoxCollider2D _GuardinArea
    {
        get
        {
            if(_GuardBox == null)
            {
                _GuardBox = transform.FindChild("GuardingArea").GetComponent<BoxCollider2D>();
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

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(_AICtrler!=null)
        {
            _AICtrler.Update();
        }
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
    
}
