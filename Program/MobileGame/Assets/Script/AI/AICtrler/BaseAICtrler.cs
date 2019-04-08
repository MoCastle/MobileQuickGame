using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public abstract class BaseAICtrler {
    #region 属性
    //行为链
    LinkedList<BaseBehaviour> _AIList;
    protected EnemyObj _ActorObj;
    protected ActionCtrler _ActionCtrler;
    BaseBehaviour _CurBehaviour
    {
        get
        {
            return _AIList.First == null? null: _AIList.First.Value;
        }
    }
	public BaseAICtrler( EnemyObj enemyObj,ActionCtrler actionCtrler )
    {
        _AIList = new LinkedList<BaseBehaviour>();
        _ActorObj = enemyObj;
        _ActionCtrler = actionCtrler;
    }
    protected BaseActorObj _TargetActor;
    public BaseActorObj TargetActor
    {
        get
        {
            return _TargetActor;
        }
    }
    #endregion

    #region 管理行为链
    //头插行为
    public void HeadAddBehaviour( BaseBehaviour behaviour)
    {
        _AIList.AddFirst(behaviour);
    }
    //尾插行为
    public void TailAddBehaviour(BaseBehaviour behaviour)
    {
        _AIList.AddLast(behaviour);
    }
    //将头放到尾
    public void HeadPutTaile()
    {
        BaseBehaviour FirstNode = _AIList.First.Value;
        _AIList.RemoveFirst();
        TailAddBehaviour(FirstNode);
    }
    //移除头节点
    public void RemoveHead()
    {
        _AIList.RemoveFirst();
    }
    //重置链条
    protected void ClearAIList()
    {
        if(_AIList.Count > 0)
        {
            _CurBehaviour.ComplteteBehaviour();
            _AIList.Clear();
        }
    }
    #endregion

    #region 事件

    #endregion

    #region 生命周期
    //帧事件
    public virtual void Update()
    {
        if (_AIList.Count > 0)
        {
            _AIList.First.Value.Update();
        }
        else
        {
            GenBehaviour();
        }
    }
    #endregion
    //生成行为
    protected abstract void GenBehaviour();
    #region 对外接口
    public void CompleteTarget()
    {
        GenBehaviour();
    }
    //攻击目标
    public void SetTargetActor( BaseActorObj targetActor)
    {
        _TargetActor = targetActor;
    }
    public virtual void BeBreak()
    {
        GenBehaviour();
    }
    #endregion


}
