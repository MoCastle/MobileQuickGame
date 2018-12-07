using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : BaseSpawn
{
    public GameObject Player
    {
        get
        {
            return GameCtrl.GameCtrler.PlayerSample;
        }
    }
    public override BaseActorObj GenActor()
    {
        Vector3 NewPs = this.transform.position;
        NewPs.z = 0;
        BaseActorObj NewPlayer = GenActor("Player");
        PlayerObj Actor = NewPlayer as PlayerObj;
        if(Actor == null || PlayerDelegate.GetPropty( ) == null )
        {
            PlayerDelegate.SetPropty(Actor._ActorPropty);
        }else
        {
            Actor._ActorPropty = PlayerDelegate.GetPropty();
        }
        Actor.ActorPropty.Init();
        Actor.transform.position = NewPs;
        return Actor;
    }

    protected override void Action()
    {
        GenActor();
    }
}
