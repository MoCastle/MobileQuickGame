using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AICtrler {
    protected LinkedList<AIAction> AIActionList = new LinkedList<AIAction>();
    protected EnemyActor Actor;
    protected BaseActor Target;
    public BaseActor CurTarget
    {
        get
        {
            return Target;
        }
    }
    public AICtrler(EnemyActor InActor )
    {
        Actor = InActor;
    }
    public void Update()
    {
        if(AIActionList.First != null && Actor.Alive)
        {
            AIActionList.First.Value.Update();
        }
    }
    //设置攻击目标
    public virtual void FoundTarget( BaseActor InActor )
    {
        Target = InActor;
    }
    //生成战斗AI
    public virtual void EnterBattle()
    {
    }
    //重置AI列表
    public virtual void ResetAIStack()
    {
        AIActionList = new LinkedList<AIAction>();
    }
    //弹栈
    public virtual void PopAIStack()
    {
        AIActionList.RemoveFirst();
    }
}
