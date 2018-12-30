using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
public class AnimAdapt : StateMachineBehaviour
{
    [Title("对应逻辑名字", "black")]
    public string ActionName;
    [Title("属性", "black")]
    public SkillInfo SkillInfo;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(ActionName== null || ActionName == "")
        {
            ActionName = "BaseAction";
        }
        BaseActorObj NoticeObject = animator.gameObject.GetComponent<BaseActorObj>();
        Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
        Type GetState = assembly.GetType(ActionName);
        BaseAction NewState = (BaseAction)Activator.CreateInstance(GetState, new object[] { NoticeObject, SkillInfo }); // 创建类的实例，返回为 object 类型，需要强制类型转换
        NoticeObject.ActionCtrl.CurAction = NewState;
        NoticeObject.SwitchAction();
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BaseActorObj NoticeObject = animator.gameObject.GetComponent<BaseActorObj>();
    }
}
