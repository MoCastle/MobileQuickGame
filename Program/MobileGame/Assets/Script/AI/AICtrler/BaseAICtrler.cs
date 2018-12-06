using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAICtrler {
    //行为链
    LinkedList<BaseBehaviour> _AIList;
    protected EnemyObj _ActorObj;
    protected ActionCtrler _ActionCtrler;
	public BaseAICtrler( EnemyObj enemyObj,ActionCtrler actionCtrler )
    {
        _AIList = new LinkedList<BaseBehaviour>();
        _ActorObj = enemyObj;
        _ActionCtrler = actionCtrler;
    }
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
    //重置链条
    protected void ClearAIList()
    {
        _AIList.Clear();
    }
    #endregion

    //帧事件
    public virtual void Update()
    {
        if(_AIList.Count >0)
        {
            _AIList.First.Value.Update();
        }
        else
        {
            GenBehaviour();
        }
    }
    //生成行为
    protected abstract void GenBehaviour();
}
