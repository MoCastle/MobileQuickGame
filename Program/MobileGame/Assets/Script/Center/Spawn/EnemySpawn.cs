using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : BaseSpawn
{
    //敌人模板
    public GameObject EnemySample;

    public void Awake()
    {
        GenActor();
    }
    //生怪
    public override GameObject GenActor()
    {
        Vector3 NewPs = this.transform.position;
        NewPs.z = 0;
        GameObject NewEnemy = GamePoolManager.Manager.GenObj(EnemySample);
        BaseActor EnemyActor = NewEnemy.GetComponent<BaseActor>();
        EnemyActor.AddDeathEvent(Break);
        NewEnemy.transform.position = NewPs;
        NewEnemy.transform.rotation = transform.rotation;
        return NewEnemy;
    }
    public void Break( )
    {
        GenActor();
    }
}
