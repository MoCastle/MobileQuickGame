using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : BaseSpawn
{
    //敌人模板
    public GameObject EnemySample;

    public override void LogicStart()
    {
        
    }

    //生怪
    public override BaseActorObj GenActor()
    {
        /*
        Vector3 NewPs = this.transform.position;
        NewPs.z = 0;
        BaseActorObj NewActor = CurDir.GenActor(EnemySample);
        EnemyObj NewEnemy = NewActor as EnemyObj;
        if( NewEnemy == null )
        {
            return null;
        }
        NewEnemy.transform.position = NewPs;
        NewEnemy.transform.rotation = transform.rotation;
        //NewEnemy.BirthPlace = NewPs;
        //NewEnemy.AddDeathEvent(Break);
        NewEnemy.ActorPropty.Init();
        */
        return null;
    }
    
    public void Break( )
    {
        GenActor();
    }
    /*
    protected override void Action()
    {
        throw new System.NotImplementedException();
    }
    */
}
