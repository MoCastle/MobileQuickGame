using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using GameScene;

public class AnimAdapt : StateMachineBehaviour
{
    [Title("不要重置输入信息", "black")]
    public bool dontResetInput;
    [Title("对应逻辑名字", "black")]
    public string ActionName;
    [Title("属性", "black")]
    public SkillInfo SkillInfo;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BaseActorObj NoticeObject = animator.gameObject.GetComponentInParent<BaseActorObj>();
        BaseAction NewState = (BaseAction)GenState(animator, NoticeObject); // 创建类的实例，返回为 object 类型，需要强制类型转换
        NoticeObject.SwitchAction(NewState);
    }

    protected virtual object GenState(Animator animator, BaseActorObj baseActorObj)
    {
        if (ActionName == null || ActionName == "")
        {
            ActionName = "BaseAction";
        }
        BaseActorObj NoticeObject = baseActorObj;
        Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
        Type GetState = assembly.GetType(ActionName);
        return Activator.CreateInstance(GetState, new object[] { NoticeObject, SkillInfo });
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BaseActorObj NoticeObject = animator.gameObject.GetComponent<BaseActorObj>();
    }
}
