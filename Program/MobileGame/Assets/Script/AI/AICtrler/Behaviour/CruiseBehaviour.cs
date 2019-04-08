using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class CruiseBehaviour : BaseBehaviour {
    string AnimName = "Run";
    Vector2 _Target;
    //行进方向
    int _MoveDir;
    //总距离
    
    float _Distance
    {
        get
        {
            return _Target.x - _Actor.transform.position.x;
        }
    }
    #region 生命周期
    public CruiseBehaviour(EnemyObj enemy, BaseAICtrler ctrler, Vector2 target) : base(enemy, ctrler)
    {
        _Target = target;
    }

    public override void Update()
    {
        base.Update();
        if (_MoveDir * _Distance < 0)
        {
            ComplteteBehaviour();
        }
        Collider2D[] enemys = _Actor.FindEnemy(_Actor.IDLayer);
        if (enemys!= null&&enemys[0] != null)
        {
            _AICtrler.SetTargetActor(enemys[0].GetComponent<BaseActorObj>());
            _AICtrler.CompleteTarget();
        }
    }
    #endregion


    public override void ComplteteBehaviour()
    {
        if (!Inited)
        {
            return;
        }
        base.ComplteteBehaviour();
        _ActionCtrler.SetBool("Run", false);
        _AICtrler.HeadPutTaile();

    }
    #region 事件
    public override void OnStartBehaviour()
    {
        base.OnStartBehaviour();
        _ActionCtrler.SetBool("Run", true);
        
    }
    public override void OnSwitchAnime()
    {
        base.OnSwitchAnime();
    }

    protected override void InitFunc()
    {
        _MoveDir = _Distance < 0 ? -1 : 1;
        _ActionCtrler.CurAction.InputDirect(_MoveDir * Vector2.right);
    }
    #endregion
}
