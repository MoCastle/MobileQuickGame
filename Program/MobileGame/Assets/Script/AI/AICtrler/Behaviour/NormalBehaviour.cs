using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBehaviour : BaseBehaviour {
    float _ContinueTime;
    float _Clock;
	// Use this for initialization
    public NormalBehaviour(EnemyObj enemy, BaseAICtrler ctrler,float continueTime) :base(enemy, ctrler)
    {
        _ContinueTime = continueTime;
    }
    public override void Update()
    {
        base.Update();
        if(Inited)
        {
            if(Time.time > _Clock)
            {
                ComplteteBehaviour();
            }
        }
    }

    public override void ComplteteBehaviour()
    {
        base.ComplteteBehaviour();
        _AICtrler.RemoveHead();
    }

    protected override void InitFunc()
    {
        _Clock = Time.time + _ContinueTime;
    }

}
