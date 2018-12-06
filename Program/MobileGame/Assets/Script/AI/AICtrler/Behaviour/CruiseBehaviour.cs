using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiseBehaviour : BaseBehaviour {
    string AnimName = "Run";
    public CruiseBehaviour(EnemyObj enemy, BaseAICtrler ctrler) :base(enemy, ctrler)
    {
        ChangeDir( );
    }
    public void Update()
    {

    }
    //设置参数
    void SetParam()
    {
        _ActionCtrler.SetBool("Run",true);
    }
    //转向
    void ChangeDir()
    {
        Vector2 faceDir = _Actor.FaceDir;
        faceDir.y = 0;
        faceDir.x *= -1;
        _Actor.FaceToDir(faceDir);
    }

    protected override void _CompleteBehaviour()
    {
        _ActionCtrler.SetBool("Run", false);
        _Ctrler.HeadPutTaile();
    }
}
