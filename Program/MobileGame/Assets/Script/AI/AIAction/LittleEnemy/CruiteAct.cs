using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiteAct : AIAction
{
    //是否正在巡逻
    bool IsMoving = false;
    //站岗时间
    float GuardingTime = 0;
    //巡逻点数组
    Vector2[] PTArr;
    int CurPS = 0;
    //前进方向
    Vector2 FaceDir;

    //范围
    float Range = 10;

    public CruiteAct(EnemyActor InActor, AICtrler InCtrler) :base(InActor,InCtrler)
    {
    }

    void GenPT()
    {
        PTArr = new Vector2[2];
        Vector2 CurPoint = _Actor.BirthPlace;
        Vector2 StartPoint = CurPoint;
        Vector2 EndPoint = CurPoint;
        StartPoint.x = StartPoint.x - Range / 2;
        EndPoint.x = StartPoint.x + Range / 2;
        PTArr[0] = StartPoint;
        PTArr[1] = EndPoint;
        CurPS = 0;
    }

    //获取正在前往的位置
    Vector2 CurPT( )
    {
        return PTArr[CurPS];
    }

    //下一个位置
    void NextPT( )
    {
        CurPS = (CurPS + 1) % PTArr.Length;
    }

    public override void LogicUpdate()
    {
        if( IsMoving )
        {
            
            Vector2 CurPos = _Actor.TransCtrl.position;
            Vector2 Dis = (CurPT() - CurPos) - _Actor.MoveSpeed/30 * FaceDir;

            if( Dis.x * FaceDir.x < 0)
            {
                IsMoving = false;
                _Actor.AnimCtrl.SetBool("Run", false);
                NextPT( );
            }
        }
        else
        {
            if( GuardingTime > 2 )
            {
                GuardingTime = 0;
                IsMoving = true;
                //设置前进方向
                Vector2 CurPos = _Actor.TransCtrl.position;
                FaceDir = (CurPT() - CurPos);
                FaceDir.y = 0;
                FaceDir = FaceDir.normalized;
                _Actor.FaceTo(FaceDir);
                _Actor.AnimCtrl.SetBool("Run" ,true);
            }
            else
            {
                GuardingTime = GuardingTime + Time.deltaTime;
            }

        }
    }

    public override void Start()
    {
        GenPT();
    }
    public override void EndAction()
    {
        _Actor.AnimCtrl.SetBool("Run" ,false);
    }
}
