using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AICtrler {
    
    protected LinkedList<AIAction> AIActionList = new LinkedList<AIAction>();
    protected EnemyActor Actor;
    public BaseActor Target;
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

        if (AIActionList.First == null && Actor.Alive && Actor.AnimCtrl.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            AIActionList = new LinkedList<AIAction>();
            GuardAction Guarding = new GuardAction(Actor, this);
            PushAction(Guarding);
        }
        LogicUpdate();
    }
    public abstract void LogicUpdate();
    //设置攻击目标
    public virtual void FoundTarget( BaseActor InActor )
    {
        Target = InActor;
    }
    //生成战斗AI
    public abstract void EnterBattle();
    //重置AI列表
    public virtual void ResetAIStack()
    {
        AIActionList = new LinkedList<AIAction>();
    }
    //往里压
    public void PushAction(AIAction InAction)
    {
        AIActionList.AddFirst(InAction);
    }
    //弹栈
    public virtual void PopAIStack()
    {
        AIActionList.RemoveFirst();
    }
    public void Break()
    {
        AIActionList = new LinkedList<AIAction>();
        EnterBattle();
    }
}
