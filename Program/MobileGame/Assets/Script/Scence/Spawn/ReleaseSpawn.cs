using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//该卵用于孵化怪物 怪物出生后会走想目标
public class ReleaseSpawn : EnemySpawn {
    static ReleaseSpawn _Spawn;
    public static void PubReleaseActor(string name)
    {
        if(_Spawn != null)
        {
            _Spawn.ReleaseActor(name);
        }
    }
    public GameObject Target;
	
    public void ReleaseActor(string name)
    {
        Vector3 NewPs = this.transform.position;
        NewPs.z = 0;
        BaseActor newActor = CurDir.GenActor(name);

        EnemyActor newEnemy = newActor as EnemyActor;
        if (newEnemy == null)
        {
            return;
        }
        newEnemy.transform.position = NewPs;
        //NewEnemy.transform.rotation = transform.rotation;
        newEnemy.BirthPlace = NewPs;
        newEnemy.ActorPropty.Init();

        AICtrler enemyAICtrler = newEnemy.AICtrl;
        enemyAICtrler.PushAction(new RunToAction(newEnemy,enemyAICtrler,Target.transform.position));
        Debug.Log("Birth");
        Debug.Log(newActor.transform.position);
        return;
    }

    protected override void Action()
    {
        _Spawn = this;
    }
}
