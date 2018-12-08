using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToBehaviour:BaseBehaviour {
    float _Target;
    float _TargetDistance;
    float _CountDistance
    {
        get
        {
            return _Target -_Actor.transform.position.x;
        }
    }
    string _ParamName;
    Action _EndFunc;
	public RunToBehaviour(EnemyObj enemy, BaseAICtrler ctrler,float distance,string paramName="QuickRun", Action endFunc = null) :base(enemy, ctrler)
    {
        _EndFunc = endFunc;
        _TargetDistance = distance;
        _ParamName = paramName;
    }
    public override void Update()
    {
        base.Update();
        if(_Inited)
        {
            if(Mathf.Abs(_CountDistance)<0.5f)
            {
                ComplteteBehaviour();
            }else
            {
                _ActionCtrler.CurAction.InputDirect(Vector2.right * (_CountDistance < 0 ? -1 : 1));
            }
        }
    }
    public override void OnStartBehaviour()
    {

        base.OnStartBehaviour();
        _ActionCtrler.SetBool(_ParamName, true);
    }
    protected override void InitFunc()
    {
        
        _Target = _Actor.transform.position.x;
        _Target += (_Actor.FaceDir * _TargetDistance).x;
    }
    public override void ComplteteBehaviour()
    {
        
        base.ComplteteBehaviour();
        _ActionCtrler.SetBool(_ParamName, false);

        _AICtrler.RemoveHead();
        if (_EndFunc != null)
        {
            _EndFunc();
        }
    }
}
