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
	public RunToBehaviour(EnemyObj enemy, BaseAICtrler ctrler,float distance) :base(enemy, ctrler)
    {
        _TargetDistance = distance;
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
        _ActionCtrler.SetTriiger("QuickRun");
    }
    protected override void InitFunc()
    {
        
        _Target = _Actor.transform.position.x;
        _Target += (_Actor.FaceDir * _TargetDistance).x;
    }
    public override void ComplteteBehaviour()
    {
        base.ComplteteBehaviour();
        _ActionCtrler.SetBool("QuickRun", false);

        _AICtrler.RemoveHead();
        
    }
}
