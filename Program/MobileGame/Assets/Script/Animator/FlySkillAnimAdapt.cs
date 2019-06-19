using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using GameScene;

public class FlySkillAnimAdapt : AnimAdapt
{
    [Header("运动曲线")]
    public AnimationCurve Curve;
    [Header("运动速度")]
    public float MoveSpeed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ActionName == null || ActionName == "")
        {
            ActionName = "BaseAction";
        }
        BaseActorObj NoticeObject = animator.gameObject.GetComponentInParent<BaseActorObj>();
        Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
        Type GetState = assembly.GetType(ActionName);
        BaseAction NewState = (BaseAction)Activator.CreateInstance(GetState, new object[] { NoticeObject, SkillInfo }); // 创建类的实例，返回为 object 类型，需要强制类型转换
        NoticeObject.SwitchAction(NewState);
    }
}
