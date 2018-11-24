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
    public override BaseActor GenActor()
    {
        Vector3 NewPs = this.transform.position;
        NewPs.z = 0;
        BaseActor NewPlayer = GenActor("Player");
        PlayerActor Actor = NewPlayer as PlayerActor;
        if(Actor == null || PlayerDelegate.GetPropty( ) == null )
        {
            PlayerDelegate.SetPropty(Actor.ActorPropty);
        }else
        {
            Actor.ActorPropty = PlayerDelegate.GetPropty();
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
