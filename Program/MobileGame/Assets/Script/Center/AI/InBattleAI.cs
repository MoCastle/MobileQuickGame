using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBattleAI : BaseAI
{
    public BaseActor TargetActor;
    public InBattleAI(EnemyActor Actor, BaseActor InTargetActor) : base(Actor)
    {
        float CloseDis = ((FatEnemyActor)Actor).CloseDis;
        float HightDis = ((FatEnemyActor)Actor).HightCheck;
        TargetActor = InTargetActor;
        ReleaseAttack();
    }
    //
    public void ReleaseAttack( )
    {
        Transform TargetTrans = TargetActor.TransCtrl;
        Vector2 Distance = TargetActor.TransCtrl.position - Actor.TransCtrl.position;
        //Actor.SwitchAttackAI();
        AttackRate[] ActList;
        if (Mathf.Abs(Distance.x) > 3)
        {
            ActList = Actor.FarAttackArray;
        }
        else
        {
            ActList = Actor.CloseAttackArray;
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
    }
    public override void Update()
    {
        if (TargetActor == null )
        {
            Actor.SwitcfhToGuardState();
            return;
        }
        Vector2 Distance = TargetActor.TransCtrl.position - Actor.TransCtrl.position;
        //先面向玩家
        Actor.FaceTo(Distance);
        
        //检查玩家是否还在作战区域
        //不在 进入搜寻状态
    }
    public override void EndAI()
    {
        ReleaseAttack();
    }
}
