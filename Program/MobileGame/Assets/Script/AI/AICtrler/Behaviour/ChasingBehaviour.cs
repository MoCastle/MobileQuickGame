using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class ChasingBehaviour : BaseBehaviour {
    BaseAction ChasingAction;
    protected float _Distacnce;
    protected float _LimitTime;
    protected float _CountTime;
    public ChasingBehaviour(EnemyObj Enemy, BaseAICtrler ctrler,float distance, float limitTime = -1) :base(Enemy, ctrler)
    {
        ChasingAction = Enemy.ActionCtrl.CurAction;
        _Distacnce = distance;
        _LimitTime = limitTime;
    }
    public override void Update()
    {
        base.Update();
        BaseActorObj target = _AICtrler.TargetActor;
        if(target != null)
        {
            if (Mathf.Abs(target.transform.position.x - _Actor.transform.position.x)< _Distacnce)
            {
                ComplteteTarget();
            }
            else
            {
                if(_CountTime > 0&&_CountTime < Time.time)
                {
                    TimeUp();
                }
                _ActionCtrler.CurAction.InputDirect((target.transform.position - _Actor.transform.position).normalized);
            }
                
        }else
        {
            LostTarget();
        }
        
    }
    #region 追击逻辑功能
    //追踪时间到
    protected virtual void TimeUp()
    {
        ComplteteBehaviour();
    }
    //完成任务
    protected virtual void ComplteteTarget()
    {
        ComplteteBehaviour();
    }
    //丢失目标
    protected virtual void LostTarget()
    {
        ComplteteBehaviour();
    }
    #endregion

    public override void ComplteteBehaviour()
    {
        if (!Inited)
        {
            return;
        }
        base.ComplteteBehaviour();
        //
        _ActionCtrler.SetBool("Run", false);
        //最后再对列表进行操作
        _AICtrler.RemoveHead();
        //该函数不能反复被执行
        
        
    }
    public override void OnStartBehaviour()
    {
        base.OnStartBehaviour();
        _ActionCtrler.SetBool("Run", true);
        
    }
    protected override void InitFunc()
    {
        if (_LimitTime > 0)
            _CountTime = Time.time + _LimitTime;
        else
            _CountTime = -1;
    }
}
