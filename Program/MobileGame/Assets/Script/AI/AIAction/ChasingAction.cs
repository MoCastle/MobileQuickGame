using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingAction : AIAction {
    //偏移量
    Vector3 Shift;
    //实际偏移量
    Vector3 RealityShift
    {
        get
        {
            Vector3 ReturnShift = Shift;
            if( ReturnShift.x * _Actor.ActorState.Direction.x < 0 )
            {
                ReturnShift.x = ReturnShift.x * -1;
            }
            return ReturnShift;
        }
    }
    //误差范围
    float MisArea;
    //限时
    float LimitTime;
    float _TimeCount;
    public ChasingAction( EnemyActor InActor, AICtrler InCtrler, float InMisArea = 2f, Vector3 InShift
        = new Vector3(), float LimitTime = 0 ) :base( InActor, InCtrler)
    {
        
        MisArea = InMisArea;
        Shift = InShift;
    }
    public override void LogicUpdate()
    {
        if( _Ctrler.CurTarget == null )
        {
            _Ctrler.GenAction();
            return;
        }
        Vector2 Dis = _Ctrler.CurTarget.TransCtrl.position - _Actor.TransCtrl.position - RealityShift;
        _Actor.FaceTo(Dis);
        //追击条件达成 结束追击
        if (Mathf.Abs(Dis.x) < MisArea)
        {
            _Actor.AnimAdaptor.IsRuning = false;
            _Ctrler.PopAIStack();
        }
        else if(LimitTime > 0.0001f)
        {
            _TimeCount = _TimeCount + Time.deltaTime;
            if (_TimeCount > LimitTime)
            {
                    _Ctrler.PopAIStack();
            }
        }
        
    }
    public override void Start()
    {
        if (_Ctrler.CurTarget == null)
        {
            EndAction();
            _Ctrler.GenAction();
            return;
        }
        Vector2 Dis = _Ctrler.CurTarget.TransCtrl.position - _Actor.TransCtrl.position - RealityShift;
        _Actor.FaceTo(Dis);
        //追击条件达成 结束追击
        if (Mathf.Abs(Dis.x) < MisArea)
        {
            _Ctrler.PopAIStack();
        }else
        {
            _Actor.AnimAdaptor.IsRuning = true;
        }
        
    }
    public override void EndAction()
    {
        _Actor.AnimAdaptor.IsRuning = false;
    }
}
