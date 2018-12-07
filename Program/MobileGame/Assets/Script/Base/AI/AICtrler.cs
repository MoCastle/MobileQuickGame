using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AICtrler {
    
    protected LinkedList<AIAction> AIActionList = new LinkedList<AIAction>();
    protected EnemyObj Actor;
    protected BaseActor Target;
    public BaseActor CurTarget
    {
        get
        {
            return Target;
        }
        set
        {
            Target = value;
        }
    }

    #region 事件
    public void EnterState()
    {

    }
    #endregion

    public AICtrler(EnemyObj InActor )
    {
        Actor = InActor;
    }
    public void Update()
    {
        if(AIActionList.First != null && Actor.Alive)
        {
            AIActionList.First.Value.Update();
        }else
        {
            GenAction();
        }

        LogicUpdate();
    }
    public abstract void LogicUpdate();

    //设置攻击目标
    public virtual void FoundTarget( BaseActor InActor )
    {
        Target = InActor;
    }

    //外部接口
    #region
    //重置AI栈
    public virtual void ResetAIStack()
    {
        if(AIActionList.First != null )
        {
            AIActionList.First.Value.EndAction();
        }
        AIActionList = new LinkedList<AIAction>();
    }

    //往里压
    public void PushAction(AIAction InAction)
    {
        AIActionList.AddLast(InAction);
    }

    //生成战斗AI
    public abstract void GenAction();
    //弹栈
    public virtual void PopAIStack()
    {
        AIActionList.First.Value.EndAction();
        AIActionList.RemoveFirst();
    }

    //中断处理
    public virtual void Break()
    {
        ResetAIStack();
        GenAction();
    }
    //
    public virtual void BeAttack()
    { }

    #endregion


}
