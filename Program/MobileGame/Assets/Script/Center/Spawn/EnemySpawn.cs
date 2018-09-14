﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : BaseSpawn
{
    //敌人模板
    public GameObject EnemySample;

    public override void LogicStart()
    {
        GenActor();
    }

    //生怪
    public override BaseActor GenActor()
    {
        Vector3 NewPs = this.transform.position;
        NewPs.z = 0;
        BaseActor NewActor = CenceDir.GenActor(EnemySample);
        EnemyActor NewEnemy = NewActor as EnemyActor;
        if( NewEnemy == null )
        {
            return null;
        }
        NewEnemy.transform.position = NewPs;
        NewEnemy.transform.rotation = transform.rotation;
        NewEnemy.BirthPlace = NewPs;
        NewEnemy.AddDeathEvent(Break);
        NewEnemy.ActorPropty.Init();
        return NewEnemy;
    }

    public void Break( )
    {
        GenActor();
    }
}
