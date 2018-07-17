using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum InBattleState
{
    AttackStart,
    AttackTing,
    AttackEnd
}

public class InBattleAI : BaseAI
{

    InBattleState State;
    public bool AttackTing;
    public BaseActor TargetActor;
    float _SpaceTimeCount;
    public float SpaceTime
    {
        get
        {
            return 6f;
        }
    }

    public InBattleAI(EnemyActor Actor, BaseActor InTargetActor) : base(Actor)
    {
        float CloseDis = ((FatEnemyActor)Actor).CloseDis;
        float HightDis = ((FatEnemyActor)Actor).HightCheck;
        TargetActor = InTargetActor;
        State = InBattleState.AttackStart;
    }
    //
    public void ReleaseAttack( )
    {
        Transform TargetTrans = TargetActor.TransCtrl;
        Vector2 Distance = TargetActor.TransCtrl.position - Actor.TransCtrl.position;
        //Actor.SwitchAttackAI();
        AttackRate[] ActList;
        if (Mathf.Abs(Distance.x) < ((FatEnemyActor)Actor).CloseDis)
        {
            ActList = Actor.CloseAttackArray;
        }
        else if(Mathf.Abs(Distance.x) < ((FatEnemyActor)Actor).FarDis)
        {
            ActList = Actor.FarAttackArray;
        }
        else
        {
            Actor.AICtrl = new ChasingAI(Actor, TargetActor);
            return;
        }

        AttackRate[] NewActList = new AttackRate[ActList.Length];
        float TotalRate = 0;
        for (int Idx = 0; Idx < ActList.Length; ++Idx)
        {
            NewActList[Idx] = ActList[Idx];
            TotalRate = ActList[Idx].Rate + TotalRate;
            NewActList[Idx].Rate = TotalRate;
        }
        float RandValue = Random.Range(0, TotalRate);
        foreach (AttackRate Act in NewActList)
        {
            if (Act.Rate >= RandValue)
            {
                Actor.AnimCtrl.SetTrigger(Act.Name);
                break;
            }
        }
        State = InBattleState.AttackTing;
    }
    public override void Update()
    {
        return;//zerg
        if (TargetActor == null )
        {
            Actor.SwitcfhToGuardState();
            return;
        }
        Vector2 Distance = TargetActor.FootTransCtrl.position - Actor.FootTransCtrl.position;
        //先面向玩家
        Actor.FaceTo(Distance);
        switch( State )
        {
            case InBattleState.AttackStart:
                ReleaseAttack();
                break;
            case InBattleState.AttackEnd:
                if( _SpaceTimeCount + SpaceTime < Time.time )
                {
                    State = InBattleState.AttackStart;
                }
                break;
            case InBattleState.AttackTing:
                if( Actor.AnimCtrl.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    EndAI();
                }
                break;
            default:
                break;
        }
        
        //检查玩家是否还在作战区域
        //不在 进入搜寻状态
    }

    public override void EndAI()
    {
        _SpaceTimeCount = Time.time;
        State = InBattleState.AttackEnd;
    }
}
