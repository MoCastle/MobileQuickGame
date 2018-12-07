using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpChasing : ChasingBehaviour {

    public SpeedUpChasing(EnemyObj enemy, BaseAICtrler ctrler, float distance, float limitTime = -1) :base(enemy, ctrler, distance, limitTime)
    {

    }
    protected override void ComplteteTarget()
    {
        //base.ComplteteTarget();
        ComplteteBehaviour();
        _ActionCtrler.SetBool("QuickRun", false);
    }
    protected override void LostTarget()
    {
        ComplteteBehaviour();
    }
    protected override void TimeUp()
    {
        ComplteteBehaviour();
    }
    public override void OnStartBehaviour()
    {
        base.OnStartBehaviour();
        _ActionCtrler.SetBool("QuickRun",true);
    }
}
