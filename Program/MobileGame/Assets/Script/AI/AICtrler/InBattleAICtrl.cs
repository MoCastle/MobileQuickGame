using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InBattleAICtrl : AICtrler
{
    //正在攻击的目标
    BaseActor _BattleTarget;
    public float WaitingTime = 3f;
    float CountTime = 0;

    public InBattleAICtrl(EnemyActor InActor) :base(InActor )
    {

    }

    public override void LogicUpdate()
    {
        if( CurTarget != Actor.CheckInBattleArea( ) )
        {
            GenAction();
        }
    }

    //外部接口
    #region
    bool _IfFoundEnemy = false;
    public bool IfFoundEnemy
    {
        get
        {
            if( (Target!= null)!= _IfFoundEnemy)
            {
                _IfFoundEnemy = Target != null;
                CountTime = 0;
            }
            return _IfFoundEnemy;
        }
    }
    //设置正在攻击的目标
    protected void SetTarget( BaseActor InTarget )
    {
        _BattleTarget = InTarget;
    }

    //重置攻击目标
    protected void FoundTarget( )
    {
        BaseActor AtcTarget = Actor.CheckInBattleArea();
        Target = AtcTarget;
    }

    //生成活动
    public override void GenAction()
    {
        FoundTarget();
        if( IfFoundEnemy )
        {
            GenBattleAction();
        }
        else
        {
            if(CountTime>WaitingTime)
            {
                CountTime = 0f;
                ReturnGuard();
            }
            else
            {
                CountTime = CountTime + Time.deltaTime;
            }
            
        }
    }
    void ReturnGuard( )
    {
        Actor.EnterGuarding();
    }

    abstract protected void GenBattleAction();

    #endregion

}
