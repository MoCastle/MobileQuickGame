using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : BaseBehaviour {
    float _Time;
    float _CountTime;
	public GuardBehaviour(EnemyObj enemyObj,BaseAICtrler aiCtrler,float time ):base(enemyObj, aiCtrler)
    {
        _Time = time;
    }
    public override void Update()
    {
        base.Update();
        if (_CountTime < Time.time)
        {
            ComplteteBehaviour();
        }
    }
    public override void OnStartBehaviour()
    {
        base.OnStartBehaviour();
    }
    protected override void InitFunc()
    {
        if(Inited)
        {
            _CountTime = Time.time + _Time;
        }
    }
    public override void ComplteteBehaviour()
    {
        if (!Inited)
        {
            return;
        }
        base.ComplteteBehaviour();
        
        _AICtrler.HeadPutTaile();
    }
}
