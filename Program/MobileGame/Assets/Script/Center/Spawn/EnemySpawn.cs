using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : BaseSpawn
{
    //敌人模板
    public GameObject EnemySample;
    public override GameObject GenActor()
    {
        Vector3 NewPs = this.transform.position;
        NewPs.z = 0;
        GameObject NewPlayer = GamePoolManager.Manager.GenObj(EnemySample);
        EnemySample.transform.position = NewPs;
        EnemySample.transform.rotation = transform.rotation;
        return NewPlayer;
    }
}
